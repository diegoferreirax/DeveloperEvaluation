using Ambev.DeveloperEvaluation.Domain.Entities;
using CSharpFunctionalExtensions;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Sale entity operations
/// </summary>
public interface ISaleRepository
{
    /// <summary>
    /// Register a new sale in the repository
    /// </summary>
    /// <param name="sale">The sale to register</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The registered sale Id</returns>
    Task<Guid> RegisterSaleAsync(Sale sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a sale by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale if found, Maybe.None otherwise</returns>
    Task<Maybe<Sale>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Check if sale exist by saleNumber
    /// </summary>
    /// <param name="saleNumber">The number of the sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale if exist, false otherwise</returns>
    Task<bool> GetSaleExistByNumberAsync(int saleNumber, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a sale from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the sale to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the sale was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a sale by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a sale by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale if found, Maybe otherwise</returns>
    Task<Maybe<Sale>> GetByIdWithTrackingAsync(Guid id, CancellationToken cancellationToken = default);
}
