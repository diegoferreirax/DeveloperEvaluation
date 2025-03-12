using Ambev.DeveloperEvaluation.Domain.Validation;
using CSharpFunctionalExtensions;

namespace Ambev.DeveloperEvaluation.Domain.Strategies.Discount;

public static class DiscountFactory
{
    public static Maybe<IDiscountStrategy> GetDiscountStrategy(int quantity)
    {
        if (IsLowDiscount(quantity))
        {
            return new LowDiscountStrategy();
        }
        else if (IsMediumDiscount(quantity))
        {
            return new MediumDiscountStrategy();
        }
        return Maybe.None;
    }

    private static bool IsLowDiscount(int quantity) 
    {
        return quantity >= 4 && quantity <= 9;
    }

    private static bool IsMediumDiscount(int quantity) 
    {
        return quantity >= 10 && quantity <= 20;
    }
}
