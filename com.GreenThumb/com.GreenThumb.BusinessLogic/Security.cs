using BusinessObjects;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogic
{
    public class Security
    {
        const int MIN_USERNAME = 5;
        const int MIN_PASSWORD = 5;
        public static AccessToken ValidateExistingUser(string username, string password)
        {
            AccessToken accessToken;

            if (username.Length < MIN_USERNAME || password.Length < MIN_PASSWORD)
            {
                throw new ApplicationException("Invalid username or password.");
            }

            try
            {
                if (1 == UserAccessor.FindUserByUsernameAndPassword(username, password))
                {
                    var user = UserAccessor.RetrieveUserByUsername(username);
                    var roles = UserAccessor.RetrieveRolesByUserName(user.UserName);
                    accessToken = new AccessToken();
                }
                else
                {
                    throw new ApplicationException("Data not found.");
                }
            }
            catch
            {
                throw;
            }
            return accessToken;
        }
        public static AccessToken ValidateNewUser(string username, string newPassword)
        {
            // check for new user
            if (1 == UserAccessor.FindUserByUsernameAndPassword(username, "NEWUSER"))
            {
                UserAccessor.SetPasswordForUsername(username, "NEWUSER", newPassword);
            }
            else
            {
                throw new ApplicationException("Data not found.");
            }

            return ValidateExistingUser(username, newPassword);
        }
    }
}
