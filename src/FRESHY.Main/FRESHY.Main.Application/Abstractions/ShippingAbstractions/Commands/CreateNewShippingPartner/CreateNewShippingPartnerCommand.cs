using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Models.Wrappers;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.ShippingAggregate;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.ShippingAbstractions.Commands.CreateNewShippingPartner;

public record CreateNewShippingPartnerCommand
(
    string Name,
    string FeatureImage,
    string Description,
    double ShippingPrice,
    string Address
) : ICommand<CommandResult>;

public class CreateNewShippingPartnerCommandHandler : ICommandHandler<CreateNewShippingPartnerCommand, CommandResult>
{
    private readonly IShippingRepository _shippingRepository;

    public CreateNewShippingPartnerCommandHandler(IShippingRepository shippingRepository)
    {
        _shippingRepository = shippingRepository;
    }

    public async Task<CommandResult> Handle(CreateNewShippingPartnerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _shippingRepository.UnitOfWork.BeginTransaction();

            var shipping = Shipping.Create(
                request.Name,
                request.Description,
                request.FeatureImage,
                request.ShippingPrice,
                request.Address);

            await _shippingRepository.InsertAsync(shipping);
            await _shippingRepository.UnitOfWork.Commit(cancellationToken);

            return new CommandResult();
        }
        catch (Exception e)
        {
            return new CommandResult(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}