using FRESHY.Main.Application.Abstractions.OrderAbstractions.Commands.CreateOfflineOrder;
using FRESHY.Main.Application.Abstractions.OrderAbstractions.Commands.CreateOnlineOrder;
using FRESHY.Main.Application.Abstractions.OrderAbstractions.Commands.Shared.Abstractions;
using FRESHY.Main.Application.Abstractions.OrderAbstractions.Queries.GetAllOrders.Results;
using FRESHY.Main.Application.Abstractions.OrderAbstractions.Queries.GetOrderDetails.Results;
using FRESHY.Main.Application.Abstractions.OrderAbstractions.Queries.Shared.Results;
using FRESHY.Main.Contract.Requests.OrderRequests;
using FRESHY.Main.Contract.Requests.OrderRequests.Shared;
using FRESHY.Main.Contract.Responses.OrderResponses;
using Mapster;

namespace FRESHY.Service.Config.Mapster;

public class OrderMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //Requests Mapping
        config.NewConfig<CreateOfflineOrderRequest, CreateOfflineOrderCommand>();
        config.NewConfig<CreateOnlineOrderRequest, CreateOnlineOrderCommand>();
        config.NewConfig<CreateOrderItemRequest, CreateOrderItemCommand>();

        //Responses Mapping
        config.NewConfig<AllOrdersResult, AllOrdersResponse>();
        config.NewConfig<OrderDetailsResult, OrderDetailsResponse>();
        config.NewConfig<OrderShippingResult, OrderShippingResponse>();
        config.NewConfig<OrderVoucherResult, OrderVoucherResponse>();
        config.NewConfig<PaymentTypeResult, PaymentTypeResponse>();
        config.NewConfig<OrderStatusResult, OrderStatusResponse>();
    }
}