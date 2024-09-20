using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Models.Wrappers;
using FRESHY.Main.Application.Abstractions.CustomerProfileAbstactions.Queries.GetAllOrderAddressesOfCustomer.Results;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.Entities;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.Events;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.EmployeeProfileAbstractions.CustomerProfileAbstactions.Commands.CreateCustomerProfile;

public record CreateCustomerProfileCommand
(
    Guid AccountId,
    string? Email,
    string? Phone,
    string Name
) : ICommand<ReturnCommandResult<CustomerResutl>>;

public record CustomerResutl
(
        Guid CustomerId,
        string Name,
        string Avatar,
        Guid AccountId,
        string? Email,
        string? Phone,
        List<AllOrderAddressesOfCustomerResult>? AllOrderAddresses
 );


public class CreateProfileCommandHandler : ICommandHandler<CreateCustomerProfileCommand, ReturnCommandResult<CustomerResutl>>
{
    private readonly ICustomerProfileRepository _customerProfileRepository;
    private readonly ICartRepository _cartRepository;

    public CreateProfileCommandHandler(
        ICustomerProfileRepository customerProfileRepository,
        ICartRepository cartRepository)
    {
        _customerProfileRepository = customerProfileRepository;
        _cartRepository = cartRepository;
    }

    public async Task<ReturnCommandResult<CustomerResutl>> Handle(CreateCustomerProfileCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _customerProfileRepository.UnitOfWork.BeginTransaction();

            var customerProfile = Customer.Create(
                request.Name,
                request.AccountId,
                request.Email,
                request.Phone
            );

            var cart = Cart.Create(
                customerProfile.Id
            );

            var @event = new CustomerProfileCreated(
                customerProfile.Id.Value,
                customerProfile.AccountId);

            customerProfile.AddDomainEvent(@event);

            await _cartRepository.InsertAsync(cart);
            await _customerProfileRepository.InsertAsync(customerProfile);
            await _customerProfileRepository.UnitOfWork.Commit(cancellationToken);

            var data = new CustomerResutl(
                customerProfile.Id.Value,
                customerProfile.Name,
                customerProfile.Avatar,
                customerProfile.AccountId,
                customerProfile.Email,
                customerProfile.Phone,
                null);
            return new ReturnCommandResult<CustomerResutl>(data);
            
        }
        catch (Exception e)
        {
            return new ReturnCommandResult<CustomerResutl>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}