using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.v1.ListSales;

/// <summary>
/// Represents a command to list sales, including pagination, ordering, and validation logic.
/// Implements the <see cref="IRequest{ListSalesResult}"/> interface for use with a request-response pattern.
/// </summary>
public class ListSalesCommand : IRequest<ListSalesResult>
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

    public ValidationResultDetail Validate()
    {
        var validator = new ListSalesCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
