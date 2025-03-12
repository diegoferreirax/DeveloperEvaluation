using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.v1.UpdateSale;

/// <summary>
/// AutoMapper for objects related to updating a sale.
/// </summary>
public class UpdateSaleProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the UpdateSaleProfile.
    /// 
    /// Mapping configurations:
    /// - Maps UpdateSaleCommand to Sale.
    /// - Maps UpdateSaleItemCommand to SaleItem.
    /// - Maps Sale to UpdateSaleResult.
    /// </summary>
    public UpdateSaleProfile()
    {
        CreateMap<UpdateSaleCommand, Sale>();
        CreateMap<UpdateSaleItemCommand, SaleItem>();
        CreateMap<Sale, UpdateSaleResult>();
    }
}
