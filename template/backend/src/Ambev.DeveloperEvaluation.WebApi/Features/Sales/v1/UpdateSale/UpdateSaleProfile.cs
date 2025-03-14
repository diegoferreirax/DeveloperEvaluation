﻿using Ambev.DeveloperEvaluation.Application.Sales.v1.UpdateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.v1.UpdateSale;

/// <summary>
/// Profile for mapping between Application and API UpdateSale responses
/// </summary>
public class UpdateSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateSale feature
    /// </summary>
    public UpdateSaleProfile()
    {
        CreateMap<UpdateSaleRequest, UpdateSaleCommand>();

        CreateMap<UpdateSaleItemRequest, UpdateSaleItemCommand>()
            .ForMember(com => com.TotalItemAmount, req => req.Ignore())
            .ForMember(com => com.Discount, req => req.Ignore());
    }
}
