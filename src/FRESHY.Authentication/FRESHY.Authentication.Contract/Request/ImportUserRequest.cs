using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRESHY.Authentication.Contract.Request
{
    public record  ImportUserRequest
    (
       string UserName,
    string Password,
    string Email,
    string PhoneNumber,
    List<string> Roles
        );
}
