using FRESHY.Main.Application.Abstractions.ProductAbstractions.Commands.ImportProduct;
using FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.GetAllProducts.Results;
using FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.GetProductDetails.Results;
using FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.GetProductsOfASupplier.Results;
using FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.Shared.Results;
using FRESHY.Main.Contract.Requests.ProductRequests;
using FRESHY.Main.Contract.Responses.ProductResponses;
using FRESHY.Main.Contract.Responses.ProductResponses.Shared;
using Mapster;

namespace FRESHY.Service.Config.Mapster;

public class ProductMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //Requests Mapping
        config.NewConfig<ImportProductRequest, ImportProductCommand>();
        config.NewConfig<CreateProductUnitRequest, CreateProductUnitCommand>();

        //Responses Mapping
        config.NewConfig<AllProductsResult, AllProductsResponse>();
        config.NewConfig<ProductDetailsResult, ProductDetailsResponse>();
        config.NewConfig<ProductsOfASupplierResult, ProductsOfASupplierResponse>();
        config.NewConfig<ProductSupplierResult, ProductSupplierResponse>();
        config.NewConfig<ProductTypeResult, ProductTypeResponse>();
    }
}