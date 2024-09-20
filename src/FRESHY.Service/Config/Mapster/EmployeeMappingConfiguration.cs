using FRESHY.Main.Application.Abstractions.EmployeeProfileAbstractions.Commands.CreateEmployeeProfile;
using FRESHY.Main.Application.Abstractions.EmployeeProfileAbstractions.Commands.UpdateEmployeeProfile;
using FRESHY.Main.Application.Abstractions.EmployeeProfileAbstractions.Queries.GetAllEmployeeProfilesQuery.Results;
using FRESHY.Main.Application.Abstractions.EmployeeProfileAbstractions.Queries.GetEmployeeProfileDetailsQuery.Results;
using FRESHY.Main.Contract.Requests.EmployeeRequests;
using FRESHY.Main.Contract.Responses.EmployeeResponses;
using Mapster;

namespace FRESHY_API.Config.Mapster;

public class EmployeeMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //Requests Mapping
        config.NewConfig<CreateEmployeeProfileRequest, CreateEmployeeProfileCommand>();
        config.NewConfig<UpdateEmployeeProfileRequest, UpdateEmployeeProfileCommand>();

        //Responses Mapping
        config.NewConfig<AllEmployeeProfilesResult, AllEmployeeProfilesResponse>();
        config.NewConfig<EmployeeProfileDetailsResult, EmployeeProfileDetailsResponse>();
        config.NewConfig<EmployeeJobPositionResult, EmployeeJobPositionResponse>();
    }
}