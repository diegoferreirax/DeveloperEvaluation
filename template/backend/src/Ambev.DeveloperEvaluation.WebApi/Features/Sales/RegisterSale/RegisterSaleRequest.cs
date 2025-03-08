namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.RegisterSale;

/// <summary>
/// Represents a request to register a new sale in the system.
/// </summary>
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