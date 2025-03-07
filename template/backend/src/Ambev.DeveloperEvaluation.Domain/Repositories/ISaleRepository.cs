using Ambev.DeveloperEvaluation.Domain.Entities;

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
    /// <returns>The registered sale</returns>
    Task<Sale> RegisterSaleAsync(Sale sale, CancellationToken cancellationToken = default);
}
