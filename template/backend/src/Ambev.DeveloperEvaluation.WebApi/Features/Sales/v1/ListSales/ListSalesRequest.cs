namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.v1.ListSales;

/// <summary>
/// Represents a request to retrieve a list of sales, including pagination and ordering options.
/// </summary>
public class ListSalesRequest
{
    /// <summary>
    /// Gets or sets the page number for pagination.
    /// The default value is 1.
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Gets or sets the number of sales to be retrieved per page.
    /// The default value is 10.
    /// </summary>
    public int Size { get; set; } = 10;

    /// <summary>
    /// Gets or sets the field by which the sales should be ordered.
    /// The default value is "SaleNumber".
    /// </summary>
    public string Order { get; set; } = "SaleNumber";

    /// <summary>
    /// Indicates whether the sorting should be in descending order.
    /// </summary>
    /// <value>
    /// <c>true</c> if the sorting is descending; otherwise, <c>false</c>.
    /// </value>
    public bool Descending { get; set; } = false;
}
