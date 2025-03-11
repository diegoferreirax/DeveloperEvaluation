using Ambev.DeveloperEvaluation.Application.Sales.v1.ListSales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.v1.ListSales;

/// <summary>
/// Request model for list sales
/// </summary>
public class ListSalesProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListSales feature
    /// </summary>
    public ListSalesProfile()
    {
        CreateMap<ListSalesRequest, ListSalesCommand>();
        CreateMap<SalesResult, SalesResponse>();
    }
}
