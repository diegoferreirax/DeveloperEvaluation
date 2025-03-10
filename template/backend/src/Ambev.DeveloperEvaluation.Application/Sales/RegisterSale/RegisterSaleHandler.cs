using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Strategies.Discount;
using Ambev.DeveloperEvaluation.Domain.Validation;
using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using System.Transactions;

namespace Ambev.DeveloperEvaluation.Application.Sales.RegisterSale;

/// <summary>
/// Handler for processing RegisterSaleCommand requests
/// </summary>
public class RegisterSaleHandler : IRequestHandler<RegisterSaleCommand, RegisterSaleResult>
{
    private readonly IMapper _mapper;
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleItemRepository _saleItemRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IItemRepository _itemRepository;

    /// <summary>
    /// Initializes a new instance of RegisterSaleHandler
    /// </summary>
    /// <param name="mapper">An instance of IMapper.</param>
    /// <param name="saleRepository">An instance of ISaleRepository.</param>
    /// <param name="saleItemRepository">An instance of ISaleItemRepository.</param>
    /// <param name="customerRepository">An instance of ICustomerRepository.</param>
    /// <param name="itemRepository">An instance of IItemRepository.</param>
    public RegisterSaleHandler(
        IMapper mapper,
        ISaleRepository saleRepository,
        ISaleItemRepository saleItemRepository,
        ICustomerRepository customerRepository,
        IItemRepository itemRepository)
    {
        _mapper = mapper;
        _saleRepository = saleRepository;
        _saleItemRepository = saleItemRepository;
        _customerRepository = customerRepository;
        _itemRepository = itemRepository;
    }

    /// <summary> 
    /// Handles the RegisterSaleCommand request
    /// </summary>
    /// <param name="command">The RegisterSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A RegisterSaleResult containing the result of the sale registration.</returns>
    public async Task<RegisterSaleResult> Handle(RegisterSaleCommand command, CancellationToken cancellationToken)
    {
        using (var tx = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
        {
            var validator = new RegisterSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var saleExist = await _saleRepository.GetSaleExistByNumberAsync(command.SaleNumber);
            if (saleExist)
            {
                throw new InvalidOperationException($"Sale number already exist.");
            }

            var customer = await _customerRepository.GetByIdAsync(command.CustomerId);
            if (customer.HasNoValue)
            {
                throw new KeyNotFoundException($"Customer with ID {customer.Value.Id} not found");
            }

            bool hasItemWithMoreThan20 = command.SaleItens.Any(item => item.Quantity > 20);
            if (hasItemWithMoreThan20)
            {
                throw new InvalidOperationException($"Item quantity is more then 20.");
            }

            var itemsPrices = await GetItemsPrice(command);

            var totalAmount = 0m;
            foreach (var item in command.SaleItens)
            {
                decimal price;
                itemsPrices.TryGetValue(item.ItemId, out price);

                var discountStrategy = DiscountFactory.GetDiscountStrategy(item.Quantity);
                if (discountStrategy.HasValue)
                {
                    item.Discount = discountStrategy.Value.GetPercent();

                    var discountedPrice = discountStrategy.Value.GetDiscount(price, item.Quantity);
                    var totalPriceWithDiscount = CalculateTotalPrice(discountedPrice, item.Quantity);
                    item.SetTotalItemAmount(totalPriceWithDiscount);
                    totalAmount += totalPriceWithDiscount;
                }
                else
                {
                    var totalPrice = CalculateTotalPrice(price, item.Quantity);
                    item.SetTotalItemAmount(totalPrice);
                    totalAmount += totalPrice;
                }
            }

            var sale = _mapper.Map<Sale>(command);
            sale.Customer = customer.Value;
            sale.TotalAmount = totalAmount;

            var saleValidator = new SaleValidator();
            var saleValidationResult = await saleValidator.ValidateAsync(sale, cancellationToken);
            if (!saleValidationResult.IsValid)
                throw new ValidationException(saleValidationResult.Errors);

            var saleId = await _saleRepository.RegisterSaleAsync(sale, cancellationToken);

            var saleItens = new List<SaleItem>();
            foreach (var saleItemCommand in command.SaleItens)
            {
                var saleItem = _mapper.Map<SaleItem>(saleItemCommand);
                saleItem.SaleId = saleId;

                saleItens.Add(saleItem);
            }
            await _saleItemRepository.RegisterSaleItensAsync(saleItens.ToArray(), cancellationToken);

            var result = _mapper.Map<RegisterSaleResult>(saleId);

            tx.Complete();
            return result;
        }
    }

    private async Task<IDictionary<Guid, decimal>> GetItemsPrice(RegisterSaleCommand command)
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
