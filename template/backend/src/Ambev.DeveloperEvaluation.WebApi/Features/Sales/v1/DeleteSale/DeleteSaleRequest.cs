namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.v1.DeleteSale;

/// <summary>
/// Request model for deleting a sale
/// </summary>
/// <param name="Id">The unique identifier of the sale to be deleted.</param>
public record DeleteSaleRequest(Guid Id);
