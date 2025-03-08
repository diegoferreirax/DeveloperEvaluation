using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/*
 TODO: 
    contrutores privados
    validações
 */

/// <summary>
/// Represents a customer in the system with name information.
/// </summary>
public class Customer : BaseEntity
{
    /// <summary>
    /// Gets the customer's name.
    /// Must not be null or empty and should contain both first and last names.
    /// </summary>
    public string Name { get; set; }
}