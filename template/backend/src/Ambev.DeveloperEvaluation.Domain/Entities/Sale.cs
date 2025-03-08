using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/*
 TODO: 
    botar comentarios
    contrutores privados
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