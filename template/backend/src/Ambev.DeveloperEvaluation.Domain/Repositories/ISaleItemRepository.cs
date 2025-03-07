using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for SaleItem entity operations
/// </summary>
public interface ISaleItemRepository
{
    /// <summary>
    /// Register a new saleItem in the repository
    /// </summary>
    /// <param name="saleItem">The saleItem to register</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The registered saleItem</returns>
    Task<SaleItem[]> RegisterSaleItensAsync(SaleItem[] saleItens, CancellationToken cancellationToken = default);
}
