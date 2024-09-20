using FRESHY.Authentication.Contract.Responses;
using FRESHY.Common.Domain.Common.Interfaces;
using FRESHY.SharedKernel.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Net;

using System.Threading;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace FRESHY.Authentication.Application.Abtractions.UserAbtracttions.Command.ForgetPassword
{
    // public record ForgetPasswordCommand(string Email) : IRequest<QueryResult<ForgetResponse>>;

    // public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, QueryResult<ForgetResponse>>
    // {
    //     private readonly UserManager<IdentityUser> _userManager;
    //     private readonly IEmailService _emailService; // Đây là một dịch vụ gửi email, bạn cần triển khai nó

    //     public ForgetPasswordCommandHandler(UserManager<IdentityUser> userManager, IEmailService emailService)
    //     {
    //         _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    //         _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
    //     }

    //     public async Task<QueryResult<ForgetResponse>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
    //     {
    //         var result = new QueryResult<ForgetResponse>();

    //         try
    //         {
    //             var user = await _userManager.FindByEmailAsync(request.Email);
    //             if (user == null)
    //             {
    //                 return new QueryResult<ForgetResponse>(HttpStatusCode.NotFound, "User not found.");
    //             }

    //             // Tạo mã đặt lại mật khẩu
    //             //      var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

    //             // Gửi mã đặt lại mật khẩu đến người dùng (qua email)
    //             int ma = await _emailService.SendPasswordResetEmailAsync(user.Email);
    //             var forget = new ForgetResponse { OTP = ma };
    //             result.Data = forget;
    //             result.Succeeded = true;
    //             result.Message = "Email sent successfully.";
    //             result.StatusCode = HttpStatusCode.OK;

    //         }
    //         catch (Exception ex)
    //         {
    //             // Xử lý lỗi
    //             return new QueryResult<ForgetResponse>(HttpStatusCode.InternalServerError, "An error occurred while processing the request.");
    //         }

    //         return result;
    //     }
    // }
}
