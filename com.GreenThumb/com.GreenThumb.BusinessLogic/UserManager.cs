using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;

namespace com.GreenThumb.BusinessLogic
{
    public class UserManager
    {
        ///<summary>
        ///Author: Chris Schwebach
        ///EditUserPersonalInfo validates input from user calling to the UserAccessor
        ///Date: 3/3/16
        ///</summary>
        public bool EditUserPersonalInfo(int userID, string firstName, string lastName, string zip, string emailAddress, int? regionId)
        {
           bool result = false;

            if (firstName.Length < 1 || firstName.Length > 50 || firstName.Equals(""))
            {
                throw new ApplicationException("Invalid First Name! First name must be between 1 and 50 characters in length");
            }
            else if (lastName.Length < 1 || lastName.Length > 100)
            {
                throw new ApplicationException("Invalid Last Name! Last name must be between 1 and 100 characters in length");
            }
            else if (zip != "" && zip.Length != 9)
            {
                throw new ApplicationException("Invalid zip! Zip must be 9 characters in length.");
            }
            else if (emailAddress.Length > 100)
            {
                throw new ApplicationException("Invalid Email Address! Email must be less than 100 characters in length.");
            }

            try
            {

                var count = UserAccessor.UpdateUserPersonalInfo(userID, firstName, lastName, zip, emailAddress, regionId);
                result = 0 != count ? true : false;
                if (count != 0)
                {
                    result = true;
                }
                else if (count == 0)
                {
                    result = false;
                }
                else
                {
                    throw new ApplicationException("Multiple Record Found!");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        ///<summary>
        ///Author: Chris Schwebach
        ///GetUserPersonalInfo get the Personal information from user based on accessToken.UserID
        ///calling to the user accessor
        ///Date: 3/3/16
        ///</summary>
        public List<User> GetPersonalInfo(int userID)
        {
            try
            {
                return UserAccessor.FetchPersonalInfo(userID);
            }
            catch (ApplicationException)
            {
                throw new ApplicationException("No records found");
            }
        }
    }
}
