using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.SupplierAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRESHY.Main.Application.Abstractions.SupplierAbstractions.Commands.UpdateSupplier
{
    public record UpdateSupplierCommand
    (
        Guid SupplierId,
        string Name,
        string FeatureImage,
        string? Description,
        bool IsValid
    ) : ICommand;
    public class UpdateSupplierCommandHandler : ICommandHandler<UpdateSupplierCommand>
    {
        private readonly ISupplierRepository _supplierRepository;

        public UpdateSupplierCommandHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            _supplierRepository.UnitOfWork.BeginTransaction();
            var supplier = await _supplierRepository.GetByIdAsync(SupplierId.Create(request.SupplierId));
            if (supplier is not null)
            {
               /* supplier.UpdateSupplier
                    (
                    request.Name,
                    request.FeatureImage,
                    request.Description,
                    request.IsValid
                    ) ;
                await _supplierRepository.UnitOfWork.Commit(cancellationToken);*/
            }
        }
    }
}
