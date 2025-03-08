using Ambev.DeveloperEvaluation.Domain.Entities;
using CSharpFunctionalExtensions;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Customer entity operations
/// </summary>
public interface ICustomerRepository
{

    /// <summary>
    /// Retrieves a customer by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the customer</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The customer if found, Maybe otherwise</returns>
    Task<Maybe<Customer>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
