using FluentValidation;
using FRESHY.Common.Contract.Wrappers;
using FRESHY.Main.Application.Abstractions.ShippingAbstractions.Commands.CreateNewShippingPartner;
using FRESHY.Main.Application.Abstractions.ShippingAbstractions.Queries.GetAllShippingPartnersDetails;
using FRESHY.Main.Application.Abstractions.SupplierAbstractions.Commands.AddNewSupplier;
using FRESHY.Main.Application.Abstractions.SupplierAbstractions.Queries.GetAllSuppliers;
using FRESHY.Main.Contract.Requests.ShippingRequests;
using FRESHY.Main.Contract.Requests.SupplierRequests;
using FRESHY.Main.Contract.Responses.ShippingResponses;
using FRESHY.Main.Contract.Responses.SupplierResponses;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FRESHY_API.Controllers;

[ApiController]
[Route("partner")]
[Produces("application/json")]
public class PartnerController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public PartnerController(
        IMapper mapper,
        IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost("supplier")]
    public async Task<IActionResult> AddSupplierAsync([FromBody] AddNewSupplierRequest request)
    {
        var command = _mapper.Map<AddNewSupplierCommand>(request);
        var result = await _mediator.Send(command);
        if (result.Succeeded)
        {
            return Ok();
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }

    [HttpGet("suplliers")]
    public async Task<IActionResult> GetAllSupplierAsync()
    {
        var query = new GetAllSuppliersQuery();
        var result = await _mediator.Send(query);

        if (result.Succeeded)
        {
            return Ok(_mapper.Map<Response<IEnumerable<SupplierResponse>>>(result));
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }

    [HttpPost("shipping")]
    public async Task<IActionResult> CreateNewShippingAsync([FromBody] CreateNewShippingPartnerRequest request)
    {
        var command = _mapper.Map<CreateNewShippingPartnerCommand>(request);
        var result = await _mediator.Send(command);
        if (result.Succeeded)
        {
            return Ok();
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }

    [HttpGet("shippings")]
    public async Task<IActionResult> GetAllShippingPartners()
    {
        var query = new GetAllShippingPartnersDetailsQuery();
        var result = await _mediator.Send(query);

        if (result.Succeeded)
        {
            return Ok(_mapper.Map<Response<IEnumerable<AllShippingPartersResponse>>>(result));
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }
}