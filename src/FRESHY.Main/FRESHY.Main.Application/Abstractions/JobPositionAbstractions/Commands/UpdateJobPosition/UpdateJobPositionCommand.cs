using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Models.Wrappers;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.JobPositionAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Errors;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.JobPositionAbstractions.Commands.UpdateJobPosition;

public record UpdateJobPositionCommand
(
    Guid Id,
    string Name,
    string? Description,
    double Salary
) : ICommand<CommandResult>;

public class UpdateJobPositionCommandHandler : ICommandHandler<UpdateJobPositionCommand, CommandResult>
{
    private readonly IJobPositionRepository _jobPositionRepository;

    public UpdateJobPositionCommandHandler(IJobPositionRepository jobPositionRepository)
    {
        _jobPositionRepository = jobPositionRepository;
    }

    public async Task<CommandResult> Handle(UpdateJobPositionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _jobPositionRepository.UnitOfWork.BeginTransaction();

            var jobPosition = await _jobPositionRepository.GetByIdAsync(JobPositionId.Create(request.Id));

            if (jobPosition is not null)
            {
                jobPosition.UpdateJobPositon(
                    request.Name,
                    request.Description,
                    request.Salary
                );

                await _jobPositionRepository.UnitOfWork.Commit(cancellationToken);
                return new CommandResult();
            }
            return new CommandResult(HttpStatusCode.NotFound, Error.NOT_FOUND);
        }
        catch (Exception e)
        {
            return new CommandResult(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}