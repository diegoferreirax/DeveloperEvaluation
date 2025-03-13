using Ambev.DeveloperEvaluation.Domain.Entities;
using CSharpFunctionalExtensions;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Item entity operations
/// </summary>
public interface IItemRepository
{
    /// <summary>
    /// Retrieves a list of price items by their unique identifier
    /// </summary>
    /// <param name="saleItems">Sale itens to get prices</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale if found, Maybe.None otherwise</returns>
    Task<Maybe<IDictionary<Guid, decimal>>> GetItemsPriceByIdAsync(IEnumerable<Guid> saleItemsIds, CancellationToken cancellationToken = default);
}
