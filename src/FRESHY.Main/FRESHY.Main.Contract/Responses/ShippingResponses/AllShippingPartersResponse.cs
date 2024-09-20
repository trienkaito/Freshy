namespace FRESHY.Main.Contract.Responses.ShippingResponses;

public record AllShippingPartersResponse
(
    Guid Id,
    string Name,
    string FeatureImage,
    string Description,
    double ShippingPrice,
    string Address,
    DateTime JoinedDate
);