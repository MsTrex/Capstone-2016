using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BussinessLogic
{
    public class UserRoleManager
    {
        public List<UserRole> GetUserRoleList()
        {
            /// <summary>
            /// Author: Ibrahim Abuzaid
            /// Data Transfer Object to represent a User from the
            /// Database
            /// 
            /// Added 3/25 By Ibarahim
            /// </summary>
            try
            {
                var userRoleList = UserRoleAccessor.FetchUserRoleList();

                if (userRoleList.Count > 0)
                {
                    return userRoleList;
                }
                else
                {
                    throw new ApplicationException("There were no records found.");
                }
            }

            catch (Exception)
            {
                // *** we should sort the possible exceptions and return friendly messages for each
                Console.Out.WriteLine("Exception Handler on Role manager Class...");
                throw;
            }
        }

        public int GetUserRoleCount()
        {
            try
            {
                return UserRoleAccessor.FetchUserRoleCount();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AddNewUserRole(int userId,
                               string roleId,
                               int createdBy,
                               DateTime createdDate)
        {
            try
            {
                var userRole = new UserRole()
                {
                    UserID = userId,
                    RoleID = roleId,
                    CreatedBy = createdBy,
                    CreatedDate = createdDate
                };
                if (UserRoleAccessor.InsertUserRole(userRole) == 1)
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
        // should not be used after approval
        public bool ChangeUserRole(UserRole userRole)
        {
            if (userRole.UserID < 1000)
            {
                throw new ApplicationException("Invalid userID");
            }
            
            try
            {
                if (UserRoleAccessor.UpdateUserRole(userRole) == 1)
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
        // should be replaced by ChangeUserRoleStatus
        public bool DeleteUserRole(int usr, string role)
        {
            if (usr < 1000)
            {
                throw new ApplicationException("Invalid userID");
            }

            try
            {
                if (UserRoleAccessor.UpdateUserRoleRemove(usr, role) == 1)
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

        public bool ChangeUserRoleStatus(int usr, string role, bool active)
        {
            if (usr < 1000)
            {
                throw new ApplicationException("Invalid userID");
            }

            try
            {
                if (UserRoleAccessor.UpdateUserRoleStatus(usr, role, active) == 1)
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
    }
}
