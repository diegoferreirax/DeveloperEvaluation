using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/*
 TODO: 
    contrutores privados
 */

/// <summary>
/// Represents a sale in the system with some informations and customer.
/// </summary>
public class Sale : BaseEntity
{
    /// <summary>
    /// Gets the sale's customerId.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets the sale's customer object.
    /// </summary>
    public Customer Customer { get; set; }

    /// <summary>
    /// Gets the sale's number.
    /// SaleNumber is unique for each sale.
    /// </summary>
    public int SaleNumber { get; set; }

    /// <summary>
    /// Gets the sale's date.
    /// Sale creation date.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Gets the total amount of sale.
    /// Must be gran than zero.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets canceled sale's status.
    /// </summary>
    public bool IsCanceled { get; set; }

    // TODO: poderia criar uma classe para filial
    public string Branch { get; set; }

    // TODO: aplicar lista de saleItem sem redundância
    // public string SaleItem[] { get; set; }

    /// <summary>
    /// Performs validation of the sale entity using the SaleValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">CustomerId validity</list>
    /// <list type="bullet">SaleNumber number validade</list>
    /// <list type="bullet">SaleDate validity</list>
    /// <list type="bullet">TotalAmount greater than</list>
    /// <list type="bullet">IsCanceled validity</list>
    /// <list type="bullet">Branch validity</list>
    /// <list type="bullet">Customer validity</list>
    /// </remarks>
    public ValidationResultDetail Validate()
    {
        var validator = new SaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}