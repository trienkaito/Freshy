using FRESHY.Main.Application.Abstractions.CustomerProfileAbstactions.Commands.CreateCustomerProfile;
using FRESHY.Main.Application.Abstractions.EmployeeProfileAbstractions.CustomerProfileAbstactions.Commands.CreateCustomerProfile;
using FRESHY.Main.Application.Abstractions.EmployeeProfileAbstractions.CustomerProfileAbstactions.Queries.GetAllCustomerProfiles.Results;
using FRESHY.Main.Contract.Requests.CustomerRequests;
using FRESHY.Main.Contract.Responses.CustomerResponses;
using Mapster;

namespace FRESHY.Service.Config.Mapster;

public class CustomerMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //Requests Mapping
        config.NewConfig<CreateCustomerProfileRequest, CreateCustomerProfileCommand>();

        //Responses Mapping
        config.NewConfig<AllCustomerProfilesResult, AllCustomerProfilesResponse>();
    }
}