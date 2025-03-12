namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.v1.ListSaleItems;

/// <summary>
/// Response model for list items of a sale
/// </summary>
/// <param name="Id">The unique identifier of the sale.</param>
public record ListItemsResponse
(
    ListSaleItemsResponse[] Items
);

/// <summary>
/// Represents the response of a list of sale items.
/// Contains information about an item in the sale, including its ID, quantity, discount, 
/// total item amount, and associated product details.
/// </summary>
public record ListSaleItemsResponse
(
    /// <summary>
    /// Gets the unique identifier for the item.
    /// </summary>
    Guid ItemId,

    /// <summary>
    /// Gets the quantity of the item in the sale.
    /// </summary>
    int Quantity,

    /// <summary>
    /// Gets the discount applied to the item.
    /// </summary>
    decimal Discount,

    /// <summary>
    /// Gets the total amount for the item after any discounts are applied.
    /// </summary>
    decimal TotalItemAmount,

    /// <summary>
    /// Gets the details of the item, including its product name and unit price.
    /// </summary>
    ItemResponse Item
);

/// <summary>
/// Represents the details of an item in the sale, including its product name and unit price.
/// </summary>
public record ItemResponse
(
    /// <summary>
    /// Gets the product name associated with the item.
    /// </summary>
    string Product,

    /// <summary>
    /// Gets the unit price of the item.
    /// </summary>
    decimal UnitPrice
);
