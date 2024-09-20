using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Models.Wrappers;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.CartItemAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Errors;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.CartItemAbstractions.Commands.DeleteItemFromCartCommand;

public record DeleteItemFromCartCommand
(
    Guid CustomerId,
    Guid CartItemId
) : ICommand<CommandResult>;

public class DeleteItemFromCartCommandHandler : ICommandHandler<DeleteItemFromCartCommand, CommandResult>
{
    private readonly ICartItemRepository _cartItemRepository;
    private readonly ICartRepository _cartRepository;

    public DeleteItemFromCartCommandHandler(
        ICartItemRepository cartItemRepository,
        ICartRepository cartRepository)
    {
        _cartItemRepository = cartItemRepository;
        _cartRepository = cartRepository;
    }

    public async Task<CommandResult> Handle(DeleteItemFromCartCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _cartItemRepository.UnitOfWork.BeginTransaction();

            var cart = await _cartRepository.GetCartByCustomerId(CustomerId.Create(request.CustomerId));

            if (cart is not null)
            {
                var itemToDelete = await _cartItemRepository.GetByIdAsync(CartItemId.Create(request.CartItemId));

                if (itemToDelete is not null)
                {
                    if (itemToDelete.CartId.Equals(cart.Id))
                    {
                        _cartItemRepository.Delete(itemToDelete);
                        await _cartItemRepository.UnitOfWork.Commit(cancellationToken);
                        return new CommandResult();
                    }
                    return new CommandResult(HttpStatusCode.Unauthorized, "");
                }
                return new CommandResult(HttpStatusCode.NotFound, Error.ITEM_NOT_FOUND);
            }
            return new CommandResult(HttpStatusCode.NotFound, Error.NOT_FOUND);
        }
        catch (Exception e)
        {
            return new CommandResult(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}