using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Command to update an existing sale.
/// </summary>
public class UpdateSaleCommand : IRequest<UpdateSaleResult>
{
    /// <summary>
    /// Unique identifier of the sale.
    /// </summary>
    public Guid Id { get; set; }

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
    public UpdateSaleItemCommand[] SaleItens { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new UpdateSaleCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}

/// <summary>
/// Represents a sale item in the update command.
/// </summary>
public class UpdateSaleItemCommand
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