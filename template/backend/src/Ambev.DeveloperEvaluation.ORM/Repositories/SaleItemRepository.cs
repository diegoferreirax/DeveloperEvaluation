using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

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
    /// Register a new saleItens in the database
    /// </summary>
    /// <param name="saleItens">The saleItens to register</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The registered saleItens</returns>
    public async Task<SaleItem[]> RegisterSaleItensAsync(SaleItem[] saleItens, CancellationToken cancellationToken = default)
    {
        await _context.SaleItens.AddRangeAsync(saleItens, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return saleItens;
    }
}
