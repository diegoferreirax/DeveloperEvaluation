namespace Ambev.DeveloperEvaluation.Domain.Strategies.Discount;

public interface IDiscountStrategy
{
    decimal GetPercent();
    decimal GetDiscount(decimal price, int quantity);
}
