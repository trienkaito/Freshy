using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRESHY.Authentication.Contract.Responses
{
    public class ForgetResponse
    {  
       public int OTP { get; set; }
        public ForgetResponse(int otp)
        {
            OTP = otp;
        }
        public ForgetResponse()
        {
            
        }

    }
    
}
