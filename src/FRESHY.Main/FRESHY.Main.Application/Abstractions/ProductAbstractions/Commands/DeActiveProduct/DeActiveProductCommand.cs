using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Models.Wrappers;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.Events;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Errors;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.ProductAbstractions.Commands.DeActiveProduct;

public record DeActiveProductCommand
(
    Guid ProductId,
    Guid EmployeeId
) : ICommand<CommandResult>;

public class DeleteProductCommandHandler : ICommandHandler<DeActiveProductCommand, CommandResult>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<CommandResult> Handle(DeActiveProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _productRepository.UnitOfWork.BeginTransaction();

            var product = await _productRepository.GetByIdAsync(ProductId.Create(request.ProductId));

            if (product is not null)
            {
                product.DeActiveProduct();

                var @event = new ProductBeingDeActivated(
                    product.Id.Value,
                    request.EmployeeId);

                product.AddDomainEvent(@event);
                await _productRepository.UnitOfWork.Commit(cancellationToken);

                return new CommandResult();
            }
            return new CommandResult(HttpStatusCode.NotFound, Error.PRODUCT_NOT_FOUND);
        }
        catch (Exception e)
        {
            return new CommandResult(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}