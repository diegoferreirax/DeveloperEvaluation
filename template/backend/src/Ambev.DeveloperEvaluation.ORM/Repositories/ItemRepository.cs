using Ambev.DeveloperEvaluation.Domain.Repositories;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IItemRepository using Entity Framework Core
/// </summary>
public class ItemRepository : IItemRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of ItemRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public ItemRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves a list of price items by their unique identifier
    /// </summary>
    /// <param name="saleItems">Sale itens to get prices</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The dictionary of Ids and prices if found, Maybe.None otherwise</returns>
    public async Task<Maybe<IDictionary<Guid, decimal>>> GetItemsPriceByIdAsync(Guid[] saleItemsIds, CancellationToken cancellationToken = default)
    {
        // TODO: ver como ficou a consulta internamente
        return await _context.Items.Where(i => saleItemsIds.Contains(i.Id)).ToDictionaryAsync(i => i.Id, i => i.UnitPrice).ConfigureAwait(false);
    }
}
