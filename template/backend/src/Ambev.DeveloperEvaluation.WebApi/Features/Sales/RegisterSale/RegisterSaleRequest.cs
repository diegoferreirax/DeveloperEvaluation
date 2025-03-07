using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.RegisterSale;

/// <summary>
/// Represents a request to register a new sale in the system.
/// </summary>
public class RegisterSaleRequest
{
    public Guid CustomerId { get; set; }
    public int SaleNumber { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsCanceled { get; set; }
    public string Branch { get; set; }

    public RegisterSaleItemRequest[] SaleItens { get; set; }
}

public class RegisterSaleItemRequest
{
    public Guid ItemId { get; set; }
    public int Quantity { get; set; }
    public decimal Discount { get; set; }
}