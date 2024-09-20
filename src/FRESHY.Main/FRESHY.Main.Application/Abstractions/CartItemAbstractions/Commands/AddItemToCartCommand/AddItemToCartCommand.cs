using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Models.Wrappers;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.CartItemAggregate;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ProductUnitAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Errors;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.CartItemAbstractions.Commands.AddItemToCartCommand;

public record AddItemToCartCommand
(
    Guid CustomerId,
    Guid ProductId,
    Guid ProductUnitId,
    int BoughtQuantity
) : ICommand<CommandResult>;

public class AddItemToCartCommandHadnler : ICommandHandler<AddItemToCartCommand, CommandResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IProductRepository _productRepository;
    private readonly IProductUnitRepository _productUnitRepository;

    public AddItemToCartCommandHadnler(
        ICartItemRepository cartItemRepository,
        IProductRepository productRepository,
        IProductUnitRepository productUnitRepository,
        ICartRepository cartRepository)
    {
        _cartItemRepository = cartItemRepository;
        _productRepository = productRepository;
        _productUnitRepository = productUnitRepository;
        _cartRepository = cartRepository;
    }

    public async Task<CommandResult> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _cartItemRepository.UnitOfWork.BeginTransaction();

            var cart = await _cartRepository.GetCartByCustomerId(CustomerId.Create(request.CustomerId));
            
            var existingItem = await _cartItemRepository.GetByCartProductAndUnitIdAsync(
                cart.Id,
                ProductId.Create(request.ProductId),
                ProductUnitId.Create(request.ProductUnitId)
            );

            if (existingItem is not null)
            {
                var isValid = await _productUnitRepository.CheckProductUnitQuantity(existingItem.ProductUnitId, existingItem.BoughtQuantity + request.BoughtQuantity);

                if (isValid)
                {
                    var unit = await _productUnitRepository.GetByIdAsync(existingItem.ProductUnitId, unit => new { unit.SellPrice });

                    existingItem.UpdateQuantity(request.BoughtQuantity, request.BoughtQuantity * unit!.SellPrice);
                    await _cartItemRepository.UnitOfWork.Commit(cancellationToken);
                    return new CommandResult();
                }
                return new CommandResult(HttpStatusCode.BadRequest, Error.PRODUCT_QUANTITY_NOT_ENOUGH);
            }

            var product = await _productRepository.GetByIdAsync(ProductId.Create(request.ProductId));

            if (product is not null)
            {
                var productUnit = await _productUnitRepository.GetByIdAsync(ProductUnitId.Create(request.ProductUnitId));

                if (productUnit is not null)
                {
                    var isValid = await _productUnitRepository.CheckProductUnitQuantity(productUnit.Id, request.BoughtQuantity);

                    if (isValid)
                    {
                        var item = CartItem.AddToCart(
                            cart.Id,
                            ProductId.Create(request.ProductId),
                            ProductUnitId.Create(request.ProductUnitId),
                            request.BoughtQuantity,
                            productUnit.SellPrice * request.BoughtQuantity
                        );

                        await _cartItemRepository.InsertAsync(item);
                        await _cartItemRepository.UnitOfWork.Commit(cancellationToken);
                        return new CommandResult();
                    }
                    return new CommandResult(HttpStatusCode.BadRequest, Error.PRODUCT_QUANTITY_NOT_ENOUGH);
                }
                return new CommandResult(HttpStatusCode.NotFound, Error.PRODUCT_UNIT_NOT_FOUND);
            }
            return new CommandResult(HttpStatusCode.NotFound, Error.PRODUCT_NOT_FOUND);
        }
        catch (Exception e)
        {
            return new CommandResult(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}