using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.v1.ListSales;

/// <summary>
/// Command for list sales
/// </summary>
public class ListSalesCommandValidator : AbstractValidator<ListSalesCommand>
{
    /// <summary>
    /// Initializes validation rules for ListSalesCommand
    /// </summary>
    public ListSalesCommandValidator()
    {
        RuleFor(user => user.Page).GreaterThanOrEqualTo(1);
        RuleFor(user => user.Size).GreaterThanOrEqualTo(10);
        RuleFor(user => user.Order).NotNull().NotEmpty();
    }
}
