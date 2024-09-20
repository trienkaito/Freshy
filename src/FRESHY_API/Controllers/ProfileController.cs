using FRESHY.Common.Contract.Wrappers;
using FRESHY.Main.Application.Abstractions.CartItemAbstractions.Commands.AddItemToCartCommand;
using FRESHY.Main.Application.Abstractions.CartItemAbstractions.Commands.DeleteItemFromCartCommand;
using FRESHY.Main.Application.Abstractions.CartItemAbstractions.Queries.GetCartItemOfCustomerCartQuery;
using FRESHY.Main.Application.Abstractions.CustomerProfileAbstactions.Commands.CreateCustomerProfile;
using FRESHY.Main.Application.Abstractions.CustomerProfileAbstactions.Queries.GetAllCustomerProfile;
using FRESHY.Main.Application.Abstractions.CustomerProfileAbstactions.Queries.GetCustomerByAccountId;
using FRESHY.Main.Application.Abstractions.EmployeeProfileAbstractions.Commands.CreateEmployeeProfile;
using FRESHY.Main.Application.Abstractions.EmployeeProfileAbstractions.CustomerProfileAbstactions.Commands.CreateCustomerProfile;
using FRESHY.Main.Application.Abstractions.EmployeeProfileAbstractions.Queries.GetAllEmployeesQuery;
using FRESHY.Main.Application.Abstractions.EmployeeProfileAbstractions.Queries.SearchEmployeeProfileQuery;
using FRESHY.Main.Application.Abstractions.ProductLikeAbstractions.Commands;
using FRESHY.Main.Application.Abstractions.ProductLikeAbstractions.Queries;
using FRESHY.Main.Contract.Requests.CartItemRequests;
using FRESHY.Main.Contract.Requests.CustomerRequests;
using FRESHY.Main.Contract.Requests.EmployeeRequests;
using FRESHY.Main.Contract.Requests.ProductLikesRequests;
using FRESHY.Main.Contract.Responses.CartItemResponses;
using FRESHY.Main.Contract.Responses.CustomerResponses;
using FRESHY.Main.Contract.Responses.EmployeeResponses;
using FRESHY.Main.Contract.Responses.ProductLikesResponses;
using FRESHY.Main.Infrastructure.Persistance;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FRESHY_API.Controllers;

[ApiController]
[Route("profile")]
[Produces("application/json")]
public class ProfileController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly FreshyDbContext _context;

    public ProfileController(IMapper mapper, IMediator mediator, FreshyDbContext context)
    {
        _mapper = mapper;
        _mediator = mediator;
        _context = context;
    }

    [HttpPost("employee")]
    public async Task<IActionResult> CreateEmployeeProfile([FromBody] CreateEmployeeProfileRequest request)
    {
        var command = _mapper.Map<CreateEmployeeProfileCommand>(request);
        var result = await _mediator.Send(command);
        if (result.Succeeded)
        {
            return Ok(result);    
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }

    [HttpGet("employees")]
    public async Task<IActionResult> GetAllEmployeeProfiles([FromQuery] GetAllEmployeeProfilesQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.Succeeded)
        {
            return Ok(_mapper.Map<PageResponse<IEnumerable<AllEmployeeProfilesResponse>>>(result));
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }

    [HttpPost("customer")]
    public async Task<IActionResult> CreateCustomerProfile([FromBody] CreateCustomerProfileRequest request)
    {
        var command = _mapper.Map<CreateCustomerProfileCommand>(request);
        var result = await _mediator.Send(command);
        if (result.Succeeded)
        {
            return Ok(result);
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }

    [HttpGet("customer/{accountId}")]
    public async Task<IActionResult> GetCustomerByAcccoutId([FromRoute] Guid accountId)
    {
        var query = new GetCustomerByAccountIdQuery(accountId);
        var result = await _mediator.Send(query);
        if (result.Succeeded)
        {
            return Ok(_mapper.Map<Response<GetCustomerByAccountIdResult>>(result));
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }

    [HttpGet("customers")]
    public async Task<IActionResult> GetAllCustomerProfiles([FromBody] GetAllCustomerProfilesQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.Succeeded)
        {
            return Ok(_mapper.Map<PageResponse<IEnumerable<AllCustomerProfilesResponse>>>(result));
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }

    

    [HttpPost("customer/cart")]
    public async Task<IActionResult> AddToCustomerCart([FromBody] AddItemToCartRequest request)
    {
        var command = _mapper.Map<AddItemToCartCommand>(request);
        var result = await _mediator.Send(command);
        if (result.Succeeded)
        {
            return Ok();
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }

    [HttpGet("customer/{id}/cart")]
    public async Task<IActionResult> GetCustomerCart([FromRoute] Guid id)
    {
        var query = new GetCartItemsOfCustomerCartQuery(id);
        var result = await _mediator.Send(query);

        if (result.Succeeded)
        {
            return Ok(_mapper.Map<Response<IEnumerable<CartItemsOfCustomerCartResponse>>>(result));
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }

    [HttpDelete("customer/{id}/item/{cartId}")]
    public async Task<IActionResult> AddToCustomerCart([FromRoute] Guid id, [FromRoute] Guid cartId)
    {
        var request = new DeleteItemFromCartRequest(id, cartId);
        var command = _mapper.Map<DeleteItemFromCartCommand>(request);
        var result = await _mediator.Send(command);

        if (result.Succeeded)
        {
            return Ok();
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }
    [HttpGet("SearchCustomer/{content}")]
    public async Task<IActionResult> AddToCustomerCart([FromRoute] string content)
    {
        var result = _context.Customers.Where(x => (x.Name.Contains(content) || x.Email.Contains(content) || x.Phone.Contains(content))).ToList();
        if (result!=null)
        {
            return Ok(result);
        }
        return NotFound();
    }

    

    /*[HttpPost("customer/productlike")]
    public async Task<IActionResult> AddToListProductLike([FromBody] AllProductLikesByCustomerRequest request)
    {
        
        var result = await _mediator.Send(query);
        return Ok(_mapper.Map<PageResponse<IEnumerable<AllEmployeeProfilesResponse>>>(result));
    }*/
}