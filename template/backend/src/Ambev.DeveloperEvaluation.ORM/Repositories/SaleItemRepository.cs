﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of ISaleItemRepository using Entity Framework Core
/// </summary>
public class SaleItemRepository : ISaleItemRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of SaleItemRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public SaleItemRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Register a saleItens in the database
    /// </summary>
    /// <param name="saleItens">The saleItens to register</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The registered saleItens</returns>
    public async Task RegisterSaleItensAsync(IEnumerable<SaleItem> saleItens, CancellationToken cancellationToken = default)
    {
        await _context.SaleItens.AddRangeAsync(saleItens, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Updates a saleItem from the database
    /// </summary>
    /// <param name="id">The unique identifier of the saleItem to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task UpdateAsync(IEnumerable<SaleItem> saleItens, CancellationToken cancellationToken = default)
    {
        _context.SaleItens.UpdateRange(saleItens);
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes a saleItens from the database
    /// </summary>
    /// <param name="id">The unique identifier of the saleItens to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the saleItens was deleted, false if not found</returns>
    public async Task DeleteAsync(IEnumerable<SaleItem> saleItems, CancellationToken cancellationToken = default)
    {
        _context.SaleItens.RemoveRange(saleItems);
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves a saleItens by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the saleItens</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The saleItens if found, empty list otherwise</returns>
    public async Task<IEnumerable<SaleItem>> GetBySaleIdAsync(Guid saleId, CancellationToken cancellationToken = default)
    {
        return await _context.SaleItens.Include("Item").AsNoTracking().Where(s => s.SaleId == saleId).ToArrayAsync().ConfigureAwait(false);
    }
}
