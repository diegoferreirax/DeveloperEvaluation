using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.RegisterSale;

public class RegisterSaleProfile : Profile
{
    public RegisterSaleProfile()
    {
        CreateMap<RegisterSaleCommand, Sale>();
        CreateMap<RegisterSaleItemCommand, SaleItem>();
        CreateMap<Sale, RegisterSaleResult>();
    }
}
