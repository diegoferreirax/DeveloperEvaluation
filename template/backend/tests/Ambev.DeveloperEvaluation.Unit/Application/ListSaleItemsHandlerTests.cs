using Ambev.DeveloperEvaluation.Application.Sales.ListSaleItems;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using AutoMapper;
using CSharpFunctionalExtensions;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class ListSaleItemsHandlerTests
{
    private readonly IMapper _mapper;
    private readonly ISaleItemRepository _saleItemRepository;
    private readonly ISaleRepository _saleRepository;
    private readonly ListSaleItemsHandler _handler;

    public ListSaleItemsHandlerTests()
    {
        _mapper = Substitute.For<IMapper>();
        _saleItemRepository = Substitute.For<ISaleItemRepository>();
        _saleRepository = Substitute.For<ISaleRepository>();
        _handler = new ListSaleItemsHandler(_mapper, _saleItemRepository, _saleRepository);
    }

    [Fact(DisplayName = "Given valid sale items When listing items Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var command = ListSaleItemsHandlerTestData.GenerateValidCommand();

        _saleRepository.GetByIdAsync(Arg.Any<Guid>(), CancellationToken.None).Returns(SaleTestData.GenerateValidSale());

        var salesItems = SaleItemTestData.GenerateValidSaleItemsList();
        _saleItemRepository.GetBySaleWithItemIdAsync(Arg.Any<Guid>(), CancellationToken.None).Returns(salesItems);

        var salesResult = GetSalesItemsResult(salesItems);
        _mapper.Map<ListSaleItemsResult[]>(salesItems).Returns(salesResult);

        // Act
        var listSalesResult = await _handler.Handle(command, CancellationToken.None);

        // Assert
        listSalesResult.Should().NotBeNull();
    }

    [Fact(DisplayName = "Given error When sale not found Then returns error response")]
    public async Task Handle_SaleNotFound_ReturnsErroResponse()
    {
        // Arrange
        var command = ListSaleItemsHandlerTestData.GenerateValidCommand();

        _saleRepository.GetByIdAsync(Arg.Any<Guid>(), CancellationToken.None).Returns(Maybe.None);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }

    private ListSaleItemsResult[] GetSalesItemsResult(SaleItem[] saleItems)
    {
        return saleItems.Select(s => _mapper.Map<ListSaleItemsResult>(s)).ToArray();
    }
}
