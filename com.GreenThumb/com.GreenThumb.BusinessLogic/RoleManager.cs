using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessLogic
{
    public class RoleManager
    {
        /// <summary>
        /// Author: Ibrahim Abuzaid
        /// Data Transfer Object to represent a User from the
        /// Database
        /// 
        /// Added 3/4 By Ibarahim
        /// </summary>
        public List<Role> GetRoleList()
        {
            try
            {
                var roleList = RoleAccessor.FetchRoleList();

                if (roleList.Count > 0)
                {
                    return roleList;
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

        public int GetRoleCount()
        {
            try
            {
                return RoleAccessor.FetchRoleCount();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AddNewRole(string roleId,
                               string description,
                               int createdBy,
                               DateTime createdDate)
        {
            try
            {
                var role = new Role()
                {
                    RoleID  = roleId,
                    Description = description,
                    CreatedBy = createdBy,
                    CreatedDate = createdDate
                };
                if (RoleAccessor.InsertRole(role) == 1)
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
