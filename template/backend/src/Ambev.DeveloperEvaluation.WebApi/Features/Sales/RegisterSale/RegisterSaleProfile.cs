using Ambev.DeveloperEvaluation.Application.Sales.RegisterSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.RegisterSale;

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
        CreateMap<RegisterSaleItemRequest, RegisterSaleItemCommand>();
        CreateMap<RegisterSaleResult, RegisterSaleResponse>();
    }
}
