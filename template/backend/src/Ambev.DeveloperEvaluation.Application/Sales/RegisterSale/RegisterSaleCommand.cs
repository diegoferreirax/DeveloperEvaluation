using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.RegisterSale;

public class RegisterSaleCommand : IRequest<RegisterSaleResult>
{
    public Guid CustomerId { get; set; }
    public int SaleNumber { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsCanceled { get; set; }
    public string Branch { get; set; }

    public RegisterSaleItemCommand[] SaleItens { get; set; }

    //public ValidationResultDetail Validate()
    //{
    //    var validator = new CreateUserCommandValidator();
    //    var result = validator.Validate(this);
    //    return new ValidationResultDetail
    //    {
    //        IsValid = result.IsValid,
    //        Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
    //    };
    //}
}

public class RegisterSaleItemCommand
{
    public Guid ItemId { get; set; }
    public int Quantity { get; set; }
    public decimal Discount { get; set; }
}