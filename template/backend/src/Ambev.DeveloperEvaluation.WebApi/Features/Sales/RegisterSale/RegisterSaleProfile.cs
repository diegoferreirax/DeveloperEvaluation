using Ambev.DeveloperEvaluation.Application.Sales.RegisterSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.RegisterSale;

public class RegisterSaleProfile : Profile
{
    public RegisterSaleProfile()
    {
        CreateMap<RegisterSaleRequest, RegisterSaleCommand>();
        CreateMap<RegisterSaleItemRequest, RegisterSaleItemCommand>();
        CreateMap<RegisterSaleResult, RegisterSaleResponse>();
    }
}
