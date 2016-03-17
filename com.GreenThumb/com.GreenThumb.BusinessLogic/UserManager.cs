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
        ///Updated Date: 3/8/16
        ///Updated regionID user input parameters
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
            else if (regionId < 1 || regionId > 9)
            {
                throw new ApplicationException("Invalid RegionID! Must be a nuemeric value between 1 and 10.");
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

        /// <summary>
        /// Author: Ibrahim Abuzaid
        /// Data Transfer Object to represent a User from the
        /// Database
        /// 
        /// Added 3/4 By Ibarahim
        /// </summary>
       public List<User> GetUserList(Active group = Active.active)
        {
            try
            {
                var userList = UserAccessor.FetchUserList(group);

                if (userList.Count > 0)
                {
                    return userList;
                }
                else
                {
                    throw new ApplicationException("There were no records found.");
                }
            }
            
            catch (Exception)
            {
                // *** we should sort the possible exceptions and return friendly messages for each
                throw;
            }
        }

        public int GetUserCount(Active group = Active.active)
        {
            try
            {
                return UserAccessor.FetchUserCount(group);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int AddNewUser(string firstName,
                                   string lastName,
                                   string zip,
                                   string emailAddress,
                                   string userName,
                                   string passWord,
                                   bool   active,
                                   int?    regionID)
        {
            try
            {
                var usr = new User()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Zip = zip,
                    EmailAddress = emailAddress,
                    UserName = userName,
                    Password = passWord,
                    Active = active,
                    RegionId= regionID
                };
                return UserAccessor.InsertUser(usr);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ChangeUserData(User usr)
        {
           //                 var usr = new User()

            if (usr.UserID < 1000)
            {
                throw new ApplicationException("Invalid UserID");
            }
            
            try
            {
                if(UserAccessor.UpdateUser(usr)==1)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }

        ///<summary>
        ///Author: Stenner Kvindlog         
        ///GetUserByUserName gets a user by username 
		//calling to the user accessor
        ///Date: 3/4/16
		///</summary>

        public User GetUserByUserName(string username)
        {
            try
            {
                return UserAccessor.RetrieveUserByUsername(username);
            }
            catch (Exception)
            {
                throw;
            }

        }

		///<summary>
        ///Author: Stenner Kvindlog         
        ///fetchUser gets a user by userID 
		//calling to the user accessor
        ///Date: 3/4/16
		///</summary>
        public User FetchUser(int userId)
        {
            return UserAccessor.RetrieveUserByID(userId);
        }


		///<summary>
        ///Author: Stenner Kvindlog         
        ///createUser sends user to database to be created  
		//calling to the user accessor
        ///Date: 3/4/16
		///</summary>
        public int createUser(User newUser)
        {
            try
            {
                int num = UserAccessor.InsertUser(newUser);
                return num;
            }
            catch (Exception)
            {

                throw;
            }
        }

		///<summary>
        ///Author: Stenner Kvindlog         
        ///editUser sends old and new user to database to edit user 
		//calling to the user accessor
        ///Date: 3/4/16
		///</summary>
        public bool EditUser(User newUser, User oldUser)
        {
            try
            {
                bool flag = UserAccessor.EditUser(newUser, oldUser);
                return flag;
            }
            catch (Exception)
            {

                throw;
            }

        }
      }
}
