using FRESHY.Main.Application.Abstractions.CartItemAbstractions.Commands.AddItemToCartCommand;
using FRESHY.Main.Application.Abstractions.CartItemAbstractions.Commands.DeleteItemFromCartCommand;
using FRESHY.Main.Contract.Requests.CartItemRequests;
using Mapster;

namespace FRESHY_API.Config.Mapster;

public class CartMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //Requests Mapping
        config.NewConfig<AddItemToCartRequest, AddItemToCartCommand>();
        config.NewConfig<DeleteItemFromCartRequest, DeleteItemFromCartCommand>();
    }
}