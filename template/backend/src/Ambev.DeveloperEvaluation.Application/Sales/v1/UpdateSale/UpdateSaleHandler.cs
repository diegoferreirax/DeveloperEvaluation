using Ambev.DeveloperEvaluation.Application.Sales.v1.SaleEvents;
using Ambev.DeveloperEvaluation.Application.Sales.v1.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Strategies.Discount;
using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using OneOf.Types;
using System.Transactions;

namespace Ambev.DeveloperEvaluation.Application.Sales.v1.UpdateSale;

/// <summary>
/// Handler for processing UpdateSaleCommand requests
/// </summary>
public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly IMapper _mapper;
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleItemRepository _saleItemRepository;
    private readonly IItemRepository _itemRepository;

    /// <summary>
    /// Initializes a new instance of the UpdateSaleHandler class.
    /// </summary>
    /// <param name="mapper">The IMapper instance used for mapping objects.</param>
    /// <param name="saleRepository">The ISaleRepository instance used for interacting with sales data.</param>
    /// <param name="saleItemRepository">The ISaleItemRepository instance used for interacting with sale items data.</param>
    public UpdateSaleHandler(
        IMapper mapper,
        ISaleRepository saleRepository,
        ISaleItemRepository saleItemRepository,
        IItemRepository itemRepository)
    {
        _mapper = mapper;
        _saleRepository = saleRepository;
        _saleItemRepository = saleItemRepository;
        _itemRepository = itemRepository;
    }

    /// <summary>
    /// Handles the UpdateSaleCommand instance.
    /// </summary>
    /// <param name="command">The UpdateSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>An UpdateSaleResult of the sale update.</returns>
    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        using (var tx = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
        {
            // TODO: metodo muito grande, ajustar
            var validator = new UpdateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            bool hasItemWithMoreThan20 = command.SaleItens.Any(item => item.Quantity > 20);
            if (hasItemWithMoreThan20)
            {
                throw new InvalidOperationException($"Item quantity is more then 20.");
            }

            var sale = await _saleRepository.GetByIdWithTrackingAsync(command.Id, cancellationToken);
            if (sale.HasNoValue)
            {
                throw new KeyNotFoundException($"Sale with ID {command.Id} not found");
            }

            var saleToUpdate = sale.Value;
            saleToUpdate.SaleDate = command.SaleDate;
            saleToUpdate.Branch = command.Branch;

            // TODO: talvez criar um endpoint para cancelar venda
            saleToUpdate.IsCanceled = command.IsCanceled;

            var saleItems = (await _saleItemRepository.GetBySaleIdAsync(saleToUpdate.Id, cancellationToken)).ToList();

            var updatedItems = command.SaleItens;
            var updatedItemIds = updatedItems.Select(i => i.ItemId).ToList();

            var itemsToRemove = saleItems.Where(i => !updatedItemIds.Contains(i.ItemId)).ToArray();
            if (itemsToRemove.Length > 0)
            {
                await _saleItemRepository.DeleteAsync(itemsToRemove, cancellationToken);
            }

            var itemsPrices = await GetItemsPrice(command);

            var totalAmount = 0m;
            var itemsToUpdate = new List<SaleItem>();
            var itemsToAdd = new List<SaleItem>();

            foreach (var updatedItem in updatedItems)
            {
                decimal price;
                itemsPrices.TryGetValue(updatedItem.ItemId, out price);

                var discountStrategy = DiscountFactory.GetDiscountStrategy(updatedItem.Quantity);
                if (discountStrategy.HasValue)
                {
                    updatedItem.Discount = discountStrategy.Value.GetPercent();

                    var discountedPrice = discountStrategy.Value.GetDiscount(price, updatedItem.Quantity);
                    var totalPriceWithDiscount = CalculateTotalPrice(discountedPrice, updatedItem.Quantity);
                    updatedItem.SetTotalItemAmount(totalPriceWithDiscount);
                    totalAmount += totalPriceWithDiscount;
                }
                else
                {
                    var totalPrice = CalculateTotalPrice(price, updatedItem.Quantity);
                    updatedItem.SetTotalItemAmount(totalPrice);
                    totalAmount += totalPrice;
                }

                var existingItem = saleItems.FirstOrDefault(i => i.ItemId == updatedItem.ItemId);
                if (existingItem is not null)
                {
                    existingItem.Discount = updatedItem.Discount;
                    existingItem.Quantity = updatedItem.Quantity;
                    existingItem.TotalItemAmount = updatedItem.TotalItemAmount;
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
                        Discount = updatedItem.Discount,
                        TotalItemAmount = updatedItem.TotalItemAmount
                    });
                }
            }

            saleToUpdate.TotalAmount = totalAmount;

            await _saleRepository.UpdateAsync(saleToUpdate, cancellationToken);

            if (itemsToUpdate.Count > 0)
            {
                await _saleItemRepository.UpdateAsync(itemsToUpdate.ToArray(), cancellationToken);
            }

            if (itemsToAdd.Count > 0)
            {
                await _saleItemRepository.RegisterSaleItensAsync(itemsToAdd.ToArray(), cancellationToken);
            }

            var publisher = new EventPublisher();

            if (saleToUpdate.IsCanceled)
            {
                publisher.RegisterObserver(new SaleCancelledObserver());
                publisher.RegisterObserver(new SaleItemCancelledObserver());
            }

            publisher.RegisterObserver(new SaleModifiedObserver());
            await publisher.Notify(saleToUpdate.Id);

            tx.Complete();
            return new UpdateSaleResult();
        }
    }

    private async Task<IDictionary<Guid, decimal>> GetItemsPrice(UpdateSaleCommand command)
    {
        // TODO: talvez armazenar os itens no cache e buscar os preços
        var commandItemsIds = command.SaleItens.Select(i => i.ItemId).ToArray();
        var itemsPrices = (await _itemRepository.GetItemsPriceByIdAsync(commandItemsIds)).Value;
        if (!itemsPrices.Any())
        {
            throw new KeyNotFoundException($"Items not found");
        }
        return itemsPrices;
    }

    private decimal CalculateTotalPrice(decimal price, int quantity)
    {
        // TODO: criar um serviço especifico para o calculo
        return price * quantity;
    }
}
