using Azure.Core;
using FRESHY.Common.Contract.Wrappers;
using FRESHY.Main.Application.Abstractions.Shared.Commands;
using FRESHY.Main.Application.Abstractions.ShippingAbstractions.Queries.GetAllShippingPartnersDetails;
using FRESHY.Main.Application.Abstractions.VoucherAbstractions.Commands.CheckAndGetValidVoucher;
using FRESHY.Main.Application.Abstractions.VoucherAbstractions.Commands.CreateVoucher;
using FRESHY.Main.Application.Abstractions.VoucherAbstractions.Commands.DeleteVouchers;
using FRESHY.Main.Application.Abstractions.VoucherAbstractions.Queries.GetAllVouchersDetails;
using FRESHY.Main.Contract.Requests.Shared;
using FRESHY.Main.Contract.Requests.VoucherRequests;
using FRESHY.Main.Contract.Responses.VoucherResponses;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FRESHY_API.Controllers;

[ApiController]
[Route("special")]
[Produces("application/json")]
public class VoucherController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public VoucherController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost("voucher")]
    public async Task<IActionResult> CreateVoucher([FromBody] CreateVoucherRequest request)
    {
        var command = _mapper.Map<CreateVoucherCommand>(request);
        var result = await _mediator.Send(command);

        if (result.Succeeded)
        {
            return Ok();
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }

    [HttpGet("vouchers")]
    public async Task<IActionResult> GetAllVoucher()
    {
        var query = new GetAllVouchersDetailsQuery();
        var result = await _mediator.Send(query);
        if (result.Succeeded)
        {
            return Ok(_mapper.Map<Response<IEnumerable<AllVoucherDetailsResponse>>>(result));
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }

    [HttpGet("discountValue")]
    public async Task<IActionResult> CheckAndGetDiscountValue([FromQuery] CheckAndGetValidVoucherRequest request)
    {
        var query = _mapper.Map<CheckAndGetValidVoucherQuery>(request);
        var result = await _mediator.Send(query);
        if (result.Succeeded)
        {
            return Ok(_mapper.Map<Response<double>>(result));
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }

    [HttpPost("vouchers/de-active")]
    public async Task<IActionResult> DeleteVouchers([FromBody] DeActiveVouchersRequest request)
    {
        var command = _mapper.Map<DeActiveVouchersCommand>(request);
        var result = await _mediator.Send(command);
        if (result.Succeeded)
        {
            return Ok();
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }

    [HttpGet("shipping")]
    public async Task<IActionResult> GetAllShipping()
    {
        var query = new GetAllShippingPartnersDetailsQuery();
        var result = await _mediator.Send(query);
        if (result.Succeeded)
        {
            return Ok(result);
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }







}