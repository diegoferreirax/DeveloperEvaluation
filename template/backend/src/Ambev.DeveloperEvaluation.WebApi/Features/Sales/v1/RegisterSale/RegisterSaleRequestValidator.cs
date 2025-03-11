using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.v1.RegisterSale;

/// <summary>
/// Validator class for validating RegisterSaleRequest.
/// </summary>
public class RegisterSaleRequestValidator : AbstractValidator<RegisterSaleRequest>
{
    /// <summary>
    /// Defines validation rules for the RegisterSaleRequest.
    /// </summary>
    public RegisterSaleRequestValidator()
    {
        RuleFor(user => user.CustomerId).NotNull().NotEmpty();
        RuleFor(user => user.SaleNumber).GreaterThan(0);
        RuleFor(user => user.SaleDate).NotNull().NotEmpty();
        RuleFor(user => user.IsCanceled).NotNull();
        RuleFor(user => user.Branch).NotNull().NotEmpty();
        RuleForEach(user => user.SaleItens).SetValidator(new RegisterSaleItemRequestValidator());
    }
}

/// <summary>
/// Validator class for validating RegisterSaleItemRequest.
/// </summary>
public class RegisterSaleItemRequestValidator : AbstractValidator<RegisterSaleItemRequest>
{
    /// <summary>
    /// Defines validation rules for the RegisterSaleItemRequest.
    /// </summary>
    public RegisterSaleItemRequestValidator()
    {
        RuleFor(user => user.ItemId).NotNull().NotEmpty();
        RuleFor(user => user.Quantity).GreaterThan(0);
    }
}
