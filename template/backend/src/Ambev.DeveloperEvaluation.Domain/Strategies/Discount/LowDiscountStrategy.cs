namespace Ambev.DeveloperEvaluation.Domain.Strategies.Discount;

public class LowDiscountStrategy : IDiscountStrategy
{
    public static readonly decimal LowDiscount = 0.10m;

    public decimal GetDiscount(decimal price, int quantity) => price - (price * LowDiscount);

    public decimal GetPercent()
    {
        return LowDiscount;
    }
}
