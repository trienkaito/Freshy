using FRESHY.Main.Application.Abstractions.JobPositionAbstractions.Commands.CreateJobPosition;
using FRESHY.Main.Application.Abstractions.JobPositionAbstractions.Commands.DeleteJobPosition;
using FRESHY.Main.Application.Abstractions.JobPositionAbstractions.Queries.GetAllJobPositions.Results;
using FRESHY.Main.Contract.Requests.JobPositionRequests;
using FRESHY.Main.Contract.Responses.JobPositionResponses;
using Mapster;

namespace FRESHY_API.Config.Mapster;

public class JobPositionMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //Requests Mapping
        config.NewConfig<CreateJobPositionRequest, CreateJobPositionCommand>();
        config.NewConfig<UpdateJobPositionRequest, UpdateJobPositionRequest>();
        config.NewConfig<DeleteJobPositionRequest, DeleteJobPositionCommand>();

        //Responses Mapping
        config.NewConfig<AllJobPositionsResult, AllJobPositionsResponse>();
    }
}