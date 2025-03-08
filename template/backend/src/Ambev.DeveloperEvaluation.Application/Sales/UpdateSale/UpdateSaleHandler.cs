using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Validation;
using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly IMapper _mapper;
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleItemRepository _saleItemRepository;
    private readonly ICustomerRepository _customerRepository;

    public UpdateSaleHandler(
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

    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleCommandValidator();
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

        var updatedSale = await _saleRepository.UpdateAsync(sale, cancellationToken);

        var saleItens = (await _saleItemRepository.GetBySaleIdAsync(sale.Id, cancellationToken)).Value.ToList();

        var updatedItens = command.SaleItens;
        var updatedItemIds = updatedItens.Select(i => i.ItemId).ToList();

        var itemsToRemove = saleItens.Where(i => !updatedItemIds.Contains(i.Id)).ToArray();
        if (itemsToRemove.Length > 0)
        {
            await _saleItemRepository.DeleteAsync(itemsToRemove, cancellationToken);
        }

        var itemsToUpdate = new List<SaleItem>();
        var itemsToAdd = new List<SaleItem>();
        foreach (var updatedItem in updatedItens)
        {
            var existingItem = saleItens.FirstOrDefault(i => i.Id == updatedItem.ItemId);
            if (existingItem is not null) 
            {
                existingItem.Discount = updatedItem.Discount;
                existingItem.Quantity = updatedItem.Quantity;
                itemsToUpdate.Add(existingItem);
            }
            else
            {
                itemsToAdd.Add(new SaleItem
                {
                    SaleId = sale.Id,
                    Sale = sale,

                    ItemId = updatedItem.ItemId,

                    Quantity = updatedItem.Quantity,
                    Discount = updatedItem.Discount
                });
            }
        }

        if (itemsToUpdate.Count > 0)
        {
            await _saleItemRepository.UpdateAsync(itemsToUpdate.ToArray(), cancellationToken);
        }

        if (itemsToAdd.Count > 0)
        {
            await _saleItemRepository.RegisterSaleItensAsync(itemsToAdd.ToArray(), cancellationToken);
        }

        var result = _mapper.Map<UpdateSaleResult>(updatedSale.Value);
        return result;
    }
}
