namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Represents a request to update a sale in the system.
/// </summary>
public class UpdateSaleRequest
{
    public Guid Id { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsCanceled { get; set; }
    public string Branch { get; set; }

    public UpdateSaleItemRequest[] SaleItens { get; set; }
}

// TODO: validar saleitem
public class UpdateSaleItemRequest
{
    public Guid ItemId { get; set; }
    public int Quantity { get; set; }
    public decimal Discount { get; set; }
}