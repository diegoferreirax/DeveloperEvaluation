using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.RegisterSale;

public class RegisterSaleCommandValidator : AbstractValidator<RegisterSaleCommand>
{
    public RegisterSaleCommandValidator()
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
