using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Newtonsoft.Json;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleTests
{
    /// <summary>
    /// Tests that validation passes when all sale properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid sale data")]
    public void Given_ValidSaleData_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();

        // Act
        var result = sale.Validate();
        Console.WriteLine(JsonConvert.SerializeObject(result));

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when sale properties are invalid.
    /// </summary>
    [Theory(DisplayName = "Validation should fail for invalid sale data")]
    [InlineData(0, 23, "Branch")]
    [InlineData(1, 0, "Branch")]
    [InlineData(1, 23, "")]
    public void Given_InvalidUserData_When_Validated_Then_ShouldReturnInvalid(
        int saleNumber, 
        decimal totalAmount, 
        string branch)
    {
        // Arrange
        var sale = new Sale
        {
            SaleNumber = saleNumber,
            TotalAmount = totalAmount,
            Branch = branch,

            // TODO: validar demais campos
            CustomerId = Guid.NewGuid(),
            Customer = new Customer(),
            SaleDate = DateTime.Now,
            IsCanceled = false

        };

        // Act
        var result = sale.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }
}
