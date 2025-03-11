namespace Ambev.DeveloperEvaluation.Application.Sales.v1.ListSaleItems;

/// <summary>
/// Result model for list items of a sale
/// </summary>
/// <param name="Id">The unique identifier of the sale.</param>
public record ListItemsResult
(
    ListSaleItemsResult[] Items
);

public record ListSaleItemsResult
(
    Guid ItemId,
    int Quantity,
    decimal Discount,
    decimal TotalItemAmount,
    ItemResult Item
);

public record ItemResult
(
    string Product,
    decimal UnitPrice
);
