using FRESHY.Main.Application.Abstractions.ProductTypeAbstractions.Commands.CreateProductType;
using FRESHY.Main.Application.Abstractions.ProductTypeAbstractions.Queries.GetAllProductTypes.Resutls;
using FRESHY.Main.Contract.Requests.ProducTypeRequests;
using FRESHY.Main.Contract.Responses.ProductTypeResponses;
using Mapster;

namespace FRESHY.Service.Config.Mapster;

public class ProductTypeMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //Requests Mapping
        config.NewConfig<CreateProductTypeRequest, CreateProductTypeCommand>();

        //Reponses Mapping
        config.NewConfig<AllProductTypesResult, AllProductTypesResponse>();
    }
}