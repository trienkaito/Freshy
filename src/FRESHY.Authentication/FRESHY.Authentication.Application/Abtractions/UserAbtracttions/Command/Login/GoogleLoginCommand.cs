using FRESHY.Authentication.Application.Abtractions.UserAbtracttions.Validattion;
using global::FRESHY.Authentication.Application.Interfaces;
using global::FRESHY.Authentication.Contract.Responses;
using global::FRESHY.Common.Domain.Common.Interfaces;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
namespace FRESHY.Authentication.Application.Abtractions.UserAbtracttions.Command.Login
{

    public record GoogleLoginCommand(

        string IdToken

        ) : IRequest<QueryResult<AuthResponse>>;

    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommand, QueryResult<AuthResponse>>
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtTokenGenerator _tokenService;
        private readonly IValidation _valiService;
        public GoogleLoginCommandHandler(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IJwtTokenGenerator tokenService, IValidation valiService)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _valiService = valiService ?? throw new ArgumentNullException(nameof(valiService));
        }

        public async Task<QueryResult<AuthResponse>> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
        {
            var result = new QueryResult<AuthResponse>();

            try
            {
                bool validateToken = await _valiService.ValidateIdTokenWithGoogle(request.IdToken);
                if (!validateToken)
                {
                    return new QueryResult<AuthResponse>(HttpStatusCode.BadRequest, "Invalid idToken.");
                }

                var (userEmail, userName) = await _valiService.ExtractUserInfoFromIdTokenAsync(request.IdToken);

                var existingUser = await _userManager.FindByEmailAsync(userEmail);
                if (existingUser == null)
                {
                    var newUser = new IdentityUser { UserName = userName, Email = userEmail };
                    var createResult = await _userManager.CreateAsync(newUser);
                    if (!createResult.Succeeded)
                    {
                        return new QueryResult<AuthResponse>(HttpStatusCode.InternalServerError, "Failed to create new user.");
                    }
                    existingUser = newUser;
                }
                

                var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, existingUser.Id),
                            new Claim(ClaimTypes.Name, existingUser.UserName),
                            new Claim(ClaimTypes.Email, existingUser.Email)
                        };
                var roles = await _userManager.GetRolesAsync(existingUser);
                if (roles == null || roles.Count == 0)
                {
                  
                    var addRoleResult = await _userManager.AddToRoleAsync(existingUser, "Customer");
                    if (!addRoleResult.Succeeded)
                    {
                        return new QueryResult<AuthResponse>(HttpStatusCode.InternalServerError, "Failed to add 'Customer' role.");
                    }
                  
                    roles = await _userManager.GetRolesAsync(existingUser);
                }

                // Thêm các vai trò khác nếu có
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }


                var token = _tokenService.Generator(claims);

                    var authResponse = new AuthResponse
                    {
                        UserId = existingUser.Id,
                        Name = existingUser.UserName,
                        Token = token,
                        Email = existingUser.Email,
                        Phone = existingUser.PhoneNumber,
                        Roles = roles.ToList()
                    };


                    result.Data = authResponse;
                    result.Succeeded = true;
                    result.Message = "Login Succeeded";
                    result.StatusCode = HttpStatusCode.OK;
               
            }
            catch (Exception ex)
            {
                return new QueryResult<AuthResponse>(HttpStatusCode.InternalServerError, "An error occurred while processing the request.");
            }

            return result;
        }

      
    }
}


