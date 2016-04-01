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

        /// <summary>
        /// Ryan Taylor and Luke Frahm
        /// Created: 03/31/16
        /// Check to see if the user is a leader in the group
        /// </summary>
        /// <param name="userID">ID of user to query</param>
        /// <param name="groupID">ID of the group to check for leader</param>
        /// <returns>
        /// Boolean of leader status in selected group
        /// </returns>
        public bool GetLeaderStatus(int userID, int groupID)
        {
            try
            {
                return GroupAccessor.GroupLeaderStatus(userID, groupID);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Ryan Taylor and Luke Frahm
        /// Created: 03/31/16
        /// Check to see if the user is a leader in the group
        /// </summary>
        /// <param name="groupID">ID of the group to check for leader</param>
        /// <param name="newGroupName">New name to be declared</param>
        /// <param name="oldGroupName">Old name to be replaced</param>
        /// <returns>
        /// Boolean of result: success or failure
        /// </returns>
        public bool ChangeGroupName(int groupID, string newGroupName, string oldGroupName)
        {
            try
            {
                return GroupAccessor.UpdateGroupName(groupID, newGroupName, oldGroupName);
            }
            catch (Exception)
            {

                throw new ApplicationException("Group name could not be changed.");
            }
        }

        /// <summary>
        /// Luke Frahm
        /// Created: 03/31/16
        /// Set the group to Active = false
        /// </summary>
        /// <param name="group">Group object containing name of group to be deactivated</param>
        /// <returns>
        /// Boolean of result for deactivating group
        /// </returns>
        public bool DeactivateGroup(Group group)
        {
            try
            {
                return GroupAccessor.DeactivateGroupByID(group.GroupID);
            }
            catch (Exception)
            {

                throw new ApplicationException("Group could not be closed.");
            }
        }

        /// <summary>
        /// Ryan Taylor
        /// Created 03/31/16
        /// Get The Members of a group based on the groupID
        /// </summary>
        /// <param name="groupID">ID of the group</param>
        /// <returns>List of Groupmembers</returns>
        public List<GroupMember> GetGroupMembers(int groupID)
        {
            List<GroupMember> memberList;

            try
            {
                memberList = GroupAccessor.GetMemberList(groupID);
            }
            catch (Exception)
            {
                throw new ApplicationException("Unable to retrieve members for this group.");
            }

            return memberList;
        }
    }
}
