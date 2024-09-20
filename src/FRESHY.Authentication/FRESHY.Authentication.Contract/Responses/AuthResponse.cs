using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRESHY.Authentication.Contract.Responses
{
    public class AuthResponse
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public List<string> Roles{get;set;}

        public AuthResponse()
        {
            
        }

        public AuthResponse(string userId, string name, string token, string email, string phone,List<string> roles)
        {
            UserId = userId;
            Name = name;
            Token = token;
            Roles = roles;
            Email = email;
            Phone = phone;

        }
    }
    
        
    

    

}
