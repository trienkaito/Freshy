using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Models.Wrappers;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.ProductTypeAggregate.ValueObjects;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.ProductTypeAbstractions.Commands.UpdateProductType;

public record UpdateProductTypeCommand
(
    Guid TypeId,
    string Name
) : ICommand<CommandResult>;

public class UpdateProductTypeCommandHandler : ICommandHandler<UpdateProductTypeCommand, CommandResult>
{
    private readonly IProductTypeRepository _productTypeRepository;

    public UpdateProductTypeCommandHandler(IProductTypeRepository productTypeRepository)
    {
        _productTypeRepository = productTypeRepository;
    }

    public async Task<CommandResult> Handle(UpdateProductTypeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _productTypeRepository.UnitOfWork.BeginTransaction();

            var productType = await _productTypeRepository.GetByIdAsync(ProductTypeId.Create(request.TypeId));

            if (productType is not null)
            {
                productType.UpdateProductType(request.Name);
                await _productTypeRepository.UnitOfWork.Commit(cancellationToken);

                return new CommandResult();
            }
            return new CommandResult(HttpStatusCode.NotFound, "NOT_FOUND");
        }
        catch (Exception e)
        {
            return new CommandResult(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}