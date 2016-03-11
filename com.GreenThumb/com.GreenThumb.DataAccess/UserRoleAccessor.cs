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
    public class UserRoleAccessor
    {
        /// <summary>
        /// Author: Ibrahim Abuzaid
        /// Data Transfer Object to represent a User from the
        /// Database
        /// 
        /// Added 3/4 By Ibarahim
        /// </summary>
        public static List<UserRole> FetchUserRoleList()
        {
            // create a list to hold the returned data
            var userRoleList = new List<UserRole>();

            // get a connection to the database
            var conn = DBConnection.GetDBConnection();

            // create a query to send through the connection

            string query = @"SELECT UserID, RoleID, CreatedBy, CreatedDate " +
                           @"FROM Admin.UserRoles ";
   
            query += @"ORDER BY UserID, RoleID "; 
       //     string cmdText = "spDisplayUserRole";

            // create a command object
            var cmd = new SqlCommand(query, conn);
    //        cmd.CommandType = CommandType.StoredProcedure;

      
            // Try catch block to deal with the data
            try
            {
                // open connection
                
                conn.Open();
               
                // execute the command and return a data reader
                SqlDataReader reader = cmd.ExecuteReader();

                // before trying to read the reader, be sure it has data
                if (reader.HasRows)
                {
                    // now we just need a loop to process the reader
                    while (reader.Read())
                    {
                        UserRole currentUserRole = new UserRole()
                        {
                            UserID = reader.GetInt32(0),
                            RoleID = reader.GetString(1),
                            CreatedBy = reader.GetInt32(2),
                            CreatedDate = reader.GetDateTime(3)
                        };
                      
                        userRoleList.Add(currentUserRole);
                    }
                }
           
            }
            catch (Exception)
            {
                // rethrow all Exceptions, let the logic layer sort them out
                throw;
            }
            finally
            {
                conn.Close();
            }
            // this list may be empty, if so, the logic layer will need to deal with it
            return userRoleList;
        }

        public static int FetchUserRoleCount()
        {
            int count = 0;

            // let's try a scalar query

            // start with a connection object
            var conn = DBConnection.GetDBConnection();

            // prepare the querry command
            string query = @"SELECT COUNT(*) " +
                           @"FROM Admin.UserRoles ";

            var cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();

                count = (int)cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }

            return count;
        }

        public static int InsertUserRole(UserRole userRole)
        {
            int count = 0;

            // prepare the data connection
            var conn = DBConnection.GetDBConnection();

            // What comes next is a command text
            string query = @"INSERT INTO Admin.UserRoles " +
                           @"(UserID, RoleID, CreatedBy, CreatedDate " +
                           @"VALUES " +
                           @"('" + userRole.UserID + "', '" + userRole.RoleID + "', '" + 
                           @"', '" + userRole.CreatedBy + "', '" + userRole.CreatedDate + "') ";

            // get a command object
            var cmd = new SqlCommand(query, conn);

            try
            {
                // open the connection
                conn.Open();

                // execute the command with ExecuteNonQuery()
                count = cmd.ExecuteNonQuery();
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

        public static int UpdateUserRole(UserRole userRole)
        {
            int rowCount = 0;

            // begin with a connection
            var conn = DBConnection.GetDBConnection();

            // get some commandText
            string cmdText = "spUpdateUserRoles";

            // create a command object
            var cmd = new SqlCommand(cmdText, conn);

            // here is where things change a bit
            // first, we need to set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // we need to construct and add the parameters

            // this is the all-at-once way
            cmd.Parameters.Add(new SqlParameter("UserD", SqlDbType.Int)).Value = userRole.UserID;
            cmd.Parameters.Add(new SqlParameter("RoleID", SqlDbType.VarChar)).Value = userRole.RoleID;
            cmd.Parameters.Add(new SqlParameter("CreatedBy", SqlDbType.Int)).Value = userRole.CreatedBy;
            cmd.Parameters.Add(new SqlParameter("CreatedDate", SqlDbType.VarChar)).Value = userRole.CreatedDate;

            // we can also create an output parameter
            var o = new SqlParameter("Rowcount", SqlDbType.Int);
            o.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(o);

            try
            {
                // open the connection
                conn.Open();

                // execute  the command
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
            return rowCount;
        }
    }
}
