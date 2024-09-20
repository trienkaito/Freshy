
using Amazon.Runtime.Internal;
using Azure.Core;
using FRESHY.Authentication.Application.Abtractions.UserAbtracttions.Command.ImportUser;
using FRESHY.Authentication.Application.Abtractions.UserAbtracttions.Command.Login;
using FRESHY.Authentication.Contract.Request;
using FRESHY.Authentication.Contract.Responses;
using FRESHY.Common.Contract.Wrappers;
using FRESHY.Main.Application.Abstractions.ProductTypeAbstractions.Commands.CreateProductType;
using FRESHY.Main.Contract.Responses.ProductResponses;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FRESHY_API_AUTHENTICATION.Controllers
{
    [Route("/user")]
    [ApiController]
    public class CustommerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CustommerController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var command = _mapper.Map<UserLoginCommand>(request);
            var result = await _mediator.Send(command);

            if (result.Succeeded)
            {
                return StatusCode((int)result.StatusCode, result.Data);
            }
            return StatusCode((int)result.StatusCode, result.Message);
        }

        [HttpPost("register")]
        public async Task<IActionResult> ImportUser([FromBody] ImportUserRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                // Gọi hàm Handle của ImportUserCommandHandler bằng cách gửi một instance của ImportUserCommand
                var command = new ImportUserCommand(
                    request.UserName,
                    request.Password,
                    request.Email,
                    request.PhoneNumber,
                    request.Roles
                );

                // Sử dụng MediatR để gọi handler của command
                var result = await _mediator.Send(command);

                // Kiểm tra kết quả và trả về phản hồi tương ứng
                if (result)
                {
                    return Ok("Người dùng được nhập thành công.");
                }
                else
                {
                    return BadRequest("Có lỗi xảy ra khi nhập người dùng.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }

        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            var command = _mapper.Map<GoogleLoginCommand>(request);
            var result = await _mediator.Send(command);

            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            else
            {
                return StatusCode((int)result.StatusCode, result.Message);
            }

        }

        // [HttpPost("sendEmail")]
        // public async Task<IActionResult> SendEmail([FromBody] GoogleLoginRequest request)
        // {
        //     var command = _mapper.Map<GoogleLoginCommand>(request);
        //     var result = await _mediator.Send(command);

        //     if (result.Succeeded)
        //     {
        //         return Ok(result.Data);
        //     }
        //     else
        //     {
        //         return StatusCode((int)result.StatusCode, result.Message);
        //     }

        // }
    }

}



