using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Models.Wrappers;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.ProductTypeAggregate;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.ProductTypeAbstractions.Commands.CreateProductType;

public record CreateProductTypeCommand
(
    string Name
) : ICommand<CommandResult>;

public class CrreateProductTypeCommandHandler : ICommandHandler<CreateProductTypeCommand, CommandResult>
{
    private readonly IProductTypeRepository _productTypeRepository;

    public CrreateProductTypeCommandHandler(IProductTypeRepository productTypeRepository)
    {
        _productTypeRepository = productTypeRepository;
    }

    public async Task<CommandResult> Handle(CreateProductTypeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _productTypeRepository.UnitOfWork.BeginTransaction();

            var type = ProductType.Create(request.Name);

            await _productTypeRepository.InsertAsync(type);
            await _productTypeRepository.UnitOfWork.Commit(cancellationToken);

            return new CommandResult();
        }
        catch (Exception e)
        {
            return new CommandResult(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}