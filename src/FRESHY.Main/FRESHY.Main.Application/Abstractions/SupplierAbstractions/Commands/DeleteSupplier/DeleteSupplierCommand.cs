using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.SupplierAggregate.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRESHY.Main.Application.Abstractions.SupplierAbstractions.Commands.DeleteSupplier
{
    public record DeleteSupplierCommand
    (
        Guid SupplierId
    ):ICommand;
    public class DeleteSupplierCommandHandler : ICommandHandler<DeleteSupplierCommand>
    {
        private readonly ISupplierRepository _supplierRepository;

        public DeleteSupplierCommandHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            _supplierRepository.UnitOfWork.BeginTransaction();
            var supplier = await _supplierRepository.GetByIdAsync(SupplierId.Create(request.SupplierId));
            if (supplier is not null)
            {
                _supplierRepository.Delete(supplier);
                await _supplierRepository.UnitOfWork.Commit(cancellationToken);
            }
        }
    }
}
