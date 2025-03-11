using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.v1.UpdateSale;

/// <summary>
/// Validator for the UpdateSaleCommand.
/// Ensures that all required fields are properly filled and meet the validation criteria.
/// </summary>
public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of theUpdateSaleCommandValidator.
    /// 
    /// Validation rules:
    /// - SaleDate must not be null or empty.
    /// - TotalAmount must be greater than zero.
    /// - IsCanceled must not be null.
    /// - Branch must not be null or empty.
    /// - SaleItens must not be null or empty.
    /// </summary>
    public UpdateSaleCommandValidator()
    {
        RuleFor(user => user.SaleDate).NotNull().NotEmpty();
        RuleFor(user => user.IsCanceled).NotNull();
        RuleFor(user => user.Branch).NotNull().NotEmpty();
        RuleForEach(user => user.SaleItens).SetValidator(new UpdateSaleItemCommandValidator());
    }
}


/// <summary>
/// Validator for the UpdateSaleItemCommand.
/// Ensures that all required fields are properly filled and meet the validation criteria.
/// </summary>
public class UpdateSaleItemCommandValidator : AbstractValidator<UpdateSaleItemCommand>
{
    /// <summary>
    /// Initializes a new instance of the UpdateSaleItemCommandValidator.
    /// 
    /// Validation rules:
    /// - ItemId must not be null or empty.
    /// - Quantity must be greater than zero.
    /// </summary>
    public UpdateSaleItemCommandValidator()
    {
        RuleFor(user => user.ItemId).NotNull().NotEmpty();
        RuleFor(user => user.Quantity).GreaterThan(0);
        RuleFor(user => user.Discount).GreaterThanOrEqualTo(0);
    }
}
