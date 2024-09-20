using FRESHY.Common.Contract.Wrappers;
using FRESHY.Main.Application.Abstractions.JobPositionAbstractions.Commands.CreateJobPosition;
using FRESHY.Main.Application.Abstractions.JobPositionAbstractions.Queries.GetAllJobPositions;
using FRESHY.Main.Contract.Requests.JobPositionRequests;
using FRESHY.Main.Contract.Responses.JobPositionResponses;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FRESHY_API.Controllers;

[ApiController]
[Route("job")]
[Produces("application/json")]
public class JobPositionController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public JobPositionController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost("position")]
    public async Task<IActionResult> AddJobPositionAsync([FromBody] CreateJobPositionRequest request)
    {
        var command = _mapper.Map<CreateJobPositionCommand>(request);
        var result = await _mediator.Send(command);
        if (result.Succeeded)
        {
            return Ok();
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }

    [HttpGet("positions")]
    public async Task<IActionResult> GetAllJobPositions()
    {
        var query = new GetAllJobPosistionsQuery();
        var result = await _mediator.Send(query);
        if (result.Succeeded)
        {
            return Ok(_mapper.Map<Response<IEnumerable<AllJobPositionsResponse>>>(result));
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }
}