using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.RegisterSale;

/// <summary>
/// Profile for mapping between Application and API RegisterSale responses
/// </summary>
public class RegisterSaleProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the RegisterSaleProfile.
    /// 
    /// Mapping configurations:
    /// - Maps RegisterSaleCommand to Sale.
    /// - Maps RegisterSaleItemCommand to SaleItem.
    /// - Maps Guid to RegisterSaleResult.
    /// </summary>
    public RegisterSaleProfile()
    {
        CreateMap<RegisterSaleCommand, Sale>();
        CreateMap<RegisterSaleItemCommand, SaleItem>();
        CreateMap<Guid, RegisterSaleResult>().ConstructUsing(id => new RegisterSaleResult(id));
    }
}
