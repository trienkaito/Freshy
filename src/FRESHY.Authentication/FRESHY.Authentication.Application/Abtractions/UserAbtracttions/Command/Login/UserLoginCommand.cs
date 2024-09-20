using FRESHY.Authentication.Application.Interfaces;
using FRESHY.Authentication.Contract.Responses;
using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Security.Claims;
namespace FRESHY.Authentication.Application.Abtractions.UserAbtracttions.Command.Login

{
    public record UserLoginCommand
    (
         string UserName,
         string Password
    ) : ICommand<QueryResult<AuthResponse>>;

    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, QueryResult<AuthResponse>>
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtTokenGenerator _tokenService;

        public UserLoginCommandHandler(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IJwtTokenGenerator tokenService)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        public async Task<QueryResult<AuthResponse>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var result = new QueryResult<AuthResponse>();

            var user = await _userManager.FindByNameAsync(request.UserName);
            
            if (user == null)
            {
                return new QueryResult<AuthResponse>(HttpStatusCode.NotFound, "Tên người dùng không tồn tại.");
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

            if (signInResult.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var token = _tokenService.Generator(claims);

                await _userManager.UpdateAsync(user);

                var AuthResponse = new AuthResponse { UserId = user.Id, Name = user.UserName, Token = token, Email = user.Email,Phone= user.PhoneNumber,Roles = roles.ToList() };
                result.Data = AuthResponse;
                result.Succeeded = true;
                result.Message = "Login Succeed";
                result.StatusCode = HttpStatusCode.OK;
            }
            else if (signInResult.IsLockedOut)
            {
                return new QueryResult<AuthResponse>(HttpStatusCode.Locked, "Tài khoản của bạn đã bị khóa.");
            }
            else if (signInResult.IsNotAllowed)
            {
                return new QueryResult<AuthResponse>(HttpStatusCode.Forbidden, "Tài khoản của bạn không được phép đăng nhập.");
            }
            else
            {
                return new QueryResult<AuthResponse>(HttpStatusCode.Unauthorized, "Tên người dùng hoặc mật khẩu không chính xác.");
            }

            return result;
        }
    }
}