public record ListSalesResponse
(
    SalesResponse[] Sales
);

public record SalesResponse
(
    Guid Id,
    Guid CustomerId,
    int SaleNumber,
    DateTime SaleDate,
    bool IsCanceled,
    string Branch
);
