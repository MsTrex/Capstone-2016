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
                        Group currentGroup = new Group(){
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

        public static int InsertGroupMembers(int userID, int groupID, int createdBy, DateTime createdDate, bool isLeader)
        {
            string query = @"Gardens.spInsertGroupMembers";
            int rowCount = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupID", userID);
            cmd.Parameters.AddWithValue("@UserID", groupID);
            cmd.Parameters.AddWithValue("@CreatedDate", createdDate);
            cmd.Parameters.AddWithValue("@CreatedBy", createdBy);
            cmd.Parameters.AddWithValue("@Leader", isLeader);

            try
            {
                conn.Open();
                rowCount = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return rowCount = 2;
            }
            finally
            {
                conn.Close();
            }

            return rowCount = 1;
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

    }
}
