using com.GreenThumb.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.DataAccess
{
    /// <summary>
    /// Created by Kristine Johnson 2/28/16
    /// Takes an organization and gets a connection to the database to pull a list of groups.
    /// </summary>
    public class GroupAccessor
    {
        public static List<Group> GetGroupList(int userID, Active recordType = Active.active)
        {
            var groupList = new List<Group>();

            var conn = DBConnection.GetDBConnection();

            ///sent to Chris Sheenan 2/28 to add to database by Kristine Johnson

            string cmdText = @"Gardens.spSelectUserGroups";



            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //int groupID, int organizationID,string groupName,int groupLeaderID, bool active
            cmd.Parameters.AddWithValue("@UserID", userID);

            //// we can also create an output parameter
            //var o = new SqlParameter("Group", SqlDbType.I);
            //o.Direction = ParameterDirection.ReturnValue;
            //cmd.Parameters.Add(o);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    // now we just need a loop to process the reader
                    while (reader.Read())
                    {
                        Group currentGroup = new Group()
                        {  //int groupID, int organizationID,string groupName,int groupLeaderID, bool active
                            GroupID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            GroupLeaderID = reader.GetInt32(2)

                        };
                        groupList.Add(currentGroup); ///returns a group list
                    }
                }
                else
                {
                    var ax = new ApplicationException("No group was found");
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return groupList;
        }

        /// <summary>
        /// 
        /// Created by: Nicholas King 04/03/2016
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static List<Group> FetchJoinableGroups(int UserID)
        {
            List<Group> joinableGroups = new List<Group>();
            var conn = DBConnection.GetDBConnection();
            string cmdText = "Gardens.spSelectJoinableGroups";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@Active", 1);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        joinableGroups.Add(new Group()
                        {
                            GroupID
                                = reader.GetInt32(0),
                            Name
                                = reader.GetString(1),
                            Active
                                = reader.GetBoolean(2),
                            GroupLeader = new GroupMember()
                            {
                                User = new User()
                                {
                                    UserID
                                        = reader.GetInt32(3),
                                    UserName
                                        = reader.GetString(4),
                                    FirstName
                                        = reader.GetString(5),
                                    LastName
                                        = reader.GetString(6),
                                    EmailAddress
                                        = reader.GetString(7)
                                }
                            }
                        });
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return joinableGroups;
        }

        /// <summary>
        /// added by Nicholas King
        /// creates a group leader request
        /// </summary>
        public static int InsertGroupLeaderRequest(int userID, int groupID, DateTime time)
        {
            int count = 0;
            var conn = DBConnection.GetDBConnection();
            var query = @"Gardens.spInsertGroupLeaderRequest";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@userID", userID);
            cmd.Parameters.AddWithValue("@groupID", groupID);
            cmd.Parameters.AddWithValue("@RequestDate", time);
            cmd.Parameters.AddWithValue("@ModifiedDate", DBNull.Value);
            cmd.Parameters.AddWithValue("@ModifiedBy", DBNull.Value);
            cmd.Parameters.AddWithValue("@RequestActive", 1);

            try
            {
                conn.Open();
                count = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return count;
        }


        /// <summary>
        /// added by Nicholas King
        /// gets a list of all groups the user is in
        /// </summary>
        public static List<Group> GetUsersGroups(int userID, Active recordType = Active.active)
        {
            var groupList = new List<Group>();

            var conn = DBConnection.GetDBConnection();

            string cmdText = @"Gardens.spSelectUserGroups";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", userID);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Group currentGroup = new Group()
                        {
                            GroupID = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                        groupList.Add(currentGroup);
                    }
                }
                else
                {
                    var msg = new ApplicationException("No groups was found");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return groupList;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created 03/23/16
        /// Inserts a new group into the database
        /// </summary>
        /// <param name="userID">ID of user creating the group</param>
        /// <param name="groupName">Name of the new group to be added</param>
        /// <returns>True if data was added, False otherwise</returns>
        public static bool CreateGroup(int userID, string groupName)
        {
            int rowCount = 0;

            var conn = DBConnection.GetDBConnection();
            string query = @"Gardens.spInsertGroups";
            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupName", groupName);
            cmd.Parameters.AddWithValue("@GroupLeaderID", userID);
            cmd.Parameters.AddWithValue("@OrganizationID", 0);

            try
            {
                conn.Open();
                rowCount = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowCount == 1;
        }

        /// <summary>
        /// Ryan Taylor and Luke Frahm
        /// Created 03/31/16
        /// Query database to determine of the supplied user is a leader in the group.
        /// </summary>
        /// <param name="userID">ID of user to check status</param>
        /// <param name="groupID">ID of the group to query for user status</param>
        /// <returns>True if data was added, False otherwise</returns>
        public static bool GroupLeaderStatus(int userID, int groupID)
        {
            bool isLeader = false;

            var conn = DBConnection.GetDBConnection();
            string query = @"Gardens.spCheckLeaderStatus";
            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupId", groupID);
            cmd.Parameters.AddWithValue("@UserId", userID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    isLeader = reader.GetBoolean(0);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isLeader;
        }

        /// <summary>
        /// Ryan Taylor and Luke Frahm
        /// Created 03/31/16
        /// Update database to reflect new group name.
        /// </summary>
        /// <param name="groupID">ID of the group to alter</param>
        /// <param name="newGroupName">New name to be declared</param>
        /// <param name="oldGroupName">Old name to be replaced</param>
        /// <returns>True if data was updated, False otherwise</returns>
        public static bool UpdateGroupName(int groupID, string newGroupName, string oldGroupName)
        {
            int rowCount = 0;

            var conn = DBConnection.GetDBConnection();
            string query = @"Gardens.spUpdateGroupName";
            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupID", groupID);
            cmd.Parameters.AddWithValue("@NewGroupName", newGroupName);
            cmd.Parameters.AddWithValue("@OldGroupName", oldGroupName);

            try
            {
                conn.Open();
                rowCount = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowCount == 1;
        }

        /// <summary>
        /// Poonam Dubey
        /// 04/06/2016
        /// Function to call DB and insert groupmember request
        /// </summary>
        /// <param name="reqObj"></param>
        /// <returns></returns>
        public static int AddGroupMember(GroupRequest reqObj)
        {
            string query = @"Admin.spInsertGroupRequest";
            int rowCount = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupID", reqObj.GroupID);
            cmd.Parameters.AddWithValue("@UserID", reqObj.UserID);
            cmd.Parameters.AddWithValue("@RequestStatus", reqObj.RequestStatus);
            cmd.Parameters.AddWithValue("@RequestDate", reqObj.RequestDate);

            try
            {
                conn.Open();
                rowCount = (int)cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowCount;

        }

        /// <summary>
        /// Luke Frahm
        /// Created 03/31/16
        /// Update database to set this group to inactive.
        /// </summary>
        /// <param name="groupID">ID of the group to deactivate</param>
        /// <returns>True if deactivated, False otherwise</returns>
        public static bool DeactivateGroupByID(int groupID)
        {
            int rowCount = 0;

            var conn = DBConnection.GetDBConnection();
            string query = @"Gardens.spDeactivateGroupByID";
            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupID", groupID);
            cmd.Parameters.AddWithValue("@Active", 0);

            try
            {
                conn.Open();
                rowCount = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowCount == 1;
        }

        /// <summary>
        /// "But Trent... There are already two of these!" 
        /// Yes. Yes there is, and now there is a third.
        /// 
        /// Created By: Trent Cullinan 02/31/2016
        /// </summary>
        /// <param name="userId">User Id to get groups for.</param>
        /// <returns>Collection of groups that the user belongs to.</returns>
        public static IEnumerable<Group> RetrieveUserGroups(int userId)
        {
            IList<Group> groups = null;

            groups = new List<Group>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Gardens.spSelectUserGroups", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", userId);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    groups = new List<Group>();

                    while (reader.Read())
                    {
                        groups.Add(new Group()
                        {
                            GroupID
                                = reader.GetInt32(0),
                            Name
                                = reader.GetString(1),
                            Active
                                = reader.GetBoolean(2),
                            GroupLeader = new GroupMember()
                            {
                                User = new User()
                                {
                                    UserID
                                        = reader.GetInt32(3),
                                    UserName
                                        = reader.GetString(4),
                                    FirstName
                                        = reader.GetString(5),
                                    LastName
                                        = reader.GetString(6),
                                    EmailAddress
                                        = reader.GetString(7)
                                }
                            }
                        });
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return groups;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created 03/31/16
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns>Members associtated with groupID</returns>
        public static List<GroupMember> GetMemberList(int groupID)
        {
            var memberList = new List<GroupMember>();
            var conn = DBConnection.GetDBConnection();
            string cmdText = @"Gardens.spSelectGroupMembers";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupID", groupID);
            // get all the users already accepted
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        GroupMember currentMember = new GroupMember()
                        {
                            User = new User
                            {
                                UserID = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2)
                            },
                            Status = reader.GetString(3)
                        };
                        memberList.Add(currentMember);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return memberList;



        }

        /// <summary>
        /// Modifies the group member to either be 
        /// active or inactive for a particular group.
        /// 
        /// Created By: Trent Cullinan 02/31/2016
        /// </summary>
        /// <param name="userId">User from group to be modified.</param>
        /// <param name="groupId">Group the user belongs to.</param>
        /// <returns>Rows affected by change.</returns>
        public static int InactivateGroupMember(int userId, int groupId)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Gardens.spUpdateGroupMember", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID",
                userId);
            cmd.Parameters.AddWithValue("@GroupID",
                groupId);

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }


        /// <summary>
        /// Poonam Dubey
        /// 04/06/2016
        /// Function to fetch groups to view and request to join
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="recordType"></param>
        /// <returns></returns>
        public static List<Group> GetGroupsToView(int userID)
        {
            var groupList = new List<Group>();

            var conn = DBConnection.GetDBConnection();

            string cmdText = @"Gardens.spSelectGroupsToView";



            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", userID);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Group currentGroup = new Group()
                        { 
                            GroupID = reader.GetInt32(0),
                            Name = reader.GetString(1)

                        };
                        groupList.Add(currentGroup); ///returns a group list
                    }
                }
                else
                {
                    var ax = new ApplicationException("No group was found");
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return groupList;
        }
    }
}
