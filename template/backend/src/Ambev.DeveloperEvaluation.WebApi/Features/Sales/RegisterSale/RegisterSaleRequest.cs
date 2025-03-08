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
public record RegisterSaleItemRequest(Guid itemId, int quantity, decimal discount);