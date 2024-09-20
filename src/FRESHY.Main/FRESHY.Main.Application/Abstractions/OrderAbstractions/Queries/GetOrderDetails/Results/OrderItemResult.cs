namespace FRESHY.Main.Application.Abstractions.OrderAbstractions.Queries.GetOrderDetails.Results;

public record OrderItemResult
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