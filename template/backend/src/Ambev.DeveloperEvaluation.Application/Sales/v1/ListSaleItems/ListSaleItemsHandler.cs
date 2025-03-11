using Ambev.DeveloperEvaluation.Application.Sales.v1.ListSaleItems;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.v1.ListSaleItems;

public class ListSaleItemsHandler : IRequestHandler<ListSaleItemsCommand, ListItemsResult>
{
    private readonly IMapper _mapper;
    private readonly ISaleItemRepository _saleItemRepository;
    private readonly ISaleRepository _saleRepository;

    /// <summary>
    /// Initializes a new instance of ListSaleItemsHandler.
    /// </summary>
    /// <param name="saleItemRepository">The ISaleItemRepository instance</param>
    public ListSaleItemsHandler(
        IMapper mapper,
        ISaleItemRepository saleItemRepository,
        ISaleRepository saleRepository)
    {
        _mapper = mapper;
        _saleItemRepository = saleItemRepository;
        _saleRepository = saleRepository;
    }

    public async Task<ListItemsResult> Handle(ListSaleItemsCommand command, CancellationToken cancellationToken)
    {
        var validator = new ListSaleItemsCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
        if (sale.HasNoValue)
        {
            throw new KeyNotFoundException($"Sale with ID {command.Id} not found");
        }

        // TODO: verificar necessidade de paginação
        var saleItems = await _saleItemRepository.GetBySaleWithItemIdAsync(command.Id);
        var itemsResult = _mapper.Map<ListSaleItemsResult[]>(saleItems);

        return new ListItemsResult(itemsResult);
    }
}
