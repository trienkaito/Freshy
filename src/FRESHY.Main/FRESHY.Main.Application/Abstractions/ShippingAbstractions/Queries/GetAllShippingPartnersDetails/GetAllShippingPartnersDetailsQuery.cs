using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Interfaces;
using FRESHY.Main.Application.Abstractions.ShippingAbstractions.Queries.GetAllShippingPartnersDetails.Results;
using FRESHY.Main.Application.Interfaces.Persistance;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.ShippingAbstractions.Queries.GetAllShippingPartnersDetails;

public record GetAllShippingPartnersDetailsQuery : IQuery<QueryResult<IEnumerable<AllShippingPartnersResult>>>;

public class GetAllShippingPartnersDetailsQueryHandler : IQueryHandler<GetAllShippingPartnersDetailsQuery, QueryResult<IEnumerable<AllShippingPartnersResult>>>
{
    private readonly IShippingRepository _shippingRepository;

    public GetAllShippingPartnersDetailsQueryHandler(IShippingRepository shippingRepository)
    {
        _shippingRepository = shippingRepository;
    }

    public async Task<QueryResult<IEnumerable<AllShippingPartnersResult>>> Handle(GetAllShippingPartnersDetailsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var shippings = await _shippingRepository.GetAllAsync(shipping => new
            {
                shipping.Id,
                shipping.Name,
                shipping.FeatureImage,
                shipping.Description,
                shipping.ShippingPrice,
                shipping.Address,
                shipping.JoinedDate
            });

            var data = shippings.Select(shipping => new AllShippingPartnersResult
            (
                shipping.Id.Value,
                shipping.Name,
                shipping.FeatureImage,
                shipping.Description,
                shipping.ShippingPrice,
                shipping.Address,
                shipping.JoinedDate
            )).ToList();

            return new QueryResult<IEnumerable<AllShippingPartnersResult>>(data);
        }
        catch (Exception e)
        {
            return new QueryResult<IEnumerable<AllShippingPartnersResult>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}