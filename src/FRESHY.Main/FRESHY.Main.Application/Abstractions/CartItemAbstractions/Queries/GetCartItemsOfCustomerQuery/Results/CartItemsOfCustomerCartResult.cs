namespace FRESHY.Main.Application.Abstractions.CartItemAbstractions.Queries.GetCartItemsOfCustomerQuery.Results;

public record CartItemsOfCustomerCartResult
(
    Guid CartItemId,
    Guid ProductId,
    Guid ProductUnitId,
    string ProductName,
    ProductUnitForCustomerResult ProductUnit,
    int BoughtQuantity,
    double TotalPrice
);