namespace Ambev.DeveloperEvaluation.Domain.Strategies.Discount;

public class MediumDiscountStrategy : IDiscountStrategy
{
    public static readonly decimal MediumDiscount = 0.20m;

    public decimal GetDiscount(decimal price, int quantity) => price - (price * MediumDiscount);

    public decimal GetPercent()
    {
        return MediumDiscount;
    }
}
