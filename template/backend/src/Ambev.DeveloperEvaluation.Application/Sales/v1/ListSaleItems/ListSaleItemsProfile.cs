using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.v1.ListSaleItems;

/// <summary>
/// Profile for mapping between Application and API ListSaleItems responses
/// </summary>
public class ListSaleItemsProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the ListSaleItems Profile.
    /// </summary>
    public ListSaleItemsProfile()
    {
        CreateMap<SaleItem, ListSaleItemsResult>();
        CreateMap<Item, ItemResult>();
    }
}
