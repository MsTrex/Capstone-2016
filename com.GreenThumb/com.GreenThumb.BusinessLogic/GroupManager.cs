using com.GreenThumb.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.DataAccess;

/// <summary>
/// Created by Kristine Johnson 2/28/16
/// Selects a list of groups within an organziation
/// </summary>

namespace com.GreenThumb.BusinessLogic
{
        public class GroupManager : com.GreenThumb.BusinessLogic.Interfaces.IGroupManager
    {
        public List<Group> GetGroupList(int OrganizationID)
        {
           
            try
            {
                return GroupAccessor.GetGroupList(OrganizationID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException(ex.Message);
            }
        }


        /// <summary>
        /// Ryan Taylor
        /// Created: 03/21/16
        /// Get all groups the associated user is affiliated with
        /// </summary>
        /// <param name="userID">ID of user used for search, passed from Access Token</param>
        /// <returns>List of Groups the user belongs to</returns>
        public List<Group> GetGroupsForUser(int userID)
        {
            List<Group> groups = null;

            try
            {
                groups = GroupAccessor.GetUsersGroups(userID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

            return groups;
        }


        /// <summary>
        /// Ryan Taylor
        /// Created: 03/21/16
        /// Add a new group that this user will be the leader of
        /// </summary>
        /// <param name="userID">ID of user creating the group, passed from Access Token</param>
        /// <param name="groupName">The name of the new group</param>
        /// <returns></returns>
        public bool AddGroup(int userID, string groupName)
        {
            try
            {
                return GroupAccessor.CreateGroup(userID, groupName);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}
