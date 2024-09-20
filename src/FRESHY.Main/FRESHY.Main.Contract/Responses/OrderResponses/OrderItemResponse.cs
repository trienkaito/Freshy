namespace FRESHY.Main.Contract.Responses.OrderResponses;

public record OrderItemResponse
(
    Guid ProductId,
    Guid OrderItemId,
    string Name,
    string TypeName,
    string UnitType,
    double PricePerUnit,
    string FeatureImage,
    int BoughtQuantity,
    double TotalPrice
);