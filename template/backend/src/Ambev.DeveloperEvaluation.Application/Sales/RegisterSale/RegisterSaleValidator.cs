using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.RegisterSale;

public class RegisterSaleCommandValidator : AbstractValidator<RegisterSaleCommand>
{
    public RegisterSaleCommandValidator()
    {
        RuleFor(user => user.SaleNumber).GreaterThan(0);
    }
}
