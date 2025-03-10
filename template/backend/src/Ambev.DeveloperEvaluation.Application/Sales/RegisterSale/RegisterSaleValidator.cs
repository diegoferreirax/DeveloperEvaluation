using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.RegisterSale;

/// <summary>
/// Validator for the RegisterSaleCommand.
/// Ensures that all required fields are properly filled and meet the validation criteria.
/// </summary>
public class RegisterSaleCommandValidator : AbstractValidator<RegisterSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the RegisterSaleCommandValidator.
    /// 
    /// Validation rules:
    /// - CustomerId must not be null or empty.
    /// - SaleNumber must be greater than zero.
    /// - SaleDate must not be null or empty.
    /// - TotalAmount must be greater than zero.
    /// - IsCanceled must not be null.
    /// - Branch must not be null or empty.
    /// - SaleItens must not be null or empty.
    /// </summary>
    public RegisterSaleCommandValidator()
    {
        RuleFor(user => user.CustomerId).NotNull().NotEmpty();
        RuleFor(user => user.SaleNumber).GreaterThan(0);
        RuleFor(user => user.SaleDate).NotNull().NotEmpty();
        RuleFor(user => user.IsCanceled).NotNull();
        RuleFor(user => user.Branch).NotNull().NotEmpty();
        RuleForEach(user => user.SaleItens).SetValidator(new RegisterSaleItemCommandValidator());
    }
}

/// <summary>
/// Validator for the RegisterSaleItemCommand.
/// Ensures that all required fields are properly filled and meet the validation criteria.
/// </summary>
public class RegisterSaleItemCommandValidator : AbstractValidator<RegisterSaleItemCommand>
{
    /// <summary>
    /// Initializes a new instance of the RegisterSaleItemCommandValidator.
    /// 
    /// Validation rules:
    /// - ItemId must not be null or empty.
    /// - Quantity must be greater than zero.
    /// </summary>
    public RegisterSaleItemCommandValidator()
    {
        RuleFor(user => user.ItemId).NotNull().NotEmpty();
        RuleFor(user => user.Quantity).GreaterThan(0);
        RuleFor(user => user.Discount).GreaterThanOrEqualTo(0);
    }
}
