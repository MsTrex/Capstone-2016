using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserAccessor
    {
        //private IUserInterface iUser;
        public static User RetrieveUserByUsername(string username)
        {
            User user;
            var conn = DBConnection.GetDBConnection();
            var query = @"spSelectuser";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@username", username);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    user = new User()
                    {
                        UserName = reader.GetString(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Zip = reader.GetInt32(3),
                        EmailAddress = reader.GetString(4),
                        RegionId = reader.GetString(5),
                        Active = reader.GetBoolean(5)
                    };
                }
                else
                {
                    throw new ApplicationException("Data not found");
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
            return user;
        }

        public static object RetrieveRolesByUserName(string userName)
        {
            throw new NotImplementedException();
        }


        public static int FindUserByUsernameAndPassword(string username, string password)
        {
            int count = 0;
            var conn = DBConnection.GetDBConnection();
            var query = @"spValidateActiveUser";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            try
            {
                conn.Open();
                count = (int)cmd.ExecuteScalar();
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

        public static int SetPasswordForUsername(string username, string oldPassword, string newPassword)
        {
            int count = 0;
            var conn = DBConnection.GetDBConnection();
            var query = @"spUpdatePassword";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@oldPassword", oldPassword);
            cmd.Parameters.AddWithValue("@newPassword", newPassword);

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





        public static int InsertUser(User user)
        {
            int count = 0;

            // What comes first...a connection! Eureka!
            var conn = DBConnection.GetDBConnection();

            // What comes next is a command text
            string query = @"INSERT INTO Users " +
                           @"(FirstName, LastName, Phone, " +
                           @"EmailAddress, UserName, Password, RegionId ) " +
                           @"VALUES " +
                           @"('" + user.FirstName + "', '" + user.LastName +
                           @"', '" + user.Zip + "', '" + user.EmailAddress +
                           @"', '" + user.UserName + "', '" + user.Password + "','" + user.RegionId + ") ";

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
    }
}
