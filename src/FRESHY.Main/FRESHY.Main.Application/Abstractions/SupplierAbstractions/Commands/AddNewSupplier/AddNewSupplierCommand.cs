using FluentValidation;
using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Models.Wrappers;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.SupplierAggregate;
using FRESHY.Main.Domain.Models.Aggregates.SupplierAggregate.Events;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.SupplierAbstractions.Commands.AddNewSupplier;

public record AddNewSupplierCommand
(
    string Name,
    string FeatureImage,
    string? Description,
    bool IsValid,
    Guid EmployeeId
) : ICommand<CommandResult>;

public class AddNewSupplierCommandValidator : AbstractValidator<AddNewSupplierCommand>
{
    public AddNewSupplierCommandValidator()
    {
        RuleFor(command => command.Name).MinimumLength(2);
    }
}

public class AddNewSupplierCommandHandler : ICommandHandler<AddNewSupplierCommand, CommandResult>
{
    private readonly ISupplierRepository _supplierRepository;

    public AddNewSupplierCommandHandler(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public async Task<CommandResult> Handle(AddNewSupplierCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _supplierRepository.UnitOfWork.BeginTransaction();

            var supplier = Supplier.Create(
                request.Name,
                request.FeatureImage,
                request.Description,
                request.IsValid
            );

            var @event = new SupplierAdded(
                  supplier.Id.Value,
                  supplier.Id.Value,
                  request.EmployeeId
              );

            supplier.AddDomainEvent(@event);
            await _supplierRepository.InsertAsync(supplier);
            await _supplierRepository.UnitOfWork.Commit(cancellationToken);

            return new CommandResult();
        }
        catch (Exception e)
        {
            return new CommandResult(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}