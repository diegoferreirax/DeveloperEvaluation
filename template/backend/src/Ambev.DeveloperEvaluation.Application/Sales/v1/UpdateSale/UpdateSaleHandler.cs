using Ambev.DeveloperEvaluation.Application.Sales.v1.SaleObservers;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Strategies.Discount;
using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
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

            var sale = await _saleRepository.GetByIdWithTrackingAsync(command.Id, cancellationToken);
            if (sale.HasNoValue)
            {
                throw new KeyNotFoundException($"Sale with ID {command.Id} not found");
            }

            var commandItems = command.SaleItens;
            var commandItemIds = commandItems.Select(i => i.ItemId);

            var saleToUpdate = sale.Value;
            var saleItems = await _saleItemRepository.GetBySaleIdAsync(saleToUpdate.Id, cancellationToken);
            var saleItemsIds = saleItems.Select(i => i.ItemId);

            #region REMOVE ITEMS
            var itemsToRemove = saleItems.Where(i => !commandItemIds.Contains(i.ItemId));
            if (itemsToRemove.Any())
            {
                await _saleItemRepository.DeleteAsync(itemsToRemove, cancellationToken);
            }
            #endregion

            // TODO: encapsular calculo total amount
            var totalAmount = 0m;

            #region ADD ITEMS
            var itemsCommandToAdd = commandItems.Where(i => !saleItemsIds.Contains(i.ItemId));
            if (itemsCommandToAdd.Any())
            {
                var itemsToAdd = new List<SaleItem>();
                var itemsCommandToAddIds = itemsCommandToAdd.Select(i => i.ItemId);
                var itemsCommandPricesToAdd = await GetItemsPrice(itemsCommandToAddIds);

                foreach (var itemCommandToAdd in itemsCommandToAdd)
                {
                    decimal price;
                    itemsCommandPricesToAdd.TryGetValue(itemCommandToAdd.ItemId, out price);

                    var (discountPercent, totalPrice) = DiscountFactory.GetDiscountAndTotalPriceItem(price, itemCommandToAdd.Quantity);

                    totalAmount += totalPrice;

                    itemsToAdd.Add(new SaleItem
                    {
                        SaleId = saleToUpdate.Id,
                        Sale = saleToUpdate,

                        ItemId = itemCommandToAdd.ItemId,
                        Quantity = itemCommandToAdd.Quantity,

                        Discount = discountPercent,
                        TotalItemAmount = totalPrice
                    });
                }

                await _saleItemRepository.RegisterSaleItensAsync(itemsToAdd, cancellationToken);
            }
            #endregion

            #region UPDATE ITEMS
            var itemsToUpdate = saleItems.Where(i => commandItemIds.Contains(i.ItemId));
            if (itemsToUpdate.Any())
            {
                var itemsPricesToUpdate = await GetItemsPriceInMemory(itemsToUpdate);
                var itemsCommandToUpdate = commandItems.Where(i => saleItemsIds.Contains(i.ItemId));

                foreach (var itemCommandToUpdate in itemsCommandToUpdate)
                {
                    SaleItem price;
                    itemsPricesToUpdate.TryGetValue(itemCommandToUpdate.ItemId, out price);
                    price.Quantity = itemCommandToUpdate.Quantity;

                    var (discountPercent, totalPrice) = DiscountFactory.GetDiscountAndTotalPriceItem(price.Item.UnitPrice, price.Quantity);

                    totalAmount += totalPrice;
                    price.Discount = discountPercent;
                    price.TotalItemAmount = totalPrice;
                }
                await _saleItemRepository.UpdateAsync(itemsToUpdate, cancellationToken);
            }
            #endregion

            saleToUpdate.SaleDate = command.SaleDate;
            saleToUpdate.Branch = command.Branch;
            saleToUpdate.TotalAmount = totalAmount;

            // TODO: talvez criar um endpoint para cancelar venda
            saleToUpdate.IsCanceled = command.IsCanceled;

            await _saleRepository.UpdateAsync(saleToUpdate, cancellationToken);

            await SendNotifications(saleToUpdate.Id, saleToUpdate.IsCanceled);

            tx.Complete();
            return new UpdateSaleResult();
        }
    }

    private async Task<IDictionary<Guid, decimal>> GetItemsPrice(IEnumerable<Guid> ids)
    {
        var itemsPrices = (await _itemRepository.GetItemsPriceByIdAsync(ids)).Value;
        if (!itemsPrices.Any())
        {
            throw new KeyNotFoundException($"Items not found");
        }
        return itemsPrices;
    }

    private async Task<IDictionary<Guid, SaleItem>> GetItemsPriceInMemory(IEnumerable<SaleItem> saleItems)
    {
        var itemsPrices = new Dictionary<Guid, SaleItem>();
        foreach (var saleItem in saleItems)
        {
            itemsPrices.Add(saleItem.ItemId, saleItem);
        }
        return itemsPrices;
    }

    private async Task SendNotifications(Guid saleId, bool isCanceled)
    {
        var publisher = new EventPublisher();

        publisher.RegisterObserver(new SaleModifiedObserver());

        if (isCanceled)
        {
            publisher.RegisterObserver(new SaleCancelledObserver());
            publisher.RegisterObserver(new SaleItemCancelledObserver());
        }

        await publisher.Notify(saleId);
    }
}
