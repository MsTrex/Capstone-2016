using com.GreenThumb.BusinessObject;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessLogic
{
    public class UserRegionManager
    {
        public List<User> GetUserList(Active group = Active.active)
        {

            try
            {
                var userList = UserRegionAccessor.RetrieveUserList(group);

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
                return UserRegionAccessor.RetrieveUserCount(group);
            }
            catch (Exception)
            {

                throw;
            }
        }

   /*     public bool AddNewUser(string firstName,
                                   string lastName,
                                   string zip,
                                   string emailAddress,
                                   string userName,
                                   string passWord,
            //             bool   active,
                                   int regionID)
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
                    //       Active = active,
                    RegionID = regionID
                };
                if (UserRegionAccessor.InsertUser(usr) == 1)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        } */
        public bool ChangeUserData(int userID, int regionID)
        {
            //                 var usr = new User()

     /*       if (userID < 1000)
            {
                throw new ApplicationException("Invalid UserID");
            }  */

            try
            {
                if (UserRegionAccessor.UpdateUser(userID, regionID) == 1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        
         public User DisplayUserRecord(int userID)
        {
            User usr = null;

       /*     if (userID < 1000)
            {
                throw new ApplicationException("Invalid UserID");
            }  */

            try
            {
                usr = UserRegionAccessor.RetrieveUser(userID);            
                return usr;
            }
            catch (Exception)
            {
                throw;
            }
             
        }
    }
}
