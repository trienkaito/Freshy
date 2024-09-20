using System.Net;
using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Models.Wrappers;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate.Entities;

namespace FRESHY.Main.Application.Abstractions.ProductLikeAbstractions.Commands;

public record ProductLikeCommand
(
    Guid ProductId,
    Guid CustomerId
) : ICommand<CommandResult>;

public class ProductLikeCommandHandler : ICommandHandler<ProductLikeCommand, CommandResult>
{
    private readonly IProductLikerepository _productLikeRepository;
    private readonly IProductRepository _productRepository;

    public ProductLikeCommandHandler(IProductLikerepository productLikeRepository, IProductRepository productRepository)
    {
        _productLikeRepository = productLikeRepository;
        _productRepository = productRepository;
    }

    public async Task<CommandResult> Handle(ProductLikeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _productLikeRepository.UnitOfWork.BeginTransaction();

            var product = await _productRepository.GetByIdAsync(ProductId.Create(request.ProductId), product => new
            {
                product.Id
            });

            if (product is not null)
            {
                var productLike = ProductLike.Create(product.Id, CustomerId.Create(request.CustomerId));

                await _productLikeRepository.InsertAsync(productLike);
                await _productLikeRepository.UnitOfWork.Commit(cancellationToken);

                return new CommandResult();
            }
            return new CommandResult(HttpStatusCode.NotFound, "PRODUCT_NOT_FOUND");
        }
        catch (Exception e)
        {
            return new CommandResult(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}