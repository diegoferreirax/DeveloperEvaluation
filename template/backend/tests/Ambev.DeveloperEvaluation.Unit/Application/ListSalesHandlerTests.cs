using Ambev.DeveloperEvaluation.Application.Sales.v1.ListSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class ListSalesHandlerTests
{
    private readonly IMapper _mapper;
    private readonly ISaleRepository _saleRepository;
    private readonly ListSalesHandler _handler;

    public ListSalesHandlerTests()
    {
        _mapper = Substitute.For<IMapper>();
        _saleRepository = Substitute.For<ISaleRepository>();
        _handler = new ListSalesHandler(_mapper, _saleRepository);
    }

    [Fact(DisplayName = "Given valid sales When listing sales Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var command = ListSalesHandlerTestData.GenerateValidCommand();

        var sales = new List<Sale>()
        {
            SaleTestData.GenerateValidSale(),
            SaleTestData.GenerateValidSale()
        }.ToArray();

        (Sale[], int) result = (sales, sales.Length);
        _saleRepository.ListSalesAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>(), CancellationToken.None).Returns(result);

        var salesResult = GetSalesResult(sales);
        _mapper.Map<SalesResult[]>(sales).Returns(salesResult);

        // Act
        var listSalesResult = await _handler.Handle(command, CancellationToken.None);

        // Assert
        listSalesResult.Should().NotBeNull();
    }

    private SalesResult[] GetSalesResult(Sale[] sales)
    {
        return sales.Select(s => _mapper.Map<SalesResult>(s)).ToArray();
    }
}
