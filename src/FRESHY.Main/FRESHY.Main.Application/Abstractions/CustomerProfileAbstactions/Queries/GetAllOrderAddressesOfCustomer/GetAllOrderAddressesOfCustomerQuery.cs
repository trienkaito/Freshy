using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Interfaces;
using FRESHY.Main.Application.Abstractions.CustomerProfileAbstactions.Queries.GetAllOrderAddressesOfCustomer.Results;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Errors;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.CustomerProfileAbstactions.Queries.GetAllOrderAddressesOfCustomer;

public record GetAllOrderAddressesOfCustomerQuery
(
    Guid CustomerId
) : IQuery<QueryResult<IEnumerable<AllOrderAddressesOfCustomerResult>>>;

public class GetAllOrderAddressesOfCustomerQueryHandler : IQueryHandler<GetAllOrderAddressesOfCustomerQuery, QueryResult<IEnumerable<AllOrderAddressesOfCustomerResult>>>
{
    private readonly IOrderAddressRepository _orderAddressRepository;

    public GetAllOrderAddressesOfCustomerQueryHandler(IOrderAddressRepository orderAddressRepository)
    {
        _orderAddressRepository = orderAddressRepository;
    }

    public async Task<QueryResult<IEnumerable<AllOrderAddressesOfCustomerResult>>> Handle(GetAllOrderAddressesOfCustomerQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var orderAddresses = await _orderAddressRepository.GetOrderAddressesOfCustomer(CustomerId.Create(request.CustomerId), address => new
            {
                address.Id,
                address.Name,
                address.PhoneNumber,
                address.Country,
                address.City,
                address.District,
                address.Ward,
                address.Details,
                address.IsDefaultAddress
            });

            if (orderAddresses is not null)
            {
                var data = orderAddresses.Select(address => new AllOrderAddressesOfCustomerResult(
                    address.Id.Value,
                    address.Name,
                    address.PhoneNumber,
                    address.Country,
                    address.City,
                    address.District,
                    address.Ward,
                    address.Details,
                    address.IsDefaultAddress
                )).ToList();

                return new QueryResult<IEnumerable<AllOrderAddressesOfCustomerResult>>(data);
            }
            return new QueryResult<IEnumerable<AllOrderAddressesOfCustomerResult>>(HttpStatusCode.NoContent, Error.NO_CONTENT);
        }
        catch (Exception e)
        {
            return new QueryResult<IEnumerable<AllOrderAddressesOfCustomerResult>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}