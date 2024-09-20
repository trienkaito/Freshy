using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Interfaces;
using FRESHY.Main.Application.Abstractions.CustomerProfileAbstactions.Queries.GetCustomerProfileDetails.Results;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Errors;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.CustomerProfileAbstactions.Queries.GetCustomerProfileDetails;

public record GetCustomerProfileDetailsQuery
(
    Guid CustomerId
) : IQuery<QueryResult<CustomerProfileDetailsResult>>;

public class GetCustomerProfileDetailsQueryHandler : IQueryHandler<GetCustomerProfileDetailsQuery, QueryResult<CustomerProfileDetailsResult>>
{
    private readonly ICustomerProfileRepository _customerProfileRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IOrderAddressRepository _orderAddressRepository;

    public GetCustomerProfileDetailsQueryHandler(
        ICustomerProfileRepository customerProfileRepository,
        IOrderRepository orderRepository,
        IReviewRepository reviewRepository,
        IOrderAddressRepository orderAddressRepository)
    {
        _customerProfileRepository = customerProfileRepository;
        _orderRepository = orderRepository;
        _reviewRepository = reviewRepository;
        _orderAddressRepository = orderAddressRepository;
    }

    public async Task<QueryResult<CustomerProfileDetailsResult>> Handle(GetCustomerProfileDetailsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var customer = await _customerProfileRepository.GetByIdAsync(CustomerId.Create(request.CustomerId), customer => new
            {
                customer.Id,
                customer.Name,
                customer.Avatar,
                customer.AccountId,
                customer.CreatedDate,
                
            });

            if (customer is not null)
            {
                var orders = await _orderRepository.GetOrdersOfACustomerByCustomerIdAsync(customer.Id, order => new
                {
                    order.Id
                });

                var addresses = await _orderAddressRepository.GetOrderAddressesOfCustomer(customer.Id, address => new
                {
                    address.Id
                });

                var data = new CustomerProfileDetailsResult(
                    customer.Name,
                    customer.Avatar,
                    customer.AccountId,
                    customer.CreatedDate,
                    addresses is null ? 0 : addresses.Count(),
                    orders is null ? 0 : orders.Count(),
                    0
                );
            }
            return new QueryResult<CustomerProfileDetailsResult>(HttpStatusCode.NotFound, Error.CUSTOMER_NOT_FOUND);
        }
        catch (Exception e)
        {
            return new QueryResult<CustomerProfileDetailsResult>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}