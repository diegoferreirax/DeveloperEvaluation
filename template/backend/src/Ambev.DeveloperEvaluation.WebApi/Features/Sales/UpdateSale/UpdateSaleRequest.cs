namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Represents a request to update a sale in the system.
/// </summary>
/// <param name="Id">The unique identifier of the sale to be updated.</param>
/// <param name="SaleDate">The date when the sale occurred.</param>
/// <param name="TotalAmount">The total amount for the sale.</param>
/// <param name="IsCanceled">Indicates whether the sale has been canceled.</param>
/// <param name="Branch">The branch where the sale took place.</param>
/// <param name="SaleItens">An array of sale items associated with the sale, including their details (item ID, quantity, discount).</param>
public record UpdateSaleRequest
(
    Guid Id,
    DateTime SaleDate,
    decimal TotalAmount,
    bool IsCanceled,
    string Branch,
    UpdateSaleItemRequest[] SaleItens
);

// TODO: validar saleitem

/// <summary>
/// Represents a sale item that is part of an update sale request, including item ID, quantity, and discount details.
/// </summary>
/// <param name="itemId">The unique identifier of the item being sold.</param>
/// <param name="quantity">The quantity of the item being sold.</param>
/// <param name="discount">The discount applied to the item, if any.</param>
public record UpdateSaleItemRequest
(
    Guid itemId,
    int quantity,
    decimal discount
);
