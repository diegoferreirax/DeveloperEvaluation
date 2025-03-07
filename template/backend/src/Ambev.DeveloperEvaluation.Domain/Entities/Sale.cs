using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/*
 TODO: 
    botar comentarios
    contrutores privados
    validações
    pensar se cria uma tabela pra filial
 */
public class Sale : BaseEntity
{
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }

    public int SaleNumber { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsCanceled { get; set; }
    public string Branch { get; set; }
}