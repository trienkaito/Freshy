namespace FRESHY.Main.Contract.Responses.CartItemResponses;

public record ProductUnitForCustomerResponse
(
    string UnitFeatureImage,
    string Type,
    double Value,
    double SellPrice
);