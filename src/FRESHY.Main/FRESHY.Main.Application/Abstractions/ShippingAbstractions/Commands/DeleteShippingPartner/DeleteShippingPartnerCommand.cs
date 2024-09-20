using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.ShippingAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRESHY.Main.Application.Abstractions.ShippingAbstractions.Commands.DeleteShippingPartner
{
    public record DeleteShippingPartnerCommand
        (
            Guid ShippingId
        ):ICommand;
    public class DeleteShippingPartnerCommandHandler : ICommandHandler<DeleteShippingPartnerCommand>
    {   
        private readonly IShippingRepository _shippingRepository;
        public DeleteShippingPartnerCommandHandler(IShippingRepository shippingRepository)
        {
            _shippingRepository = shippingRepository;
        }

        public async Task Handle(DeleteShippingPartnerCommand request, CancellationToken cancellationToken)
        {
            _shippingRepository.UnitOfWork.BeginTransaction();
            var shipping = await _shippingRepository.GetByIdAsync(ShippingId.Create(request.ShippingId));
            if (shipping is not null)
            {
                _shippingRepository.Delete(shipping);
                await _shippingRepository.UnitOfWork.Commit(cancellationToken);
            }
        }
    }
}
