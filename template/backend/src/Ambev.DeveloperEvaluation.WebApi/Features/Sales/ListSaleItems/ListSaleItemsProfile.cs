using Ambev.DeveloperEvaluation.Application.Sales.ListSaleItems;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSaleItems;

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
