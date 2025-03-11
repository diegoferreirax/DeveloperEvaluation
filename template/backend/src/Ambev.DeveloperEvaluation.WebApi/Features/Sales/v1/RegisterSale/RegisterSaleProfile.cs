using Ambev.DeveloperEvaluation.Application.Sales.v1.RegisterSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.v1.RegisterSale;

/// <summary>
/// Profile for mapping between Application and API RegisterSale responses
/// </summary>
public class RegisterSaleProfile : Profile
{
    /// <summary>
    /// Configures object mappings for sale-related request, command, and response objects.
    /// </summary>
    public RegisterSaleProfile()
    {
        CreateMap<RegisterSaleRequest, RegisterSaleCommand>();

        CreateMap<RegisterSaleItemRequest, RegisterSaleItemCommand>()
            .ForMember(com => com.TotalItemAmount, req => req.Ignore())
            .ForMember(com => com.Discount, req => req.Ignore());

        CreateMap<RegisterSaleResult, RegisterSaleResponse>();
    }
}
