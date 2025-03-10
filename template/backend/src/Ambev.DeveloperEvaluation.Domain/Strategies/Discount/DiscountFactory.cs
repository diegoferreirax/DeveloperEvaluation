using CSharpFunctionalExtensions;

namespace Ambev.DeveloperEvaluation.Domain.Strategies.Discount;

public static class DiscountFactory
{
    public static Maybe<IDiscountStrategy> GetDiscountStrategy(int quantity)
    {
        // TODO: ver oq da pra melhorar
        if (quantity >= 4 && quantity <= 9)
        {
            return new LowDiscountStrategy();
        }
        else if (quantity >= 10 && quantity <= 20)
        {
            return new MediumDiscountStrategy();
        }
        return Maybe.None;
    }
}
