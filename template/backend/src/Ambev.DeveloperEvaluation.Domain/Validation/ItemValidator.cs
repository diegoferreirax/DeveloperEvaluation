using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class ItemValidator : AbstractValidator<Item>
{
    public static readonly int MaxLimitQuantityByItem = 20;

    public ItemValidator()
    {
        RuleFor(user => user.Product).NotNull().NotEmpty();
        RuleFor(user => user.UnitPrice).GreaterThan(0);
    }
}
