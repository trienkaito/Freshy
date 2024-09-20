using FRESHY.Main.Application.Abstractions.CartItemAbstractions.Queries.GetCartItemsOfCustomerQuery.Results;
using FRESHY.Main.Application.Abstractions.CustomerProfileAbstactions.Commands.CreateCustomerProfile;
using FRESHY.Main.Application.Abstractions.CustomerProfileAbstactions.Commands.UpdateCustomerPorfile;
using FRESHY.Main.Application.Abstractions.CustomerProfileAbstactions.Queries.GetAllOrderAddressesOfCustomer.Results;
using FRESHY.Main.Application.Abstractions.CustomerProfileAbstactions.Queries.GetCustomerProfileDetails.Results;
using FRESHY.Main.Application.Abstractions.EmployeeProfileAbstractions.CustomerProfileAbstactions.Commands.CreateCustomerProfile;
using FRESHY.Main.Application.Abstractions.EmployeeProfileAbstractions.CustomerProfileAbstactions.Queries.GetAllCustomerProfiles.Results;
using FRESHY.Main.Application.Abstractions.ProductLikeAbstractions.Commands;
using FRESHY.Main.Application.Abstractions.ProductLikeAbstractions.Queries.Resutls;
using FRESHY.Main.Contract.Requests.CustomerRequests;
using FRESHY.Main.Contract.Requests.ProductLikesRequests;
using FRESHY.Main.Contract.Responses.CartItemResponses;
using FRESHY.Main.Contract.Responses.CustomerResponses;
using FRESHY.Main.Contract.Responses.ProductLikesResponses;
using Mapster;

namespace FRESHY_API.Config.Mapster;

public class CustomerMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //Requests Mapping
        config.NewConfig<CreateCustomerProfileRequest, CreateCustomerProfileCommand>();
        config.NewConfig<UpdateCustomerProfileRequest, UpdateCustomerProfileCommand>();
        config.NewConfig<DeleteProductLikeRequest, DeleteProductLikeCommand>();
        // 
        //Responses Mapping
        config.NewConfig<AllCustomerProfilesResult, AllCustomerProfilesResponse>();
        config.NewConfig<CartItemsOfCustomerCartResult, CartItemsOfCustomerCartResponse>();
        config.NewConfig<CustomerProfileDetailsResult, CustomerProfileDetailsResponse>();
        config.NewConfig<AllOrderAddressesOfCustomerResult, AllOrderAddressesOfCustomerResponse>();
        config.NewConfig<AllProductLikesByCustomerResult, AllProductLikesByCustomerResponse>();

    }
}