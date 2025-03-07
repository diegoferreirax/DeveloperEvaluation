using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.RegisterSale;

public class RegisterSaleHandler : IRequestHandler<RegisterSaleCommand, RegisterSaleResult>
{
    private readonly IMapper _mapper;
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleItemRepository _saleItemRepository;

    public RegisterSaleHandler(
        IMapper mapper,
        ISaleRepository saleRepository,
        ISaleItemRepository saleItemRepository)
    {
        _mapper = mapper;
        _saleRepository = saleRepository;
        _saleItemRepository = saleItemRepository;
    }

    public async Task<RegisterSaleResult> Handle(RegisterSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new RegisterSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = _mapper.Map<Sale>(command);
        var registerSale = await _saleRepository.RegisterSaleAsync(sale, cancellationToken);



        var saleItens = new List<SaleItem>();
        foreach (var saleItemCommand in command.SaleItens)
        {
            var saleItem = _mapper.Map<SaleItem>(saleItemCommand);
            saleItem.SaleId = sale.Id;

            saleItens.Add(saleItem);
        }
        var registerSaleItens = await _saleItemRepository.RegisterSaleItensAsync(saleItens.ToArray(), cancellationToken);



        var result = _mapper.Map<RegisterSaleResult>(registerSale);
        return result;
    }
}
