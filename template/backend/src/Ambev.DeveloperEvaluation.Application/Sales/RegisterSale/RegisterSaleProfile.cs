using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.RegisterSale;

/// <summary>
/// AutoMapper for objects related to registering a sale.
/// </summary>
public class RegisterSaleProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the RegisterSaleProfile.
    /// 
    /// Mapping configurations:
    /// - Maps RegisterSaleCommand to Sale.
    /// - Maps RegisterSaleItemCommand to SaleItem.
    /// - Maps Sale to RegisterSaleResult.
    /// </summary>
    public RegisterSaleProfile()
    {
        CreateMap<RegisterSaleCommand, Sale>();
        CreateMap<RegisterSaleItemCommand, SaleItem>();
        CreateMap<Sale, RegisterSaleResult>();
    }
}
