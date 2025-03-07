using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/*
 TODO: 
    botar comentarios
    contrutores privados
    validações
 */
public class Item : BaseEntity
{
    public string Product { get; set; }
    public decimal UnitPrice { get; set; }
}