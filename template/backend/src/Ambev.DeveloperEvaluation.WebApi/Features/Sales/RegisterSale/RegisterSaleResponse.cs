namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.RegisterSale;

/// <summary>
/// Represents the response returned after successfully registering a sale.
/// </summary>
/// <param name="id">The unique identifier of the newly registered sale.</param>
public record RegisterSaleResponse(Guid id);
