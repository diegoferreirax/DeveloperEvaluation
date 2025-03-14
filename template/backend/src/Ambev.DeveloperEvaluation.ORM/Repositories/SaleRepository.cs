﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Extensions;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of ISaleRepository using Entity Framework Core
/// </summary>
public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of SaleRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Register a new sale in the database
    /// </summary>
    /// <param name="sale">The sale to register</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The registered sale</returns>
    public async Task<Guid> RegisterSaleAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        await _context.Sales.AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return sale.Id;
    }

    /// <summary>
    /// Retrieves a sale by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale if found, Maybe otherwise</returns>
    public async Task<Maybe<Sale>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Sales.Include("Customer").AsNoTracking().FirstOrDefaultAsync(o => o.Id == id, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves a list of sale 
    /// </summary>
    /// <param name="pageSize">Page size of the sales</param>
    /// <param name="pageNumber">Page number of the sales</param>
    /// <param name="order">Order of the sales</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale if found, Maybe otherwise</returns>
    public async Task<(IEnumerable<Sale>, int)> ListSalesAsync(int pageSize, int pageNumber, string order, bool descending, CancellationToken cancellationToken = default)
    {
        var count = await _context.Sales.CountAsync();
        var sales = await _context.Sales.Skip((pageNumber - 1) * pageSize).Take(pageSize).OrderByProperty(order, descending).ToArrayAsync().ConfigureAwait(false);
        return (sales, count);
    }

    /// <summary>
    /// Check if sale exist by saleNumber
    /// </summary>
    /// <param name="saleNumber">The number of the sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale if exist, false otherwise</returns>
    public async Task<bool> GetSaleExistByNumberAsync(int saleNumber, CancellationToken cancellationToken = default)
    {
        return await _context.Sales.Where(o => o.SaleNumber == saleNumber).AnyAsync().ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves a sale by their unique identifier with ef core tracking
    /// </summary>
    /// <param name="id">The unique identifier of the sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale if found, Maybe otherwise</returns>
    public async Task<Maybe<Sale>> GetByIdWithTrackingAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Sales.Include("Customer").FirstOrDefaultAsync(o => o.Id == id, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes a sale from the database
    /// </summary>
    /// <param name="id">The unique identifier of the sale to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the sale was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sale = await GetByIdAsync(id, cancellationToken);
        if (sale.HasNoValue)
            return false;

        _context.Sales.Remove(sale.Value);
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return true;
    }

    /// <summary>
    /// Updates a sale from the database
    /// </summary>
    /// <param name="id">The unique identifier of the sale to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        _context.Sales.Update(sale);
        _context.Entry(sale).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
