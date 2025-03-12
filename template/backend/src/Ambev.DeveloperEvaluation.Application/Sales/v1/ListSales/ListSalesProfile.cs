using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.v1.ListSales;

/// <summary>
/// Profile for mapping between Application and API ListSales responses
/// </summary>
public class ListSalesProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the ListSalesProfile.
    /// </summary>
    public ListSalesProfile()
    {
        CreateMap<Sale, SalesResult>();
    }
}
