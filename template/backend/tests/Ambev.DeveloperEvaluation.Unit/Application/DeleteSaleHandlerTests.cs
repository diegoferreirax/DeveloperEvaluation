using Ambev.DeveloperEvaluation.Application.Sales.v1.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Unit tests for the DeleteSaleHandler.
/// </summary>
public class DeleteSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly DeleteSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the DeleteSaleHandlerTests
    /// </summary>
    public DeleteSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _handler = new DeleteSaleHandler(_saleRepository);
    }

    /// <summary>
    /// Tests if deleting a sale with a valid ID returns a success response.
    /// </summary>
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

    /// <summary>
    /// Tests if deleting a sale with an invalid ID throws a KeyNotFoundException
    /// </summary>
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
