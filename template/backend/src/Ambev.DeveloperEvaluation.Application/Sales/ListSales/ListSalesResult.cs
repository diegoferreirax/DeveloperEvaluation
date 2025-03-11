public record ListSalesResult
(
    int salesCount,
    SalesResult[] Sales
);

public record SalesResult
(
    Guid Id,
    Guid CustomerId,
    int SaleNumber,
    DateTime SaleDate,
    bool IsCanceled,
    string Branch

// TODO: retornar itens, 
//ListSalesItemsResult[] SaleItems
);

public record SalesItemsResult
(
    Guid ItemId,
    int Quantity,
    decimal Discount,
    decimal TotalItemAmount
);
