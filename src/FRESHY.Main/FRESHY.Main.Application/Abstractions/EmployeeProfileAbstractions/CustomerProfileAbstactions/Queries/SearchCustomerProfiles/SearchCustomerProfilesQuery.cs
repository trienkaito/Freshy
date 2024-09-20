using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Interfaces;
using FRESHY.Main.Application.Abstractions.EmployeeProfileAbstractions.CustomerProfileAbstactions.Queries.GetAllCustomerProfiles.Results;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate.Helpers;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.EmployeeProfileAbstractions.CustomerProfileAbstactions.Queries.SearchCustomerProfiles;

public record SearchCustomerProfilesQuery
(
    string? Email,
    string? Phone,
    int PageNumber,
    int PageSize
) : IQuery<PageQueryResult<IEnumerable<AllCustomerProfilesResult>>>;

public class SearchCustomerProfilesQueryHandler : IQueryHandler<SearchCustomerProfilesQuery, PageQueryResult<IEnumerable<AllCustomerProfilesResult>>>
{
    private readonly ICustomerProfileRepository _customerProfileRepository;
    private readonly IOrderRepository _orderRepository;

    public SearchCustomerProfilesQueryHandler(
        ICustomerProfileRepository customerProfileRepository,
        IOrderRepository orderRepository)
    {
        _customerProfileRepository = customerProfileRepository;
        _orderRepository = orderRepository;
    }

    public async Task<PageQueryResult<IEnumerable<AllCustomerProfilesResult>>> Handle(SearchCustomerProfilesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var customers = await _customerProfileRepository.GetAllAsync(customer => new
            {
                customer.Id,
                customer.AccountId,
                customer.Avatar,
                customer.Name,
                customer.Email,
                customer.Phone,
                customer.CreatedDate
            });

            var data = customers.Select(customer =>
            {
                var orders = _orderRepository.GetOrdersOfACustomerByCustomerIdAsync(customer.Id, order => new
                {
                    order.Id,
                    order.PaidAmount
                }).Result;

                return new AllCustomerProfilesResult(
                    customer.AccountId,
                    customer.Id.Value,
                    customer.Email is null ? CredentialStatus.EMAIL_NOT_REGISTERED : customer.Email,
                    customer.Phone is null ? CredentialStatus.PHONE_NOT_REGISTERED : customer.Phone,
                    customer.Name,
                    customer.Avatar,
                    customer.CreatedDate,
                    orders is not null ? orders.Select(order => order!.PaidAmount).Sum() : 0
                );
            })
            .ToList()
            .Where(customer =>
                (string.IsNullOrEmpty(request.Email) || customer.Email.Contains(request.Email, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(request.Phone) || customer.Phone.Contains(request.Phone, StringComparison.OrdinalIgnoreCase))
            ).ToList();

            int totalPage = (int)Math.Ceiling((double)data.Count / request.PageSize);
            return new PageQueryResult<IEnumerable<AllCustomerProfilesResult>>(data, request.PageNumber, request.PageSize, totalPage);
        }
        catch (Exception e)
        {
            return new PageQueryResult<IEnumerable<AllCustomerProfilesResult>>(request.PageNumber, request.PageSize, HttpStatusCode.InternalServerError, e.Message);
        }
    }
}