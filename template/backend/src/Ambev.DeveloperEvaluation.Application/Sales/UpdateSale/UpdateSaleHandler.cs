using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using System.Transactions;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly IMapper _mapper;
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleItemRepository _saleItemRepository;

    public UpdateSaleHandler(
        IMapper mapper,
        ISaleRepository saleRepository,
        ISaleItemRepository saleItemRepository)
    {
        _mapper = mapper;
        _saleRepository = saleRepository;
        _saleItemRepository = saleItemRepository;
    }

    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        using (var tx = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
        {
            var validator = new UpdateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetByIdWithTrackingAsync(command.Id, cancellationToken);
            if (sale.HasNoValue)
            {
                throw new KeyNotFoundException($"Sale with ID {sale.Value.Id} not found");
            }
            
            // TODO: atualizar todos os dados ? 
            var saleToUpdate = sale.Value;
            saleToUpdate.SaleDate = command.SaleDate;
            saleToUpdate.TotalAmount = command.TotalAmount;
            saleToUpdate.IsCanceled = command.IsCanceled;
            saleToUpdate.Branch = command.Branch;

            var saleItens = (await _saleItemRepository.GetBySaleIdAsync(saleToUpdate.Id, cancellationToken)).Value.ToList();

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
                        SaleId = saleToUpdate.Id,
                        Sale = saleToUpdate,

                        ItemId = updatedItem.ItemId,

                        Quantity = updatedItem.Quantity,
                        Discount = updatedItem.Discount
                    });
                }
            }

            await _saleRepository.UpdateAsync(saleToUpdate, cancellationToken);

            if (itemsToUpdate.Count > 0)
            {
                await _saleItemRepository.UpdateAsync(itemsToUpdate.ToArray(), cancellationToken);
            }

            if (itemsToAdd.Count > 0)
            {
                await _saleItemRepository.RegisterSaleItensAsync(itemsToAdd.ToArray(), cancellationToken);
            }

            tx.Complete();
            return new UpdateSaleResult();
        }
    }
}
