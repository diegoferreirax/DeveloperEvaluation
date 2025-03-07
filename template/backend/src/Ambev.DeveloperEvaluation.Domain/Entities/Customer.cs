using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/*
 TODO: 
    botar comentarios
    contrutores privados
    validações
 */
public class Customer : BaseEntity
{
    public string Name { get; set; }
}