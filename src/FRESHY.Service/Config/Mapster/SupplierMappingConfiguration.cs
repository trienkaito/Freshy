using FRESHY.Main.Application.Abstractions.SupplierAbstractions.Commands.AddNewSupplier;
using FRESHY.Main.Application.Abstractions.SupplierAbstractions.Queries.Shared.Results;
using FRESHY.Main.Contract.Requests.SupplierRequests;
using FRESHY.Main.Contract.Responses.SupplierResponses;
using Mapster;

namespace FRESHY.Service.Config.Mapster;

public class SupplierMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //Requests Mapping
        config.NewConfig<AddNewSupplierRequest, AddNewSupplierCommand>();

        //Responses Mapping
        config.NewConfig<SupplierResult, SupplierResponse>();
    }
}