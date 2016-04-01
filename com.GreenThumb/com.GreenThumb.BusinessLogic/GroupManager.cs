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
        public List<Group> GetGroupList(int userID)
        {

            try
            {
                return GroupAccessor.GetGroupList(userID);
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

        public int AddGroupMember(int userID, int groupID, int createdBy)
        {
            try
            {

                return GroupAccessor.InsertGroupMembers(userID, groupID, createdBy, DateTime.Now, false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Retrieve a collection of groups with a group leader that the user id is a member of.
        /// 
        /// Created by: Trent Cullinan 03/31/2016
        /// </summary>
        /// <param name="userId">User Id of user to retrieve groups for.</param>
        /// <returns>Collection of groups that a user belongs to.</returns>
        public IEnumerable<Group> RetrieveUserGroups(int userId)
        {
            IEnumerable<Group> groups = new List<Group>(); // Empty collection to return

            try
            {
                groups = GroupAccessor.RetrieveUserGroups(userId);
            }
            catch (Exception) { } // groups will be an empty collection

            return groups;
        }

        /// <summary>
        /// Marks the user as inactive for the group.
        /// 
        /// Created by: Trent Cullinan 03/31/2016
        /// </summary>
        /// <param name="userId">User Id of user leaving.</param>
        /// <param name="groupId">Group Id of which group.</param>
        /// <returns>Whether the group removal was successful.</returns>
        public bool LeaveGroup(int userId, int groupId)
        {
            bool flag = false;

            try
            {
                // 1 row should be affected
                flag =
                    1 == GroupAccessor.InactivateGroupMember(userId, groupId);
            }
            catch (Exception) { } // flag set to false

            return flag;
        }
    }
}
