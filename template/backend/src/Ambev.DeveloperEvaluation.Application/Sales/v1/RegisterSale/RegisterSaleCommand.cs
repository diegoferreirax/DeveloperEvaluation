using Ambev.DeveloperEvaluation.Application.Sales.v1.RegisterSale;
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.v1.RegisterSale;

/// <summary>
/// Command to register a new sale.
/// </summary>
public class RegisterSaleCommand : IRequest<RegisterSaleResult>
{
    /// <summary>
    /// Unique identifier of the customer.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Sale number.
    /// </summary>
    public int SaleNumber { get; set; }

    /// <summary>
    /// Sale date.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Indicates whether the sale has been canceled.
    /// </summary>
    public bool IsCanceled { get; set; }

    /// <summary>
    /// Name of the branch where the sale was made.
    /// </summary>
    public string Branch { get; set; }

    /// <summary>
    /// List of sale items.
    /// </summary>
    public IEnumerable<RegisterSaleItemCommand> SaleItens { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new RegisterSaleCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}

/// <summary>
/// Represents a sale item in the registration command.
/// </summary>
public class RegisterSaleItemCommand
{
    /// <summary>
    /// Unique identifier of the item.
    /// </summary>
    public Guid ItemId { get; set; }

    /// <summary>
    /// Quantity of the item in the sale.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Discount amount applied to the item.
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Gets sale's total item amount.
    /// </summary>
    public decimal TotalItemAmount { get; private set; }

    public void SetTotalItemAmount(decimal totalItemAmount)
    {
        TotalItemAmount = totalItemAmount;
    }
}