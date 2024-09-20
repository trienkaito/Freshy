using FRESHY.Authentication.Application.Abtractions.UserAbtracttions.Command.ImportUser;
using FRESHY.Authentication.Application.Abtractions.UserAbtracttions.Command.Login;
using FRESHY.Authentication.Contract.Request;
using FRESHY.Main.Application.Abstractions.CustomerProfileAbstactions.Commands.CreateCustomerProfile;
using FRESHY.Main.Contract.Requests.CustomerRequests;
using FRESHY.Main.Contract.Responses.CustomerResponses;
using Mapster;

namespace FRESHY_Service.Config.Mapster
{
    public class RegisterMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ImportUserRequest, ImportUserCommand>();
            config.NewConfig<UserLoginRequest, UserLoginCommand>();
            config.NewConfig<GoogleLoginRequest, GoogleLoginCommand>();
        }
    }
}
