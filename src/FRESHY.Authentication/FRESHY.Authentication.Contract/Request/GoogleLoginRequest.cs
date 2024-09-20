using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRESHY.Authentication.Contract.Request
{
    public record GoogleLoginRequest
    (
          string IdToken
        );
}
