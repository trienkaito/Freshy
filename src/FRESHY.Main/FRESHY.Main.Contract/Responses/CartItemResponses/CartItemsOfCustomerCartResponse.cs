namespace FRESHY.Main.Contract.Responses.CartItemResponses;

public record CartItemsOfCustomerCartResponse
(
    Guid CartItemId,
    Guid ProductId,
    Guid ProductUnitId,
    string ProductName,
    ProductUnitForCustomerResponse ProductUnit,
    int BoughtQuantity,
    double TotalPrice
);