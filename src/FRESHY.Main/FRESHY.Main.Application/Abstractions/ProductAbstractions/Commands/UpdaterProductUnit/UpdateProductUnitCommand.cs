using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Models.Wrappers;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ProductUnitAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Errors;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.ProductAbstractions.Commands.UpdaterProductUnit;

public record UpdateProductUnitCommand
(
    Guid ProductId,
    Guid ProductUnitId,
    string UnitType,
    double UnitValue,
    int Quantity,
    double ImportPrice,
    double SellPrice,
    string UnitFeatureImage
) : ICommand<CommandResult>;

public class UpdateProductUnitCommandHandler : ICommandHandler<UpdateProductUnitCommand, CommandResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IProductUnitRepository _productUnitRepository;
    private readonly ICartItemRepository _cartItemRepository;

    public UpdateProductUnitCommandHandler(
        IProductRepository productRepository,
        IProductUnitRepository productUnitRepository,
        ICartItemRepository cartItemRepository)
    {
        _productRepository = productRepository;
        _productUnitRepository = productUnitRepository;
        _cartItemRepository = cartItemRepository;
    }

    public async Task<CommandResult> Handle(UpdateProductUnitCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _productRepository.UnitOfWork.BeginTransaction();

            var product = await _productRepository.GetByIdAsync(ProductId.Create(request.ProductId));

            if (product is not null)
            {
                var updatedUnit = await _productUnitRepository.GetByIdAsync(ProductUnitId.Create(request.ProductUnitId));

                if (updatedUnit is not null)
                {
                    updatedUnit.Update(
                        request.UnitType,
                        request.UnitValue,
                        request.Quantity,
                        request.ImportPrice,
                        request.SellPrice,
                        request.UnitFeatureImage
                    );

                    var items = await _cartItemRepository.GetCartItemByProductUnitId(updatedUnit.Id);

                    if (items is null)
                    {
                        return new CommandResult(HttpStatusCode.NoContent, "");
                    }

                    foreach (var item in items)
                    {
                        item.UpdatePrice(updatedUnit.SellPrice);
                    }

                    await _productRepository.UnitOfWork.Commit(cancellationToken);
                    return new CommandResult();
                }
                return new CommandResult(HttpStatusCode.NotFound, Error.ITEM_NOT_FOUND);
            }
            return new CommandResult(HttpStatusCode.NotFound, Error.PRODUCT_NOT_FOUND);
        }
        catch (Exception e)
        {
            return new CommandResult(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}