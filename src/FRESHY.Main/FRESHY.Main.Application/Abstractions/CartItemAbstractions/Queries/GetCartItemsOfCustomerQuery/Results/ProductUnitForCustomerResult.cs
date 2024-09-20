namespace FRESHY.Main.Application.Abstractions.CartItemAbstractions.Queries.GetCartItemsOfCustomerQuery.Results;

public record ProductUnitForCustomerResult
(
    string UnitFeatureImage,
    string Type,
    double Value,
    double SellPrice
);