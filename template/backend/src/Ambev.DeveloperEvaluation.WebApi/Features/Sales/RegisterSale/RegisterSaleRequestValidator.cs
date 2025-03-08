using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.RegisterSale;

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
        RuleFor(user => user.TotalAmount).GreaterThan(0);
        RuleFor(user => user.IsCanceled).NotNull();
        RuleFor(user => user.Branch).NotNull().NotEmpty();
        RuleFor(user => user.SaleItens).NotNull().NotEmpty();
    }
}
