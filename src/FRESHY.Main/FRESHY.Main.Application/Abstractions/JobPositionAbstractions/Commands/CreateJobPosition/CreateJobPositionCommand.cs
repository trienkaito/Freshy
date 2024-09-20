using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Models.Wrappers;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.JobPositionAggregate;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.JobPositionAbstractions.Commands.CreateJobPosition;

public record CreateJobPositionCommand
(
    string Name,
    string? Description,
    double Salary
) : ICommand<CommandResult>;

public class CreateJobPositionCommandHandler : ICommandHandler<CreateJobPositionCommand, CommandResult>
{
    private readonly IJobPositionRepository _jobPositionRepository;

    public CreateJobPositionCommandHandler(IJobPositionRepository jobPositionRepository)
    {
        _jobPositionRepository = jobPositionRepository;
    }

    public async Task<CommandResult> Handle(CreateJobPositionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _jobPositionRepository.UnitOfWork.BeginTransaction();

            var jobPosition = JobPosition.Create(
                request.Name,
                request.Description,
                request.Salary);

            await _jobPositionRepository.InsertAsync(jobPosition);
            await _jobPositionRepository.UnitOfWork.Commit(cancellationToken);
            return new CommandResult();
        }
        catch (Exception e)
        {
            return new CommandResult(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}