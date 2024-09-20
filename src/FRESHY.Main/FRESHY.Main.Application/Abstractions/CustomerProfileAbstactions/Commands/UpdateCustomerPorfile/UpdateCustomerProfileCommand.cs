using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Models.Wrappers;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Errors;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.CustomerProfileAbstactions.Commands.UpdateCustomerPorfile;

public record UpdateCustomerProfileCommand
(
    Guid CustomerId,
    string Name,
    string Avatar
) : ICommand<CommandResult>;

public class UpdateCustomerProfileCommandHandler : ICommandHandler<UpdateCustomerProfileCommand, CommandResult>
{
    private readonly ICustomerProfileRepository _customerProfileRepository;

    public UpdateCustomerProfileCommandHandler(ICustomerProfileRepository customerProfileRepository)
    {
        _customerProfileRepository = customerProfileRepository;
    }

    public async Task<CommandResult> Handle(UpdateCustomerProfileCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _customerProfileRepository.UnitOfWork.BeginTransaction();

            var customer = await _customerProfileRepository.GetByIdAsync(CustomerId.Create(request.CustomerId));

            if (customer is not null)
            {
                customer.UpdateCustomerInfos(
                    request.Name,
                    request.Avatar
                );

                await _customerProfileRepository.UnitOfWork.Commit(cancellationToken);
                return new CommandResult();
            }

            return new CommandResult(HttpStatusCode.NotFound, Error.CUSTOMER_NOT_FOUND);
        }
        catch (Exception e)
        {
            return new CommandResult(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}