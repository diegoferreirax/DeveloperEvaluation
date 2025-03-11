using Ambev.DeveloperEvaluation.Application.Sales.v1.ListSaleItems;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.v1.ListSaleItems;

public class ListSaleItemsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListSaleItems feature
    /// </summary>
    public ListSaleItemsProfile()
    {
        CreateMap<ListSaleItemsRequest, ListSaleItemsCommand>();
        CreateMap<ListSaleItemsResult, ListSaleItemsResponse>();
        CreateMap<ItemResult, ItemResponse>();
    }
}
