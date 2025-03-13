namespace Ambev.DeveloperEvaluation.Application.Sales.v1.ListSaleItems;

/// <summary>
/// Represents the result of a list of items in a sale. 
/// Contains an array of <see cref="ListSaleItemsResult"/> objects, each representing details of a sale item.
/// </summary>
public record ListItemsResult
(
    IEnumerable<ListSaleItemsResult> Items
);

/// <summary>
/// Represents the result of a list of sale items.
/// Contains information about an item in the sale, including its ID, quantity, discount, 
/// total item amount, and associated product details.
/// </summary>
public record ListSaleItemsResult
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
    ItemResult Item
);

/// <summary>
/// Represents the details of an item in the sale, including its product name and unit price.
/// </summary>
public record ItemResult
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
