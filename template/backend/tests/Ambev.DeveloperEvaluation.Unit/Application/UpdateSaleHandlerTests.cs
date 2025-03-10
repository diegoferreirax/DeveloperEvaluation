using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using AutoMapper;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class UpdateSaleHandlerTests
{
    private readonly IMapper _mapper;
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleItemRepository _saleItemRepository;
    private readonly IItemRepository _itemRepository;
    private readonly UpdateSaleHandler _handler;

    public UpdateSaleHandlerTests()
    {
        _mapper = Substitute.For<IMapper>();
        _saleRepository = Substitute.For<ISaleRepository>();
        _saleItemRepository = Substitute.For<ISaleItemRepository>();
        _itemRepository = Substitute.For<IItemRepository>();
        _handler = new UpdateSaleHandler(_mapper, _saleRepository, _saleItemRepository, _itemRepository);
    }

    [Fact(DisplayName = "Given update same items When updating sale Then returns success response")]
    public async Task Handle_UpdateSameSaleItemRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var command = UpdateSaleHandlerTestData.GenerateValidCommand();
        var sale = SaleTestData.GenerateValidSale();
        sale.Id = command.Id;

        var commandItems = new List<UpdateSaleItemCommand>()
        {
            UpdateSaleHandlerTestData.GenerateValidItemCommand(),
            UpdateSaleHandlerTestData.GenerateValidItemCommand()
        };
        command.SaleItens = commandItems.ToArray();

        var itemsPrices = new Dictionary<Guid, decimal>();
        var saleItems = command.SaleItens.Select(i =>
        {
            var saleItem = SaleItemTestData.GenerateValidSaleItem();
            saleItem.ItemId = i.ItemId;

            itemsPrices.Add(saleItem.ItemId, 10m);
            return saleItem;
        });

        _saleRepository.GetByIdWithTrackingAsync(Arg.Any<Guid>(), CancellationToken.None).Returns(sale);
        _saleItemRepository.GetBySaleIdAsync(Arg.Any<Guid>(), CancellationToken.None).Returns(saleItems.ToArray());
        _itemRepository.GetItemsPriceByIdAsync(Arg.Any<Guid[]>(), CancellationToken.None).Returns(itemsPrices);

        // Act
        var updateSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _saleRepository.Received(1).UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        await _saleItemRepository.Received(1).UpdateAsync(Arg.Any<SaleItem[]>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given update and add item When updating sale Then returns success response")]
    public async Task Handle_UpdateAndAddSaleItemRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var command = UpdateSaleHandlerTestData.GenerateValidCommand();
        var sale = SaleTestData.GenerateValidSale();
        sale.Id = command.Id;

        command.SaleItens = new List<UpdateSaleItemCommand>()
        {
            UpdateSaleHandlerTestData.GenerateValidItemCommand(),
            UpdateSaleHandlerTestData.GenerateValidItemCommand()
        }.ToArray();

        var saleItem1 = SaleItemTestData.GenerateValidSaleItem();
        saleItem1.ItemId = command.SaleItens.First().ItemId;
        var saleItems = new List<SaleItem>() { saleItem1 };

        var itemsPrices = new Dictionary<Guid, decimal>();
        itemsPrices.Add(command.SaleItens.First().ItemId, 10m);

        _saleRepository.GetByIdWithTrackingAsync(Arg.Any<Guid>(), CancellationToken.None).Returns(sale);
        _saleItemRepository.GetBySaleIdAsync(Arg.Any<Guid>(), CancellationToken.None).Returns(saleItems.ToArray());
        _itemRepository.GetItemsPriceByIdAsync(Arg.Any<Guid[]>(), CancellationToken.None).Returns(itemsPrices);

        // Act
        var updateSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _saleRepository.Received(1).UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        await _saleItemRepository.Received(1).UpdateAsync(Arg.Any<SaleItem[]>(), Arg.Any<CancellationToken>());
        await _saleItemRepository.Received(1).RegisterSaleItensAsync(Arg.Any<SaleItem[]>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given update and remove item When updating sale Then returns success response")]
    public async Task Handle_UpdateAndRemoveSaleItemRequest_ReturnsSuccessResponse()
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
        command.SaleItens = commandItems.ToArray();

        var itemsPrices = new Dictionary<Guid, decimal>();
        itemsPrices.Add(saleItems.First().ItemId, 10m);

        _saleRepository.GetByIdWithTrackingAsync(Arg.Any<Guid>(), CancellationToken.None).Returns(sale);
        _saleItemRepository.GetBySaleIdAsync(Arg.Any<Guid>(), CancellationToken.None).Returns(saleItems.ToArray());
        _itemRepository.GetItemsPriceByIdAsync(Arg.Any<Guid[]>(), CancellationToken.None).Returns(itemsPrices);

        // Act
        var updateSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _saleRepository.Received(1).UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        await _saleItemRepository.Received(1).UpdateAsync(Arg.Any<SaleItem[]>(), Arg.Any<CancellationToken>());
        await _saleItemRepository.Received(1).DeleteAsync(Arg.Any<SaleItem[]>(), Arg.Any<CancellationToken>());
    }
}
