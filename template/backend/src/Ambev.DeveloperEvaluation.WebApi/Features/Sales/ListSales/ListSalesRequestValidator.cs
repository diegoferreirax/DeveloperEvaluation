using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;

public class ListSalesRequestValidator : AbstractValidator<ListSalesRequest>
{
    /// <summary>
    /// Defines validation rules for the ListSalesRequest
    /// </summary>
    public ListSalesRequestValidator()
    {
        RuleFor(user => user.Page).GreaterThanOrEqualTo(1);
        RuleFor(user => user.Size).GreaterThanOrEqualTo(10);
        RuleFor(user => user.Order).NotNull().NotEmpty();
    }
}
