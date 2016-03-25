using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;
using System.Data.SqlClient;
using System.Data;

namespace com.GreenThumb.DataAccess
{
    /// <summary>
    /// Accessor for an administrator to modify requests and promote users / demote experts.
    /// 
    /// Created By: Trent Cullinan 03/15/2016
    /// </summary>
    public class AdminExpertRequestsAccessor
    {
        private const string ADMIN = "Admin";
        private const string ROLE = "Expert";
        private AccessToken accessToken = null;

        /// <summary>
        /// Only constructor to verify that client instaniating this object is an administrator.
        /// 
        /// Created By: Trent Cullinan 03/15/2016
        /// </summary>
        /// <param name="accessToken">To confirm access as administrator.</param>
        public AdminExpertRequestsAccessor(AccessToken accessToken)
        {
            if (CheckAdminRoleStatus(accessToken))
            {
                this.accessToken = accessToken;
            }
            else
            {
                throw new Exception("User must be an admin to access this method.");
            }
        }

        /// <summary>
        /// Retrieve ExpertRequests to be processed by an administrator.
        /// 
        /// Created By: Trent Cullinan 03/15/2016
        /// </summary>
        /// <param name="accessToken">To confirm access as administrator.</param>
        /// <returns>Collection of ExpertRequests that need processing.</returns>
        public IEnumerable<ExpertRequest> RetrieveExpertRequests(AccessToken accessToken)
        {
            IList<ExpertRequest> requests = null;

            if (CheckAdminRoleStatus(accessToken))
            {
                requests = new List<ExpertRequest>();

                var conn = DBConnection.GetDBConnection();

                var cmd = new SqlCommand("Admin.spSelectExpertRequests", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Active", true);

                try
                {
                    conn.Open();

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        requests.Add(new ExpertRequest()
                        {
                            RequestID
                                = reader.GetInt32(0),
                            User
                                = new User()
                                {
                                    UserID
                                        = reader.GetInt32(1),
                                    UserName
                                        = reader.GetString(2),
                                    FirstName
                                        = reader.GetString(3),
                                    LastName
                                        = reader.GetString(4),
                                    EmailAddress
                                        = reader.GetString(5)
                                },
                            RequestDate
                                = reader.GetDateTime(6),
                            RequestTitle
                                = reader.GetString(7),
                            RequestContent
                                = reader.GetString(8)
                        });
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
            }
            else
            {
                throw new Exception("User must be an admin to access this method.");
            }

            return requests;
        }

        /// <summary>
        /// Retrieve all Users who are not an active expert.
        /// 
        /// Created By: Trent Cullinan 03/15/2016
        /// </summary>
        /// <param name="accessToken">To confirm access as administrator.</param>
        /// <returns>Collection of Users who are not experts.</returns>
        public IEnumerable<User> RetrieveAllUsers(AccessToken accessToken)
        {
            IList<User> users = null;

            if (CheckAdminRoleStatus(accessToken))
            {
                users = new List<User>();

                var conn = DBConnection.GetDBConnection();

                var cmd = new SqlCommand("Admin.spSelectUsersExcludingRole", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RoleID", ROLE);
                cmd.Parameters.AddWithValue("@Active", true);

                try
                {
                    conn.Open();

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        users.Add(new User()
                        {
                            UserID
                                = reader.GetInt32(0),
                            FirstName
                                = reader.GetString(1),
                            LastName
                                = reader.GetString(2),
                            EmailAddress
                                = reader.GetString(4),
                            UserName
                                = reader.GetString(5)
                        });
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
            }
            else
            {
                throw new Exception("User must be an admin to access this method.");
            }

            return users;
        }

        /// <summary>
        /// Retrieve users who are an active expert.
        /// 
        /// Created By: Trent Cullinan 03/15/2016
        /// </summary>
        /// <param name="accessToken">To confirm access as administrator.</param>
        /// <returns>Collection of Users that are experts.</returns>
        public IEnumerable<User> RetrieveAllExperts(AccessToken accessToken)
        {
            IList<User> experts = null;

            if (CheckAdminRoleStatus(accessToken))
            {
                experts = new List<User>();

                var conn = DBConnection.GetDBConnection();

                var cmd = new SqlCommand("Admin.spSelectUsersByRole", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Active", true);
                cmd.Parameters.AddWithValue("@RoleID", ROLE);

                try
                {
                    conn.Open();

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        experts.Add(new User()
                        {
                            UserID
                                = reader.GetInt32(0),
                            FirstName
                                = reader.GetString(1),
                            LastName
                                = reader.GetString(2),
                            EmailAddress
                                = reader.GetString(3),
                            UserName
                                = reader.GetString(4)
                        });
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
            }
            else
            {
                throw new Exception("User must be an admin to access this method.");
            }

            return experts;
        }

        /// <summary>
        /// Approve the expert request and set user contained in request as an expert.
        /// 
        /// Created By: Trent Cullinan 03/15/2016
        /// </summary>
        /// <param name="request">To confirm access as administrator.</param>
        /// <returns>Rows affected by action.</returns>
        public int ApproveRequest(ExpertRequest request)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Admin.spUpdateExpertRequest", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SetRequestParams(cmd, request, approved: true);

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
        /// Decline the expert request and do nothing with the user contained in the request.
        /// 
        /// Created By: Trent Cullinan 03/15/2016
        /// </summary>
        /// <param name="request">ExpertRequest to be declined.</param>
        /// <returns>Rows affected by action.</returns>
        public int DeclineRequest(ExpertRequest request)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Admin.spUpdateExpertRequest", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SetRequestParams(cmd, request);

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
        /// Promote a User to an expert.
        /// 
        /// Created By: Trent Cullinan 03/15/2016
        /// </summary>
        /// <param name="user">User that is not an expert to be promoted.</param>
        /// <returns>Rows affected by action.</returns>
        public int PromoteUser(User user)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Admin.spUpdateUsersUserRole", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SetExpertChangeParams(cmd, user);

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
        /// Demote a User from expert status.
        /// 
        /// Created By: Trent Cullinan 03/15/2016
        /// </summary>
        /// <param name="user">User that is an expert to be demoted.</param>
        /// <returns>Rows affected by action.</returns>
        public int DemoteExpert(User user)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Admin.spUpdateUsersUserRole", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SetExpertChangeParams(cmd, user, active: false);

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

        // Created By: Trent Cullinan 03/15/2016
        private bool CheckAdminRoleStatus(AccessToken accessToken)
        {
            return 0 < accessToken.Roles.Where(r => r.RoleID.Equals(ADMIN)).Count();
        }

        // Created By: Trent Cullinan 03/15/2016
        private void SetRequestParams(SqlCommand cmd, ExpertRequest request, bool approved = false)
        {
            cmd.Parameters.AddWithValue("@RequestID",
                request.RequestID);
            cmd.Parameters.AddWithValue("@UserID",
                request.User.UserID);
            cmd.Parameters.AddWithValue("@ModifedBy",
                accessToken.UserID);
            cmd.Parameters.AddWithValue("@Approved",
                approved);
        }

        // Created By: Trent Cullinan 03/15/2016
        private void SetExpertChangeParams(SqlCommand cmd, User user, bool active = true)
        {
            cmd.Parameters.AddWithValue("@UserID",
                user.UserID);
            cmd.Parameters.AddWithValue("@RoleID",
                ROLE);
            cmd.Parameters.AddWithValue("@CreatedBy",
                accessToken.UserID);
            cmd.Parameters.AddWithValue("@Active",
                active);
        }
    }
}
