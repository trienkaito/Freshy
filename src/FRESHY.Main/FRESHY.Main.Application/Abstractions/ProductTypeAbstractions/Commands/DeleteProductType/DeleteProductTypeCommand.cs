using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Models.Wrappers;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.ProductTypeAggregate.ValueObjects;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.ProductTypeAbstractions.Commands.DeleteProductType;

public record DeleteProductTypeCommand
(
    Guid ProductTypeId
) : ICommand<CommandResult>;

public class DeleteProductTypeCommandHandler : ICommandHandler<DeleteProductTypeCommand, CommandResult>
{
    private readonly IProductTypeRepository _productTypeRepository;

    public DeleteProductTypeCommandHandler(IProductTypeRepository productTypeRepository)
    {
        _productTypeRepository = productTypeRepository;
    }

    public async Task<CommandResult> Handle(DeleteProductTypeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _productTypeRepository.UnitOfWork.BeginTransaction();

            var productType = await _productTypeRepository.GetByIdAsync(ProductTypeId.Create(request.ProductTypeId));

            if (productType is not null)
            {
                _productTypeRepository.Delete(productType);
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