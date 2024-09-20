using FRESHY.Authentication.Application.Abtractions.UserAbtracttions.Validattion;
using FRESHY.Authentication.Application.Interfaces;
using FRESHY.Authentication.Contract.Responses;
using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace FRESHY.Authentication.Application.Abtractions.UserAbtracttions.Command.RegisterCommand
{
    public record GoogleRegisterCommand(string IdToken) : IRequest<QueryResult<AuthResponse>>;

    public class GoogleRegisterCommandHandler : IRequestHandler<GoogleRegisterCommand, QueryResult<AuthResponse>>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtTokenGenerator _tokenService;
        private readonly IValidation _valiService;

        public GoogleRegisterCommandHandler(UserManager<IdentityUser> userManager, IJwtTokenGenerator tokenService, IValidation valiService
           )
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _valiService = valiService ?? throw new ArgumentNullException(nameof(valiService));
        }

        public async Task<QueryResult<AuthResponse>> Handle(GoogleRegisterCommand request, CancellationToken cancellationToken)
        {
            var result = new QueryResult<AuthResponse>();

            try
            {
                bool validateToken = await _valiService.ValidateIdTokenWithGoogle(request.IdToken);
                // Validate the idToken with Google
                if (!validateToken)
                {
                    return new QueryResult<AuthResponse>(HttpStatusCode.BadRequest, "Invalid idToken.");
                }

                var (userEmail, userName) = await _valiService.ExtractUserInfoFromIdTokenAsync(request.IdToken);

                // Check if the user already exists
                var existingUser = await _userManager.FindByEmailAsync(userEmail);
                if (existingUser != null)
                {
                    return new QueryResult<AuthResponse>(HttpStatusCode.BadRequest, "User already exists.");
                }

                // If user does not exist, create a new user
                var newUser = new IdentityUser { UserName = userName, Email = userEmail };
                var createResult = await _userManager.CreateAsync(newUser);
                if (!createResult.Succeeded)
                {
                    return new QueryResult<AuthResponse>(HttpStatusCode.InternalServerError, "Failed to create new user.");
                }

                // Generate JWT token for the user
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, newUser.Id),
                    new Claim(ClaimTypes.Name, newUser.UserName),
                    new Claim(ClaimTypes.Email, newUser.Email)
                    // Add more claims if needed
                };

                var token = _tokenService.Generator(claims);

                // Prepare AuthResponse
                var authResponse = new AuthResponse
                {
                    UserId = existingUser.Id,
                    Name = existingUser.UserName,
                    Token = token,
                };


                result.Data = authResponse;
                result.Succeeded = true;
                result.Message = "User registered successfully.";
                result.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return new QueryResult<AuthResponse>(HttpStatusCode.InternalServerError, "An error occurred while processing the request.");
            }

            return result;
        }
    }
}
