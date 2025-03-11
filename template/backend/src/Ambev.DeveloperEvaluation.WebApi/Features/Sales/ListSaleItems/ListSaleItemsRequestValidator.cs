using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSaleItems;

/// <summary>
/// Validator for ListSaleItemsRequest
/// </summary>
public class ListSaleItemsRequestValidator : AbstractValidator<ListSaleItemsRequest>
{
    /// <summary>
    /// Initializes validation rules for ListSaleItemsRequest
    /// </summary>
    public ListSaleItemsRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale ID is required");
    }
}
