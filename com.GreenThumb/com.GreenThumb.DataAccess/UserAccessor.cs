/// <summary>
/// Ryan Taylor
/// Created: 2016/02/26
/// Data Access methods relating to User objects
/// </summary>
/// <remarks>
/// Updated by Ryan Taylor 2016/03/03
/// </remarks>

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
    public class UserAccessor
    {
        public static User RetrieveUserByUsername(string username)
        {
            User user;
            var conn = DBConnection.GetDBConnection();
            var query = @"Admin.spSelectUserByUsername";
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
                    int regionID = 0;
                    string zip = "";
                    if (!reader.IsDBNull(4))
                    {
                        zip = reader.GetString(4);
                    }
                    if (!reader.IsDBNull(6))
                    {
                        reader.GetInt32(6);
                    }
                    user = new User()
                    {
                        UserID = reader.GetInt32(0),
                        UserName = reader.GetString(1),
                        FirstName = reader.GetString(2),
                        LastName = reader.GetString(3),
                        Zip = zip,
                        EmailAddress = reader.GetString(5),
                        RegionId = regionID,
                        Active = reader.GetBoolean(7)
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

        public static int FindUserByUsernameAndPassword(string username, string password)
        {
            int count = 0;
            var conn = DBConnection.GetDBConnection();
            var query = @"Admin.spSelectUserWithUsernameAndPassword";

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
            var query = @"Admin.spUpdatePassword";
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

        public static List<Role> RetrieveRolesByUserID(int userID)
        {
            var roles = new List<Role>();
            var conn = DBConnection.GetDBConnection();

            var query = @"Admin.spSelectRoles";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userID", userID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(new Role()
                        {
                            RoleID = reader.GetString(0),
                            Description = reader.GetString(1),
                            Active = reader.GetBoolean(2)
                        });
                    }
                }
                else
                {
                    throw new ApplicationException("Data not found.");
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

            return roles;
        }

        ///<summary>
        ///Author: Chris Schwebach
        ///UpdateUserPersonalInfo gets a database connection and updates information 
        ///in the DB where _accessToken.UserID = UserID 
        ///Date: 3/3/16
        ///</summary>
        public static int UpdateUserPersonalInfo(int userID, string firstName, string lastName, string zip, string emailAddress, int? regionId)
        {
            int rowCount = 0;

            // get a connection
            var conn = DBConnection.GetDBConnection();

            // we need a command object (the command text is in the stored procedure)
            var cmd = new SqlCommand("Admin.spUpdateUserPersonalInfo", conn);

            // set the command type for stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", userID);
            cmd.Parameters.AddWithValue("@FirstName", firstName);
            cmd.Parameters.AddWithValue("@LastName", lastName);
            cmd.Parameters.AddWithValue("@Zip", zip);
            cmd.Parameters.AddWithValue("@EmailAddress", emailAddress);

            if (regionId == null || regionId.Equals(""))
            {
                cmd.Parameters.AddWithValue("@regionID", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@regionID", regionId);
            }

            cmd.Parameters.Add(new SqlParameter("@RowCount", SqlDbType.Int));
            cmd.Parameters["@RowCount"].Direction = ParameterDirection.ReturnValue;

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

            return rowCount;
        }

        /// <summary>
        /// Rhett Allen
        /// Created: 2016/02/26
        /// 
        /// Edits the data fields for a user object in the database
        /// </summary>
        /// <param name="updateUser">The user that includes all of the updated fields</param>
        /// <param name="originalUser">The original user object to be checked for concurrency</param>
        /// <returns>A boolean based on if the user has been updated successfully</returns>
        public static bool EditUser(User updatedUser, User originalUser)
        {
            var conn = DBConnection.GetDBConnection();
            var query = "Admin.spUpdateUser";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", updatedUser.UserID);
            cmd.Parameters.AddWithValue("@FirstName", updatedUser.FirstName);
            cmd.Parameters.AddWithValue("@LastName", updatedUser.LastName);
            cmd.Parameters.AddWithValue("@Zip", updatedUser.Zip);
            cmd.Parameters.AddWithValue("@EmailAddress", updatedUser.EmailAddress);
            cmd.Parameters.AddWithValue("@UserName", updatedUser.UserName);
            cmd.Parameters.AddWithValue("@PassWord", updatedUser.Password);
            cmd.Parameters.AddWithValue("@Active", updatedUser.Active);
            cmd.Parameters.AddWithValue("@RegionID", updatedUser.RegionId);

            cmd.Parameters.AddWithValue("@OriginalFirstName", originalUser.FirstName);
            cmd.Parameters.AddWithValue("@OriginalLastName", originalUser.LastName);
            cmd.Parameters.AddWithValue("@OriginalZip", originalUser.Zip);
            cmd.Parameters.AddWithValue("@OriginalEmailAddress", originalUser.EmailAddress);
            cmd.Parameters.AddWithValue("@OriginalUserName", originalUser.UserName);
            cmd.Parameters.AddWithValue("@OriginalPassWord", originalUser.Password);
            cmd.Parameters.AddWithValue("@OriginalActive", originalUser.Active);
            cmd.Parameters.AddWithValue("@OriginalRegionID", originalUser.RegionId);

            bool updated = false;

            try
            {
                conn.Open();

                if (cmd.ExecuteNonQuery() == 1)
                {
                    updated = true;
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

            return updated;
        }

        /// <summary>
        /// Rhett Allen
        /// Created: 2016/02/26
        /// 
        /// Get a single user based on the id in the database
        /// </summary>
        /// <param name="userID">The UserID in the database</param>
        /// <returns>The specified plant object</returns>
        public static User RetrieveUserByID(int userID)
        {
            User user = new User();

            var conn = DBConnection.GetDBConnection();
            var query = @"Admin.spSelectUser";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", userID);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    user = new User()
                    {
                        UserID = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        UserName = reader.GetString(5),
                        Password = reader.GetString(6),
                        Active = reader.GetBoolean(7)
                    };

                    // Rhett Allen 3/6/16 - changed ExecuteReader to accept null values
                    if (reader.IsDBNull(3))
                    {
                        user.Zip = null;
                    }
                    else
                    {
                        user.Zip = reader.GetString(3);
                    }

                    if (reader.IsDBNull(4))
                    {
                        user.EmailAddress = null;
                    }
                    else
                    {
                        user.EmailAddress = reader.GetString(4);
                    }

                    if (reader.IsDBNull(8))
                    {
                        user.RegionId = null;
                    }
                    else
                    {
                        user.RegionId = reader.GetInt32(8);
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
            return user;
        }

        ///<summary>
        ///Author: Chris Schwebach
        ///FetchUserPersonalInfo gets a database connection and retrieves user personal information 
        ///information in the DB where _accessToken.UserID = UserID 
        ///Date: 3/3/16
        ///</summary>
        public static List<User> FetchPersonalInfo(int userID)
        {

            var user = new List<User>();

            var conn = DBConnection.GetDBConnection();
            var query = @"Admin.spSelectUserPersonalInfo";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", userID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    User currentUser = new User()
                    {
                        FirstName = reader.GetString(0),
                        LastName = reader.GetString(1),
                        Zip = reader.GetString(2),
                        EmailAddress = reader.GetString(3),
                        RegionId = reader.GetInt32(4)

                    };
                    user.Add(currentUser);
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

        /// <summary>
        /// Author: Ibrahim Abuzaid
        /// Data Transfer Object to represent a User from the
        /// Database
        /// 
        /// Added 3/4 By Ibarahim
        /// </summary>
        public static List<User> FetchUserList(Active group = Active.active)
        {
            // create a list to hold the returned data
            var userList = new List<User>();

            // get a connection to the database
            var conn = DBConnection.GetDBConnection();

            // create a query to send through the connection

            string query = @"SELECT UserID, FirstName, LastName, " +
                           @"Zip, EmailAddress, UserName, PassWord, Active, RegionID " +
                           @"FROM Admin.Users ";

            query += @"ORDER BY LastName ";

            // create a command object
            var cmd = new SqlCommand(query, conn);

            // be safe, not sorry! use a try-catch
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
                        User currentUser = new User()
                        {
                            UserID = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Zip = reader.GetString(3),
                            EmailAddress = reader.GetString(4),
                            UserName = reader.GetString(5),
                            Password = reader.GetString(6),
                            Active = reader.GetBoolean(7)
                            //    RegionID = reader.GetInt32(8) 

                        };

                        userList.Add(currentUser);

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
            return userList;
        }

        public static int FetchUserCount(Active group = Active.active)
        {
            int count = 0;

            // let's try a scalar query

            // start with a connection object
            var conn = DBConnection.GetDBConnection();

            // write some command text
            string query = @"SELECT COUNT(*) " +
                           @"FROM Admin.Users ";

            // include our WHERE logic
            if (group == Active.active)
            {
                query += @"WHERE Active = 1 ";
            }
            else if (group == Active.inactive)
            {
                query += @"WHERE Active = 0 ";
            }

            // create a command object
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

        public static int UpdateUser(User usr)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetDBConnection();

            string query = @"UPDATE Admin.Users SET " +
                        @"FirstName = @FirstName, LastName = @LastName,  Zip = @Zip, EmailAddress = @EmailAddress, " +
                        @"PassWord = @PassWord, Active = @Active, RegionID = @RegionID " +
                        @"WHERE UserID = @UserID ";

            var cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@UserID", usr.UserID);
            cmd.Parameters.AddWithValue("@FirstName", usr.FirstName);
            cmd.Parameters.AddWithValue("@LastName", usr.LastName);
            cmd.Parameters.AddWithValue("@Zip", usr.Zip);
            cmd.Parameters.AddWithValue("@EmailAddress", usr.EmailAddress);
            cmd.Parameters.AddWithValue("@UserName", usr.UserName);
            cmd.Parameters.AddWithValue("@PassWord", usr.Password);
            cmd.Parameters.AddWithValue("@Active", usr.Active);
            cmd.Parameters.AddWithValue("@RegionID", usr.RegionId);


            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception)
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
