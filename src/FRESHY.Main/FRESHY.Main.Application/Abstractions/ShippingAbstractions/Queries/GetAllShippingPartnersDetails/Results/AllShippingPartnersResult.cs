namespace FRESHY.Main.Application.Abstractions.ShippingAbstractions.Queries.GetAllShippingPartnersDetails.Results;

public record AllShippingPartnersResult
(
    Guid Id,
    string Name,
    string FeatureImage,
    string Description,
    double ShippingPrice,
    string Address,
    DateTime JoinedDate
);