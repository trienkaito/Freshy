using FRESHY.Main.Application.Abstractions.VoucherAbstractions.Commands.CreateVoucher;
using FRESHY.Main.Application.Abstractions.VoucherAbstractions.Queries.GetAllVouchersDetails.Results;
using FRESHY.Main.Contract.Requests.VoucherRequests;
using FRESHY.Main.Contract.Responses.VoucherResponses;
using Mapster;

namespace FRESHY.Service.Config.Mapster;

public class VoucherMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //Requests Mapping
        config.NewConfig<CreateVoucherRequest, CreateVoucherCommand>();

        //Responses Mapping
        config.NewConfig<AllVouchersDetailsResult, AllVoucherDetailsResponse>();
    }
}