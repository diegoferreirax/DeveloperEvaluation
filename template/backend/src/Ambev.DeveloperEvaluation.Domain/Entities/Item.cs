using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/*
 TODO: 
    contrutores privados
    validações
 */

/// <summary>
/// Represents a item in the system with name and price information.
/// </summary>
public class Item : BaseEntity
{
    /// <summary>
    /// Gets the product's name.
    /// Must not be null or empty and should contain first name.
    /// </summary>
    public string Product { get; set; }

    /// <summary>
    /// Gets the product's price.
    /// Must not be null or 0 and should contain a price number.
    /// </summary>
    public decimal UnitPrice { get; set; }
}