using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Validator class for validating UpdateSaleRequest
/// </summary>
public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    /// <summary>
    /// Defines validation rules for the UpdateSaleRequest
    /// </summary>
    public UpdateSaleRequestValidator()
    {
        RuleFor(user => user.SaleDate).NotNull().NotEmpty();
        RuleFor(user => user.IsCanceled).NotNull();
        RuleFor(user => user.Branch).NotNull().NotEmpty();
        RuleForEach(user => user.SaleItens).SetValidator(new UpdateSaleItemRequestValidator());
    }
}

/// <summary>
/// Validator class for validating UpdateSaleItemRequest.
/// </summary>
public class UpdateSaleItemRequestValidator : AbstractValidator<UpdateSaleItemRequest>
{
    /// <summary>
    /// Defines validation rules for the UpdateSaleItemRequest.
    /// </summary>
    public UpdateSaleItemRequestValidator()
    {
        RuleFor(user => user.ItemId).NotNull().NotEmpty();
        RuleFor(user => user.Quantity).GreaterThan(0);
    }
}
