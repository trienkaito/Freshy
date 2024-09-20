using System.Threading;
using System.Threading.Tasks;
using FRESHY.Authentication.Application.Interfaces.Persistance;
using FRESHY.Common.Application.Interfaces.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FRESHY.Authentication.Application.Abtractions.UserAbtracttions.Command.ImportUser
{
    public record ImportUserCommand
    (
        string UserName,
        string Password,
        string Email,
        string PhoneNumber,
        List<string> Roles
) : ICommand<bool>;
    public class ImportUserCommandHandler : IRequestHandler<ImportUserCommand, bool>
    {
        private readonly IAccountRepository _accountRepository;

        public ImportUserCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<bool> Handle(ImportUserCommand request, CancellationToken cancellationToken)
        {
            // Tạo mới một đối tượng IdentityUser từ dữ liệu yêu cầu
            var newUser = new IdentityUser
            {
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
            };

            // Gọi phương thức CreateUserAsync từ IAccountRepository để tạo mới người dùng
            var isSucceed = await _accountRepository.RegisterUserAsync(newUser, request.Password, request.Roles);

            return isSucceed;
        }
    }
}
