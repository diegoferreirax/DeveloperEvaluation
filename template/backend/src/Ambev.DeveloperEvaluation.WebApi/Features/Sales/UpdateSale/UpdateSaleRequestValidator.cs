using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    public UpdateSaleRequestValidator()
    {
        RuleFor(user => user.SaleDate).NotNull().NotEmpty();
        RuleFor(user => user.TotalAmount).GreaterThan(0);
        RuleFor(user => user.IsCanceled).NotNull();
        RuleFor(user => user.Branch).NotNull().NotEmpty();
        RuleFor(user => user.SaleItens).NotNull().NotEmpty();
    }
}
