using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRESHY.SharedKernel.Interfaces
{
    public interface IEmailService
    {
        public  Task<int> SendPasswordResetEmailAsync(string email);
    }
}
