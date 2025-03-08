using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/*
 TODO: 
    contrutores privados
    validações
 */

/// <summary>
/// Represents a sale item in the system with informations and relationships.
/// </summary>
public class SaleItem : BaseEntity
{
    /// <summary>
    /// Gets the id of the related sale.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Gets the related sale.
    /// </summary>
    public Sale Sale { get; set; }

    /// <summary>
    /// Gets the id of the related item.
    /// </summary>
    public Guid ItemId { get; set; }

    /// <summary>
    /// Gets the id related item.
    /// </summary>
    public Item Item { get; set; }

    /// <summary>
    /// Gets sale's item quantity.
    /// Must by gran than 0.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets sale's item discount.
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Gets sale's total item amount.
    /// </summary>
    public decimal TotalItemAmount => Item.UnitPrice * Quantity - Discount;
}