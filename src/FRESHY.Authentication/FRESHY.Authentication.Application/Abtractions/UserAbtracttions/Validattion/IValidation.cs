using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRESHY.Authentication.Application.Abtractions.UserAbtracttions.Validattion
{
    public interface IValidation
    {
       public Task<bool> ValidateIdTokenWithGoogle(string idToken);
       public Task<(string email, string name)> ExtractUserInfoFromIdTokenAsync(string idToken);
    }
}
