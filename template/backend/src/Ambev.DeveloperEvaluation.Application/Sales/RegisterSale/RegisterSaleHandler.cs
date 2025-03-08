using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Validation;
using AutoMapper;
using FluentValidation;
using MediatR;
using System.Transactions;

namespace Ambev.DeveloperEvaluation.Application.Sales.RegisterSale;

public class RegisterSaleHandler : IRequestHandler<RegisterSaleCommand, RegisterSaleResult>
{
    private readonly IMapper _mapper;
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleItemRepository _saleItemRepository;
    private readonly ICustomerRepository _customerRepository;

    public RegisterSaleHandler(
        IMapper mapper,
        ISaleRepository saleRepository,
        ISaleItemRepository saleItemRepository,
        ICustomerRepository customerRepository)
    {
        _mapper = mapper;
        _saleRepository = saleRepository;
        _saleItemRepository = saleItemRepository;
        _customerRepository = customerRepository;
    }

    public async Task<RegisterSaleResult> Handle(RegisterSaleCommand command, CancellationToken cancellationToken)
    {
        using (var tx = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
        {
            var validator = new RegisterSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var customer = await _customerRepository.GetByIdAsync(command.CustomerId);
            if (customer.HasNoValue)
            {
                throw new KeyNotFoundException($"Customer with ID {customer.Value.Id} not found");
            }

            var sale = _mapper.Map<Sale>(command);
            sale.Customer = customer.Value;

            var saleValidator = new SaleValidator();
            var saleValidationResult = await saleValidator.ValidateAsync(sale, cancellationToken);
            if (!saleValidationResult.IsValid)
                throw new ValidationException(saleValidationResult.Errors);

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

            tx.Complete();
            return result;
        }
    }
}
