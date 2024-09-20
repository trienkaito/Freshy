using FRESHY.Main.Application.Abstractions.VoucherAbstractions.Commands.CheckAndGetValidVoucher;
using FRESHY.Main.Application.Abstractions.VoucherAbstractions.Commands.CreateVoucher;
using FRESHY.Main.Application.Abstractions.VoucherAbstractions.Commands.DeleteVouchers;
using FRESHY.Main.Application.Abstractions.VoucherAbstractions.Queries.GetAllVouchersDetails.Results;
using FRESHY.Main.Contract.Requests.Shared;
using FRESHY.Main.Contract.Requests.VoucherRequests;
using FRESHY.Main.Contract.Responses.VoucherResponses;
using Mapster;

namespace FRESHY_API.Config.Mapster;

public class VoucherMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {//Requests Mapping
        config.NewConfig<CheckAndGetValidVoucherRequest, CheckAndGetValidVoucherQuery>();

        config.NewConfig<CreateVoucherRequest, CreateVoucherCommand>();
        config.NewConfig<DeActiveVouchersRequest, DeActiveVouchersCommand>();

        //Responses Mapping
        config.NewConfig<AllVouchersDetailsResult, AllVoucherDetailsResponse>();
    }
}