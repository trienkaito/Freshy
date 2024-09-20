using FRESHY.Common.Contract.Wrappers;
using FRESHY.Main.Application.Abstractions.CustomerProfileAbstactions.Queries.GetAllOrderAddressesOfCustomer;
using FRESHY.Main.Application.Abstractions.OrderAbstractions.Commands.CreateOfflineOrder;
using FRESHY.Main.Application.Abstractions.OrderAbstractions.Commands.CreateOnlineOrder;
using FRESHY.Main.Application.Abstractions.OrderAbstractions.Queries.GetAllCustomerOrders;
using FRESHY.Main.Application.Abstractions.OrderAbstractions.Queries.GetAllOrders;
using FRESHY.Main.Application.Abstractions.OrderAbstractions.Queries.GetOrderDetails;
using FRESHY.Main.Contract.Requests.OrderRequests;
using FRESHY.Main.Contract.Responses.OrderResponses;
using FRESHY.SharedKernel.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FRESHY_API.Controllers;

[ApiController]
[Route(template: "order")]
[Produces("application/json")]
public class OrderController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IQrService _qrService;

    public OrderController(IMapper mapper, IMediator mediator, IQrService qrService)
    {
        _mapper = mapper;
        _mediator = mediator;
        _qrService = qrService;
    }

    [HttpPost("offline")]
    public async Task<IActionResult> CreateOfflineOrder([FromBody] CreateOfflineOrderRequest request)
    {
        var command = _mapper.Map<CreateOfflineOrderCommand>(request);
        var result = await _mediator.Send(command);
        if (result.Succeeded)
        {
            var qrCode = _qrService.GenerateQrCode(result.Data);
            return File(qrCode, "image/png");
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }

    [HttpPost("online")]
    public async Task<IActionResult> CreateOnlineOrder([FromBody] CreateOnlineOrderRequest request)
    {
        var command = _mapper.Map<CreateOnlineOrderCommand>(request);
        var result = await _mediator.Send(command);
        if (result.Succeeded)
        {
            return Ok();
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderDetail([FromRoute] Guid id)
    {
        var query = new GetOrderDetailsQuery(id);
        var result = await _mediator.Send(query);
        if (result.Succeeded)
        {
            return Ok(_mapper.Map<Response<OrderDetailsResponse>>(result));
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }

    [HttpGet("customerorder")]
    public async Task<IActionResult> GetCustomerOrder([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] Guid customerId
        )
    {
        var query = new GetAllCustomerOrdersQuery(pageNumber, pageSize, customerId);
        var result = await _mediator.Send(query);
        if (result.Succeeded)
        {
            return Ok(_mapper.Map<PageResponse<IEnumerable<AllCustomerOrdersResponse>>>(result));
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }


    [HttpGet("customerorderitems")]
    public async Task<IActionResult> GetCustomerOrderItems([FromQuery] Guid orderId)
    {
        var query = new GetOrderDetailsQuery(orderId);
        var result = await _mediator.Send(query);
        if (result.Succeeded)
        {
            return Ok(_mapper.Map<Response<OrderDetailsResponse>>(result));
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }
    [HttpGet("all")]
    public async Task<IActionResult> GetAllOrders()
    {
        var query = new GetAllOrdersQuery(2, 10);
        var result = await _mediator.Send(query);
        if (result.Succeeded)
        {
            return Ok(_mapper.Map<PageResponse<IEnumerable<AllOrdersResponse>>>(result));
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }

    [HttpGet("customer/{customerId}/orders")]
    public async Task<IActionResult> GetALlOrdersOfCustomer([FromRoute] Guid customerId)
    {
        var query = new GetAllCustomerOrdersQuery(2, 10, customerId);
        var result = await _mediator.Send(query);

        if (result.Succeeded)
        {
            return Ok(_mapper.Map<PageResponse<IEnumerable<AllCustomerOrdersResponse>>>(result));
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }

    [HttpGet("address/{customerId}")]
    public async Task<IActionResult> GetALlOrdersAddresOfCustomer([FromRoute] Guid customerId)
    {
        var query = new GetAllOrderAddressesOfCustomerQuery(customerId);
        var result = await _mediator.Send(query);
        if (result.Succeeded)
        {
            return Ok(result);
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }


}