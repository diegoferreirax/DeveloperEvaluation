/// <summary>
/// Represents the response containing a list of sales.
/// Contains an array of SalesResponse objects, each representing the details of a sale.
/// </summary>
public record ListSalesResponse
(
    /// <summary>
    /// Gets the list of sales, where each sale provides detailed information about a particular sale transaction.
    /// </summary>
    IEnumerable<SalesResponse> Sales
);

/// <summary>
/// Represents the details of a single sale transaction.
/// Contains information such as sale ID, customer ID, sale number, sale date, cancellation status, and branch.
/// </summary>
public record SalesResponse
(
    /// <summary>
    /// Gets the unique identifier of the sale.
    /// </summary>
    Guid Id,

    /// <summary>
    /// Gets the unique identifier of the customer associated with the sale.
    /// </summary>
    Guid CustomerId,

    /// <summary>
    /// Gets the sale number, which is a unique identifier for the sale within a branch.
    /// </summary>
    int SaleNumber,

    /// <summary>
    /// Gets the date and time when the sale occurred.
    /// </summary>
    DateTime SaleDate,

    /// <summary>
    /// Gets a value indicating whether the sale has been canceled.
    /// </summary>
    bool IsCanceled,

    /// <summary>
    /// Gets the branch where the sale took place.
    /// </summary>
    string Branch,

    /// <summary>
    /// Gets the total amount of the sale.
    /// </summary>
    decimal TotalAmount
);
