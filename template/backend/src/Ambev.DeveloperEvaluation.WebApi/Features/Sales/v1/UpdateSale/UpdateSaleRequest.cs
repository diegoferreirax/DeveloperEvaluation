namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.v1.UpdateSale;

/// <summary>
/// Represents a request to update a sale in the system.
/// </summary>
/// <param name="Id">The unique identifier of the sale to be updated.</param>
/// <param name="SaleDate">The date when the sale occurred.</param>
/// <param name="IsCanceled">Indicates whether the sale has been canceled.</param>
/// <param name="Branch">The branch where the sale took place.</param>
/// <param name="SaleItens">An array of sale items associated with the sale, including their details (item ID, quantity, discount).</param>
public record UpdateSaleRequest
(
    Guid Id,
    DateTime SaleDate,
    bool IsCanceled,
    string Branch,
    IEnumerable<UpdateSaleItemRequest> SaleItens
);

/// <summary>
/// Represents a sale item that is part of an update sale request, including item ID, quantity, and discount details.
/// </summary>
/// <param name="ItemId">The unique identifier of the item being sold.</param>
/// <param name="Quantity">The quantity of the item being sold.</param>
public record UpdateSaleItemRequest
(
    Guid ItemId,
    int Quantity
);
