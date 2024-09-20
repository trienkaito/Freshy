using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Models.Wrappers;
using FRESHY.Main.Application.Abstractions.OrderAbstractions.Commands.Shared.Abstractions;
using FRESHY.Main.Application.Abstractions.Shared.Commands;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate;
using FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate.Helpers;
using FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.OrderItemAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ProductUnitAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ShippingAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.VoucherAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Errors;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.OrderAbstractions.Commands.CreateOnlineOrder;

public record CreateOnlineOrderCommand
(
    Guid CustomerId,
    string OrderAddress,
    List<CreateOrderItemCommand> OrderItems,
    PaymentType PaymentType,
    Guid ShippingId,
    VoucherCodeCommand? VoucherCode
) : ICommand<CommandResult>;

public class CreateOnlineOrderCommandHandler : ICommandHandler<CreateOnlineOrderCommand, CommandResult>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IVoucherRepository _voucherRepository;
    private readonly IProductRepository _productRepository;
    private readonly IProductUnitRepository _productUnitRepository;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IShippingRepository _shippingRepository;

    public CreateOnlineOrderCommandHandler(
        IOrderRepository orderRepository,
        IVoucherRepository voucherRepository,
        IProductRepository productRepository,
        IShippingRepository shippingRepository,
        IOrderItemRepository orderItemRepository,
        IProductUnitRepository productUnitRepository)
    {
        _orderRepository = orderRepository;
        _voucherRepository = voucherRepository;
        _productRepository = productRepository;
        _shippingRepository = shippingRepository;
        _orderItemRepository = orderItemRepository;
        _productUnitRepository = productUnitRepository;
    }

    public async Task<CommandResult> Handle(CreateOnlineOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _orderRepository.UnitOfWork.BeginTransaction();

            var orderDetailId = OrderDetailId.CreateUnique();
            var items = new List<OrderItem>();

            foreach (var item in request.OrderItems)
            {
                var product = await _productRepository.GetByIdAsync(ProductId.Create(item.ProductId), product => new { product.Id });

                if (product is not null)
                {
                    var isValid = await _productUnitRepository.CheckProductUnitQuantity(ProductUnitId.Create(item.UnitId), item.BoughtQuantity);

                    if (isValid)
                    {
                        var unit = await _productUnitRepository.GetByIdAsync(ProductUnitId.Create(item.UnitId));

                        var orderItem = OrderItem.Create(
                            orderDetailId,
                            product.Id,
                            unit!.Id,
                            item.BoughtQuantity,
                            unit.SellPrice * item.BoughtQuantity
                        );

                        unit.SubstractUnitStock(item.BoughtQuantity);
                        items.Add(orderItem);
                    }
                    else return new CommandResult(HttpStatusCode.Conflict, Error.PRODUCT_QUANTITY_NOT_ENOUGH);
                }
                else return new CommandResult(HttpStatusCode.Conflict, Error.PRODUCT_NOT_FOUND);
            }
            var voucher = await _voucherRepository.CheckAndGetValidVoucherAsync(VoucherCode.Create(request.VoucherCode.Value));

            var shipping = await _shippingRepository.GetByIdAsync(ShippingId.Create(request.ShippingId), shipping => new
            {
                shipping.Id,
                shipping.ShippingPrice
            });

            var productPrice = items.Select(item => item.TotalPrice).Sum();

            var order = OrderDetail.CreateOnlineOrder(
                orderDetailId,
                CustomerId.Create(request.CustomerId),
                request.OrderAddress,
                productPrice,
                request.PaymentType,
                shipping?.Id,
                voucher?.Id,
                voucher is not null ? Math.Round(productPrice - (productPrice * voucher.DiscountValue)) + (shipping is null ? 0 : shipping.ShippingPrice) : productPrice
            );

            await _orderRepository.InsertAsync(order);
            await _orderItemRepository.InsertRange(items);
            await _orderRepository.UnitOfWork.Commit(cancellationToken);
            return new CommandResult();
        }
        catch (Exception e)
        {
            return new CommandResult(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}