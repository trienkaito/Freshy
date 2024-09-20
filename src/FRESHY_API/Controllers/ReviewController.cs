using FRESHY.Main.Application.Abstractions.ReviewAbstractions.Commands.CreateReview;
using FRESHY.Main.Application.Abstractions.ReviewAbstractions.Commands.ReplyReview;
using FRESHY.Main.Contract.Requests.ReviewRequests;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FRESHY_API.Controllers;

[ApiController]
[Route("review")]
[Produces("application/json")]
public class ReviewController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ReviewController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewReviewAsync(CreateReviewRequest request)
    {
        var command = _mapper.Map<CreateReviewCommand>(request);
        var result = await _mediator.Send(command);

        if (result.Succeeded)
        {
            return Ok();
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }

    [HttpPost("relpy")]
    public async Task<IActionResult> ReplyExistingReview(ReplyReviewRequest request)
    {
        var command = _mapper.Map<ReplyReviewCommand>(request);
        var result = await _mediator.Send(command);

        if (result.Succeeded)
        {
            return Ok();
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }
}