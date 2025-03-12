using Ambev.DeveloperEvaluation.Domain.Services;
using CSharpFunctionalExtensions;

namespace Ambev.DeveloperEvaluation.Domain.Strategies.Discount;

public static class DiscountFactory
{
    public static (decimal, decimal) GetDiscountAndTotalPriceItem(decimal price, int quantity)
    {
        var discountStrategy = GetDiscountStrategy(quantity);
        if (discountStrategy.HasValue)
        {
            var discountPercent = discountStrategy.Value.GetPercent();

            var discountedPrice = discountStrategy.Value.GetDiscount(price, quantity);
            var totalPriceWithDiscount = CalculationService.CalculateTotalPrice(discountedPrice, quantity);

            return (discountPercent, totalPriceWithDiscount);
        }
        else
        {
            var totalPrice = CalculationService.CalculateTotalPrice(price, quantity);
            return (0, totalPrice);
        }
    }

    private static Maybe<IDiscountStrategy> GetDiscountStrategy(int quantity)
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
