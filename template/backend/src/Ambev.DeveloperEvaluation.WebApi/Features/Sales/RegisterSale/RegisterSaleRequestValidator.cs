using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.RegisterSale;

public class RegisterSaleRequestValidator : AbstractValidator<RegisterSaleRequest>
{
    public RegisterSaleRequestValidator()
    {
        //TODO: fazer mais validações
        RuleFor(user => user.SaleNumber).GreaterThan(0);
    }
}
