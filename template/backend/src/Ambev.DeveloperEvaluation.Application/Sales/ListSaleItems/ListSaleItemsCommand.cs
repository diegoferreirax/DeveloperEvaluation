using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSaleItems;

public class ListSaleItemsCommand : IRequest<ListItemsResult>
{
    /// <summary>
    /// The unique identifier of the sale
    /// </summary>
    public Guid Id { get; set; }
}
