using System.Net;
using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Interfaces;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Errors;

namespace FRESHY.Main.Application.Abstractions.CustomerProfileAbstactions.Queries.GetCustomerByAccountId;

public record GetCustomerByAccountIdQuery
(
    Guid AccountId
) : IQuery<QueryResult<GetCustomerByAccountIdResult>>;

public record GetCustomerByAccountIdResult
(
    Guid CustomerId
);

public class GetCustomerByAccountIdQueryHandler : IQueryHandler<GetCustomerByAccountIdQuery, QueryResult<GetCustomerByAccountIdResult>>
{
    private readonly ICustomerProfileRepository _customerProfileRepository;

    public GetCustomerByAccountIdQueryHandler(ICustomerProfileRepository customerProfileRepository)
    {
        _customerProfileRepository = customerProfileRepository;
    }

    public async Task<QueryResult<GetCustomerByAccountIdResult>> Handle(GetCustomerByAccountIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var customerId = await _customerProfileRepository.GetCustomerIdByAccountId(request.AccountId);  
            
            if (customerId is not null)
            {
                var data = new GetCustomerByAccountIdResult(customerId.Value);
                return new QueryResult<GetCustomerByAccountIdResult>(data);
            }
            return new QueryResult<GetCustomerByAccountIdResult>(HttpStatusCode.NotFound, Error.NOT_FOUND);
        }
        catch (Exception e)
        {
            return new QueryResult<GetCustomerByAccountIdResult>(HttpStatusCode.NotFound, e.Message);
        }
    }
}