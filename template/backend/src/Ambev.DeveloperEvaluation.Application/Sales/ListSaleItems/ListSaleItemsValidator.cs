using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSaleItems;

/// <summary>
/// Validator for ListSaleItemsCommand
/// </summary>
public class ListSaleItemsCommandValidator : AbstractValidator<ListSaleItemsCommand>
{
    /// <summary>
    /// Initializes validation rules for ListSaleItemsCommand
    /// </summary>
    public ListSaleItemsCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale ID is required");
    }
}
