using Ambev.DeveloperEvaluation.Application.Sales.v1.UpdateSale;
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
/// Unit tests for the UpdateSaleHandler
/// These tests ensure that the handler properly updates a sale and its associated sale items.
/// </summary>
public class UpdateSaleHandlerTests
{
    private readonly IMapper _mapper;
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleItemRepository _saleItemRepository;
    private readonly IItemRepository _itemRepository;
    private readonly UpdateSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the UpdateSaleHandlerTests
    /// This constructor sets up all the required dependencies for testing the handler.
    /// </summary>
    public UpdateSaleHandlerTests()
    {
        _mapper = Substitute.For<IMapper>();
        _saleRepository = Substitute.For<ISaleRepository>();
        _saleItemRepository = Substitute.For<ISaleItemRepository>();
        _itemRepository = Substitute.For<IItemRepository>();
        _handler = new UpdateSaleHandler(_mapper, _saleRepository, _saleItemRepository, _itemRepository);
    }

    /// <summary>
    /// Tests the scenario where the same items are updated in the sale.
    /// Verifies that the update operation returns a success response.
    /// </summary>
    [Fact(DisplayName = "Given update same items When updating sale Then returns success response")]
    public async Task Handle_UpdateSameSaleItemsRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var command = UpdateSaleHandlerTestData.GenerateValidCommand();
        var sale = SaleTestData.GenerateValidSale();
        sale.Id = command.Id;

        command.SaleItens = new List<UpdateSaleItemCommand>()
        {
            UpdateSaleHandlerTestData.GenerateValidItemCommand(),
            UpdateSaleHandlerTestData.GenerateValidItemCommand()
        };

        var itemsPrices = new Dictionary<Guid, decimal>();
        foreach (var item in command.SaleItens)
        {
            itemsPrices.Add(item.ItemId, 10m);
        }

        var sameSale = SaleItemTestData.GenerateValidSaleItem();
        sameSale.ItemId = command.SaleItens.First().ItemId;
        var saleItems = new List<SaleItem>() { sameSale };

        _saleRepository.GetByIdWithTrackingAsync(Arg.Any<Guid>(), CancellationToken.None).Returns(sale);
        _saleItemRepository.GetBySaleIdAsync(Arg.Any<Guid>(), CancellationToken.None).Returns(saleItems);
        _itemRepository.GetItemsPriceByIdAsync(Arg.Any<IEnumerable<Guid>>(), CancellationToken.None).Returns(itemsPrices);

        // Act
        var updateSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _saleRepository.Received(1).UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        await _saleItemRepository.Received(1).UpdateAsync(Arg.Any<IEnumerable<SaleItem>>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests the scenario where items are updated and added to the sale.
    /// Verifies that the update operation returns a success response and the items are updated and added.
    /// </summary>
    [Fact(DisplayName = "Given update and add item When updating sale Then returns success response")]
    public async Task Handle_UpdateAndAddSaleItemsRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var command = UpdateSaleHandlerTestData.GenerateValidCommand();
        var sale = SaleTestData.GenerateValidSale();
        sale.Id = command.Id;

        command.SaleItens = new List<UpdateSaleItemCommand>()
        {
            UpdateSaleHandlerTestData.GenerateValidItemCommand(),
            UpdateSaleHandlerTestData.GenerateValidItemCommand()
        };

        var saleItem1 = SaleItemTestData.GenerateValidSaleItem();
        saleItem1.ItemId = command.SaleItens.First().ItemId;
        var saleItems = new List<SaleItem>() { saleItem1 };

        var itemsPrices = new Dictionary<Guid, decimal>();
        itemsPrices.Add(command.SaleItens.First().ItemId, 10m);

        _saleRepository.GetByIdWithTrackingAsync(Arg.Any<Guid>(), CancellationToken.None).Returns(sale);
        _saleItemRepository.GetBySaleIdAsync(Arg.Any<Guid>(), CancellationToken.None).Returns(saleItems);
        _itemRepository.GetItemsPriceByIdAsync(Arg.Any<IEnumerable<Guid>>(), CancellationToken.None).Returns(itemsPrices);

        // Act
        var updateSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _saleRepository.Received(1).UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        await _saleItemRepository.Received(1).UpdateAsync(Arg.Any<IEnumerable<SaleItem>>(), Arg.Any<CancellationToken>());
        await _saleItemRepository.Received(1).RegisterSaleItensAsync(Arg.Any<IEnumerable<SaleItem>>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests the scenario where items are updated and removed to the sale.
    /// Verifies that the update operation returns a success response and the items are updated and removed.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Fact(DisplayName = "Given update and remove item When updating sale Then returns success response")]
    public async Task Handle_UpdateAndRemoveSaleItemsRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var command = UpdateSaleHandlerTestData.GenerateValidCommand();
        var sale = SaleTestData.GenerateValidSale();
        sale.Id = command.Id;

        var saleItems = new List<SaleItem>()
        {
            SaleItemTestData.GenerateValidSaleItem(),
            SaleItemTestData.GenerateValidSaleItem()
        };

        var commandItem1 = UpdateSaleHandlerTestData.GenerateValidItemCommand();
        commandItem1.ItemId = saleItems.First().ItemId;
        var commandItems = new List<UpdateSaleItemCommand>() { commandItem1 };
        command.SaleItens = commandItems;

        var itemsPrices = new Dictionary<Guid, decimal>();
        itemsPrices.Add(saleItems.First().ItemId, 10m);

        _saleRepository.GetByIdWithTrackingAsync(Arg.Any<Guid>(), CancellationToken.None).Returns(sale);
        _saleItemRepository.GetBySaleIdAsync(Arg.Any<Guid>(), CancellationToken.None).Returns(saleItems);
        _itemRepository.GetItemsPriceByIdAsync(Arg.Any<IEnumerable<Guid>>(), CancellationToken.None).Returns(itemsPrices);

        // Act
        var updateSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _saleRepository.Received(1).UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        await _saleItemRepository.Received(1).UpdateAsync(Arg.Any<IEnumerable<SaleItem>>(), Arg.Any<CancellationToken>());
        await _saleItemRepository.Received(1).DeleteAsync(Arg.Any<IEnumerable<SaleItem>>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests the scenario where the item quantity is invalid.
    /// Verifies that the update operation throws an InvalidOperationException.
    /// </summary>
    [Fact(DisplayName = "Given error When item quantity is more than valid Then returns error response")]
    public async Task Handle_InvalidItemQuantityRequest_ReturnsErrorResponse()
    {
        // Arrange
        var command = UpdateSaleHandlerTestData.GenerateValidCommand();

        var commandItems = UpdateSaleHandlerTestData.GenerateValidItemsCommand();
        commandItems.First().Quantity = ItemValidator.MaxLimitQuantityByItem + 1;
        command.SaleItens = commandItems;

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    /// <summary>
    /// Tests the scenario where the sale is not found in the repository.
    /// Verifies that the update operation throws a KeyNotFoundException.
    /// </summary>
    [Fact(DisplayName = "Given error When sale not found Then returns error response")]
    public async Task Handle_SaleNotFoundRequest_ReturnsErrorResponse()
    {
        // Arrange
        var command = UpdateSaleHandlerTestData.GenerateValidCommand();

        var commandItems = UpdateSaleHandlerTestData.GenerateValidItemsCommand();
        command.SaleItens = commandItems;

        _saleRepository.GetByIdWithTrackingAsync(Arg.Any<Guid>(), CancellationToken.None).Returns(Maybe.None);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }
}
