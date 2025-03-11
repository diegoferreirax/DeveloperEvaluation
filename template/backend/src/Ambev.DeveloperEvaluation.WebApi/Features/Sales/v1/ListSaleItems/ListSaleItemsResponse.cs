namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.v1.ListSaleItems;

/// <summary>
/// Response model for list items of a sale
/// </summary>
/// <param name="Id">The unique identifier of the sale.</param>
public record ListItemsResponse
(
    ListSaleItemsResponse[] Items
);

public record ListSaleItemsResponse
(
    Guid ItemId,
    int Quantity,
    decimal Discount,
    decimal TotalItemAmount,
    ItemResponse Item
);

public record ItemResponse
(
    string Product,
    decimal UnitPrice
);
