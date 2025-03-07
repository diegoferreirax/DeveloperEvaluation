using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/*
 TODO: 
    botar comentarios
    contrutores privados
    validações
 */
public class SaleItem : BaseEntity
{
    public Guid SaleId { get; set; }
    public Sale Sale { get; set; }

    public Guid ItemId { get; set; }
    public Item Item { get; set; }

    public int Quantity { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalItemAmount => Item.UnitPrice * Quantity - Discount;
}