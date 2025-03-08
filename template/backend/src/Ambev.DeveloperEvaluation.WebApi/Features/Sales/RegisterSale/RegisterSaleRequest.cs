namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.RegisterSale;

/// <summary>
/// Represents a request to register a new sale in the system.
/// </summary>
/// <param name="CustomerId">The unique identifier of the customer making the sale.</param>
/// <param name="SaleNumber">The number assigned to the sale for tracking purposes.</param>
/// <param name="SaleDate">The date when the sale took place.</param>
/// <param name="TotalAmount">The total amount for the sale, including all items and discounts.</param>
/// <param name="IsCanceled">Indicates whether the sale is canceled.</param>
/// <param name="Branch">The branch where the sale occurred.</param>
/// <param name="SaleItens">An array of sale items included in the sale, detailing each item and its properties (quantity, discount).</param>
public record RegisterSaleRequest
(
    Guid CustomerId,
    int SaleNumber,
    DateTime SaleDate,
    decimal TotalAmount,
    bool IsCanceled,
    string Branch,
    RegisterSaleItemRequest[] SaleItens
);

// TODO: validar saleitem

/// <summary>
/// Represents a request to register a sale item, including item details, quantity, and discount.
/// </summary>
/// <param name="itemId">The unique identifier of the item being registered for the sale.</param>
/// <param name="quantity">The quantity of the item being sold.</param>
/// <param name="discount">The discount applied to the item, if any.</param>
public record RegisterSaleItemRequest(Guid itemId, int quantity, decimal discount);