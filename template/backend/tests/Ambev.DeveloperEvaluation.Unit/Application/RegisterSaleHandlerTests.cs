using Ambev.DeveloperEvaluation.Application.Sales.RegisterSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class RegisterSaleHandlerTests
{
    private readonly IMapper _mapper;
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleItemRepository _saleItemRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IItemRepository _itemRepository;
    private readonly RegisterSaleHandler _handler;

    public RegisterSaleHandlerTests()
    {
        _mapper = Substitute.For<IMapper>();
        _saleRepository = Substitute.For<ISaleRepository>();
        _saleItemRepository = Substitute.For<ISaleItemRepository>();
        _customerRepository = Substitute.For<ICustomerRepository>();
        _itemRepository = Substitute.For<IItemRepository>();
        _handler = new RegisterSaleHandler(_mapper, _saleRepository, _saleItemRepository, _customerRepository, _itemRepository);
    }

    [Fact(DisplayName = "Given valid sale data When registering sale Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var command = RegisterSaleHandlerTestData.GenerateValidCommand();
        var saleId = Guid.NewGuid();

        _customerRepository.GetByIdAsync(Arg.Any<Guid>(), CancellationToken.None).Returns(CustomerTestData.GenerateValidCustomer());
        _saleRepository.RegisterSaleAsync(Arg.Any<Sale>(), CancellationToken.None).Returns(saleId);
        _saleRepository.GetSaleExistByNumberAsync(Arg.Any<int>(), CancellationToken.None).Returns(false);

        var sale = new Sale
        {
            Id = saleId,
            CustomerId = command.CustomerId,
            SaleNumber = command.SaleNumber,
            SaleDate = command.SaleDate,
            TotalAmount = command.TotalAmount,
            IsCanceled = command.IsCanceled,
            Branch = command.Branch,
        };

        var price = 10.0m;
        var discount = 0m;
        var itemsPrices = new Dictionary<Guid, decimal>();

        foreach (var item in command.SaleItens)
        {
            itemsPrices.Add(item.ItemId, price);

            _mapper.Map<SaleItem>(item).Returns(new SaleItem
            {
                ItemId = item.ItemId,
                Quantity = item.Quantity,
                Discount = discount
            });
        }

        _itemRepository.GetItemsPriceByIdAsync(Arg.Any<Guid[]>(), CancellationToken.None).Returns(itemsPrices);

        _mapper.Map<Sale>(command).Returns(sale);
        _mapper.Map<RegisterSaleResult>(saleId).Returns(new RegisterSaleResult(saleId));

        // Act
        var registerSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Assert
        registerSaleResult.Should().NotBeNull();
        registerSaleResult.id.Should().Be(sale.Id);
        await _saleRepository.Received(1).RegisterSaleAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        await _saleItemRepository.Received(1).RegisterSaleItensAsync(Arg.Any<SaleItem[]>(), Arg.Any<CancellationToken>());
    }
}
