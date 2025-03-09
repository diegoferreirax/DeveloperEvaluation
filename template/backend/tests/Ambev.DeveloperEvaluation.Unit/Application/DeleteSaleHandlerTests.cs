using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class DeleteSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly DeleteSaleHandler _handler;

    public DeleteSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _handler = new DeleteSaleHandler(_saleRepository);
    }

    [Fact(DisplayName = "Given valid sale Id When deleting sale Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Arrange
        _saleRepository.DeleteAsync(Arg.Any<Guid>(), CancellationToken.None).Returns(true);

        // Act
        var registerSaleResult = await _handler.Handle(new DeleteSaleCommand { Id = Guid.NewGuid() }, CancellationToken.None);

        // Assert
        registerSaleResult.Should().NotBeNull();
        registerSaleResult.Success.Should().Be(true);
        await _saleRepository.Received(1).DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid sale Id When deleting sale Then returns error response")]
    public async Task Handle_InvalidRequest_ReturnsErrorResponse()
    {
        // Arrange
        var id = Guid.NewGuid();
        _saleRepository.DeleteAsync(Arg.Any<Guid>(), CancellationToken.None).Returns(false);

        // Act
        var act = () => _handler.Handle(new DeleteSaleCommand { Id = id }, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage($"Sale with ID {id} not found");
    }

}
