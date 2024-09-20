namespace FRESHY.Main.Contract.Requests.ShippingRequests;

public record CreateNewShippingPartnerRequest
(
    string Name,
    string FeatureImage,
    string Description,
    double ShippingPrice,
    string Address
);