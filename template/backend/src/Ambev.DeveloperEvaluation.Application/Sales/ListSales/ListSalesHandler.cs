using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

public class ListSalesHandler : IRequestHandler<ListSalesCommand, ListSalesResult>
{
    private readonly IMapper _mapper;
    private readonly ISaleRepository _saleRepository;

    /// <summary>
    /// Initializes a new instance of ListSalesHandler.
    /// </summary>
    /// <param name="saleRepository">The ISaleRepository instance</param>
    public ListSalesHandler(
        IMapper mapper, 
        ISaleRepository saleRepository)
    {
        _mapper = mapper;
        _saleRepository = saleRepository;
    }

    /// <summary>
    /// Handles the ListSalesCommand request
    /// </summary>
    /// <param name="command">The ListSales command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of sales</returns>
    public async Task<ListSalesResult> Handle(ListSalesCommand command, CancellationToken cancellationToken)
    {
        var validator = new ListSalesCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var (sales, count) = await _saleRepository.ListSalesAsync(command.Size, command.Page, command.Order, cancellationToken);

        var salesResult = new ListSalesResult
        (
            count,
            _mapper.Map<SalesResult[]>(sales)
        );
        
        return salesResult;
    }
}
