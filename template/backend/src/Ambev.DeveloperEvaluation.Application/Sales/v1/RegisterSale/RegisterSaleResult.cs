namespace Ambev.DeveloperEvaluation.Application.Sales.v1.RegisterSale;

/// <summary>
/// Represents the result of a sale registration process.
/// </summary>
/// <param name="id">The unique identifier of the newly registered sale.</param>
public record RegisterSaleResult(Guid id);