using Ambev.DeveloperEvaluation.Application.Sales.v1.RegisterSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using AutoMapper;
using CSharpFunctionalExtensions;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Unit tests for the RegisterSaleHandler.
/// </summary>
public class RegisterSaleHandlerTests
{
    private readonly IMapper _mapper;
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleItemRepository _saleItemRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IItemRepository _itemRepository;
    private readonly RegisterSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the RegisterSaleHandlerTests.
    /// </summary>
    public RegisterSaleHandlerTests()
    {
        _mapper = Substitute.For<IMapper>();
        _saleRepository = Substitute.For<ISaleRepository>();
        _saleItemRepository = Substitute.For<ISaleItemRepository>();
        _customerRepository = Substitute.For<ICustomerRepository>();
        _itemRepository = Substitute.For<IItemRepository>();
        _handler = new RegisterSaleHandler(_mapper, _saleRepository, _saleItemRepository, _customerRepository, _itemRepository);
    }

    // TODO: fazer testes validando calculo total da venda

    /// <summary>
    /// Tests if registering a sale with valid data returns a success response.
    /// </summary>
    [Fact(DisplayName = "Given valid sale data When registering sale Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var command = RegisterSaleHandlerTestData.GenerateValidCommand();
        var saleId = Guid.NewGuid();

        _customerRepository.GetByIdAsync(Arg.Any<Guid>(), CancellationToken.None).Returns(CustomerTestData.GenerateValidCustomer());
        _saleRepository.RegisterSaleAsync(Arg.Any<Sale>(), CancellationToken.None).Returns(saleId);
        _saleRepository.GetSaleExistByNumberAsync(Arg.Any<int>(), CancellationToken.None).Returns(false);

        var totalAmount = 100.0m;
        var sale = new Sale
        {
            Id = saleId,
            CustomerId = command.CustomerId,
            SaleNumber = command.SaleNumber,
            SaleDate = command.SaleDate,
            TotalAmount = totalAmount,
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

        _itemRepository.GetItemsPriceByIdAsync(Arg.Any<IEnumerable<Guid>>(), CancellationToken.None).Returns(itemsPrices);

        _mapper.Map<Sale>(command).Returns(sale);
        _mapper.Map<RegisterSaleResult>(saleId).Returns(new RegisterSaleResult(saleId));

        // Act
        var registerSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Assert
        registerSaleResult.Should().NotBeNull();
        registerSaleResult.id.Should().Be(sale.Id);
        await _saleRepository.Received(1).RegisterSaleAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        await _saleItemRepository.Received(1).RegisterSaleItensAsync(Arg.Any<IEnumerable<SaleItem>>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests if an error is thrown when the sale number already exists.
    /// </summary>
    [Fact(DisplayName = "Given error When Sale number already exist Then returns error response")]
    public async Task Handle_SaleNumberExist_ReturnsErrorResponse()
    {
        // Arrange
        var command = RegisterSaleHandlerTestData.GenerateValidCommand();

        _saleRepository.GetSaleExistByNumberAsync(Arg.Any<int>(), CancellationToken.None).Returns(true);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    /// <summary>
    /// Tests if an error is thrown when the customer is not found.
    /// </summary>
    [Fact(DisplayName = "Given error When Customer not found Then returns error response")]
    public async Task Handle_CustomerNotFound_ReturnsErrorResponse()
    {
        // Arrange
        var command = RegisterSaleHandlerTestData.GenerateValidCommand();

        _saleRepository.GetSaleExistByNumberAsync(Arg.Any<int>(), CancellationToken.None).Returns(false);
        _customerRepository.GetByIdAsync(Arg.Any<Guid>(), CancellationToken.None).Returns(Maybe.None);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }

    /// <summary>
    /// Tests if an error is thrown when the item quantity exceeds the allowed limit.
    /// </summary>
    [Fact(DisplayName = "Given error When item quantity is more then 20 Then returns error response")]
    public async Task Handle_InvalidItemQuantityRequest_ReturnsErrorResponse()
    {
        // Arrange
        var command = RegisterSaleHandlerTestData.GenerateValidCommand();
        command.SaleItens.First().Quantity = ItemValidator.MaxLimitQuantityByItem + 1;

        _saleRepository.GetSaleExistByNumberAsync(Arg.Any<int>(), CancellationToken.None).Returns(false);
        _customerRepository.GetByIdAsync(Arg.Any<Guid>(), CancellationToken.None).Returns(CustomerTestData.GenerateValidCustomer());

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}
