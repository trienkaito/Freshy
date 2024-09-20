using FRESHY.Main.Application.Abstractions.Shared.Commands;
using FRESHY.Main.Application.Abstractions.Shared.Results;
using FRESHY.Main.Contract.Requests.Shared;
using FRESHY.Main.Contract.Responses.Shared;
using Mapster;

namespace FRESHY_API.Config.Mapster;

public class SharedMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //Requests Mapping
        config.NewConfig<VoucherCodeRequest, VoucherCodeCommand>();

        //Response Mapping
        config.NewConfig<VoucherCodeResult, VoucherCodeResponse>();
    }
}