/// <summary>
/// Ryan Taylor and ... 
/// Created: 2016/02/25
/// </summary> 
/// <remarks>
/// Updated code and added more methods - Ryan Taylor 2016/03/03
/// </remarks>

using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinesssLogic
{
    public class SecurityManager
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
                if (1 == UserAccessor.FindUserByUsernameAndPassword(username, password.HashSha256()))
                {
                    var user = UserAccessor.RetrieveUserByUsername(username);
                    var roles = UserAccessor.RetrieveRolesByUserID(user.UserID);
                    var activeRoles = roles.Where(r => r.Active == true).ToList();
                    accessToken = new AccessToken(user, activeRoles);
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
                UserAccessor.SetPasswordForUsername(username, "NEWUSER", newPassword.HashSha256());
            }
            else
            {
                throw new ApplicationException("Data not found.");
            }

            return ValidateExistingUser(username, newPassword);
        }
    }
}
