using Ambev.DeveloperEvaluation.Domain.Entities;
using CSharpFunctionalExtensions;

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
    Task RegisterSaleItensAsync(SaleItem[] saleItens, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a saleItens by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the saleItens</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The saleItens if found, empty list otherwise</returns>
    Task<SaleItem[]> GetBySaleIdAsync(Guid saleId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a saleItens from the database
    /// </summary>
    /// <param name="id">The unique identifier of the saleItens to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task DeleteAsync(SaleItem[] saleItems, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a saleItem from the database
    /// </summary>
    /// <param name="id">The unique identifier of the saleItem to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task UpdateAsync(SaleItem[] saleItens, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a saleItens by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the saleItens</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The saleItens if found, empty list otherwise</returns>
    Task<SaleItem[]> GetBySaleWithItemIdAsync(Guid saleId, CancellationToken cancellationToken = default);
}
