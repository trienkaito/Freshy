using System.Net;
using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Models.Wrappers;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate.ValueObjects;

namespace FRESHY.Main.Application.Abstractions.ProductLikeAbstractions.Commands;

public record DeleteProductLikeCommand
(
    Guid CustomerId,
    Guid ProductId
) : ICommand<CommandResult>;


public class DeleteProductLikeCommandHandler : ICommandHandler<DeleteProductLikeCommand, CommandResult>
{
    private readonly IProductLikerepository _productLikerepository;

    public DeleteProductLikeCommandHandler(IProductLikerepository productLikerepository)
    {
        _productLikerepository = productLikerepository;
    }

    public async Task<CommandResult> Handle(DeleteProductLikeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _productLikerepository.UnitOfWork.BeginTransaction();

            var like = await _productLikerepository.GetProductLikeByCustomerIdAndProductId(CustomerId.Create(request.CustomerId), ProductId.Create(request.ProductId));

            if (like is null)
            {
                return new CommandResult(HttpStatusCode.NotFound, "");
            }

            _productLikerepository.Delete(like);
            await _productLikerepository.UnitOfWork.Commit(cancellationToken);
            return new CommandResult();
        }
        catch (Exception e)
        {
            return new CommandResult(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}