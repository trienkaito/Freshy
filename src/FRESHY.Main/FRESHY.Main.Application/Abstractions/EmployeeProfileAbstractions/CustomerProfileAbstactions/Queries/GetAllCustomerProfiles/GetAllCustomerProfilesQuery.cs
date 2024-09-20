using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Interfaces;
using FRESHY.Main.Application.Abstractions.EmployeeProfileAbstractions.CustomerProfileAbstactions.Queries.GetAllCustomerProfiles.Results;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate.Helpers;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.CustomerProfileAbstactions.Queries.GetAllCustomerProfile;

public record GetAllCustomerProfilesQuery
(
    int PageNumber,
    int PageSize
) : IQuery<PageQueryResult<IEnumerable<AllCustomerProfilesResult>>>;

public class GetAllCustomerProfileQueryHandler : IQueryHandler<GetAllCustomerProfilesQuery, PageQueryResult<IEnumerable<AllCustomerProfilesResult>>>
{
    private readonly ICustomerProfileRepository _customerProfileRepository;
    private readonly IOrderRepository _orderRepository;

    public GetAllCustomerProfileQueryHandler(
        ICustomerProfileRepository customerProfileRepository,
        IOrderRepository orderRepository)
    {
        _customerProfileRepository = customerProfileRepository;
        _orderRepository = orderRepository;
    }

    public async Task<PageQueryResult<IEnumerable<AllCustomerProfilesResult>>> Handle(GetAllCustomerProfilesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var customers = await _customerProfileRepository.GetByPagingAsync(request.PageNumber, request.PageSize, customer => new
            {
                customer.Id,
                customer.AccountId,
                customer.CreatedDate,
                customer.Name,
                customer.Avatar,
                customer.Email,
                customer.Phone
            });

            var allCustomers = await _customerProfileRepository.GetAllAsync(customer => new
            {
                customer.Id
            });

            int totalPage = (int)Math.Ceiling((double)allCustomers.Count() / request.PageSize);

            var data = customers.Select(profile =>
            {
                var orders = _orderRepository.GetOrdersOfACustomerByCustomerIdAsync(profile.Id, order => new
                {
                    order.PaidAmount
                }).Result;

                return new AllCustomerProfilesResult
                (
                    profile.AccountId,
                    profile.Id.Value,
                    profile.Email is null ? CredentialStatus.EMAIL_NOT_REGISTERED : profile.Email,
                    profile.Phone is null ? CredentialStatus.PHONE_NOT_REGISTERED : profile.Phone,
                    profile.Name,
                    profile.Avatar,
                    profile.CreatedDate,
                    orders is not null ? orders.Select(order => order!.PaidAmount).Sum() : 0
                );
            }).ToList();

            return new PageQueryResult<IEnumerable<AllCustomerProfilesResult>>(data, request.PageNumber, request.PageSize, totalPage);
        }
        catch (Exception e)
        {
            return new PageQueryResult<IEnumerable<AllCustomerProfilesResult>>(request.PageNumber, request.PageSize, HttpStatusCode.InternalServerError, e.Message);
        }
    }
}