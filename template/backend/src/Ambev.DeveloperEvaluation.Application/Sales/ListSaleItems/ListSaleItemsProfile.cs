using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSaleItems;

public class ListSaleItemsProfile : Profile
{
    public ListSaleItemsProfile()
    {
        CreateMap<SaleItem, ListSaleItemsResult>();
        CreateMap<Item, ItemResult>();
    }
}
