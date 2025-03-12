namespace Ambev.DeveloperEvaluation.Domain.Services;

public static class CalculationService
{
    public static decimal CalculateTotalPrice(decimal price, int quantity)
    {
        return price * quantity;
    }
}
