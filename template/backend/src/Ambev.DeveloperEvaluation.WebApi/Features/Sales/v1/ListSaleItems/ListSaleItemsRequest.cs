namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.v1.ListSaleItems;

/// <summary>
/// Request model for list items of a sale
/// </summary>
/// <param name="Id">The unique identifier of the sale.</param>
public record ListSaleItemsRequest(Guid Id);
