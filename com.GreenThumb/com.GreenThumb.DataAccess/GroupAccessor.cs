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
        public static List<Group> GetGroupList(int OrganizationID, Active recordType = Active.active)
        {
            var groupList = new List<Group>();

            var conn = DBConnection.GetDBConnection();

            ///sent to Chris Sheenan 2/28 to add to database by Kristine Johnson

            string cmdText = @"Gardens.spSelectOrganization";



            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //int groupID, int organizationID,string groupName,int groupLeaderID, bool active
            cmd.Parameters.AddWithValue("@OrganizationId", OrganizationID);

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
    }
}
