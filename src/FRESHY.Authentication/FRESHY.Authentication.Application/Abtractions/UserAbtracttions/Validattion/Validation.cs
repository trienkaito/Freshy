using FRESHY.Authentication.Application.Abtractions.UserAbtracttions.Validattion;
using Google.Apis.Auth;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FRESHY.Authentication.Application.Abtractions.UserAbtracttions.Share
{
    public class Validation : IValidation
    {
        async Task<bool> IValidation.ValidateIdTokenWithGoogle(string idToken)
        {
            try
            {
                // Validate the idToken with Google
                GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(idToken);

                // Check if the token is not expired
                if (payload.ExpirationTimeSeconds < DateTimeOffset.UtcNow.ToUnixTimeSeconds())
                {
                    // Token is expired
                    return false;
                }

                // Optionally, you can perform additional validation checks here

                // Token is valid
                return true;
            }
            catch (InvalidJwtException ex)
            {
                // Token is not valid
                Console.WriteLine("Invalid token: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                // Other exceptions
                Console.WriteLine("Error validating token: " + ex.Message);
                return false;
            }
        }

        async Task<(string email, string name)> IValidation.ExtractUserInfoFromIdTokenAsync(string idToken)
        {

            
                try
                {
                    // Validate the idToken with Google and extract user information
                    GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(idToken);

                    // Extract user's email and name from the payload
                    string email = payload.Email;
                    string name = Regex.Replace(payload.GivenName + payload.FamilyName, @"[^a-zA-Z0-9]", "");
                // Return the extracted information
                return (email, name);
                }
                catch (InvalidJwtException ex)
                {
                    // Token is not valid
                    Console.WriteLine("Invalid token: " + ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    // Other exceptions
                    Console.WriteLine("Error extracting user info from token: " + ex.Message);
                    throw;
                }
          
        }

        
    }
}
