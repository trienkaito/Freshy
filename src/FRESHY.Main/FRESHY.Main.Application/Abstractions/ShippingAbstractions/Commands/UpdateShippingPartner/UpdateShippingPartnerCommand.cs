using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.ShippingAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRESHY.Main.Application.Abstractions.ShippingAbstractions.Commands.UpdateShippingPartner
{
    public record UpdateShippingPartnerCommand
    (
        Guid ShippingId,
        string Name,
        string Description,
        string FeatureImage,
        double ShippingPrice,
        string Address
    ) : ICommand;
    public class UpdateShippingPartnerCommandHandler : ICommandHandler<UpdateShippingPartnerCommand>
    {
        private readonly IShippingRepository _shippingRepository;
        public UpdateShippingPartnerCommandHandler(IShippingRepository shippingRepository)
        {
            _shippingRepository = shippingRepository;
        }
        public async Task Handle(UpdateShippingPartnerCommand request, CancellationToken cancellationToken )
        {
            _shippingRepository.UnitOfWork.BeginTransaction();
            var shipping = await _shippingRepository.GetByIdAsync(ShippingId.Create(request.ShippingId));
            if (shipping is not null)
            {/*
                shipping.Cre
                    (
                        request.Name,
                        request.Description,
                        request.FeatureImage,
                        request.ShippingPrice,
                        request.Address
                    );*/
                await _shippingRepository.UnitOfWork.Commit(cancellationToken );
            }
        }
    }
}
