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
    /// 
    /// Created By: Trent Cullinan 04/14/16
    /// </summary>
    public class UserNeedsAccessor
    {
        private int userId;

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <param name="userId"></param>
        public UserNeedsAccessor(int userId)
        {
            this.userId = userId;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <returns></returns>
        public IEnumerable<NeedContribution> RetrieveSentContributions()
        {
            List<NeedContribution> contributions = new List<NeedContribution>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Needs.spSelectUsersMetNeeds", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID",
                this.userId);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    contributions.Add(new NeedContribution()
                    {
                        NeedContributionID
                            = reader.GetInt32(0),
                        Need = new GardenNeed()
                        {
                            GardenNeedId = reader.GetInt32(1)
                        },
                        Description
                            = reader.GetString(2),
                        DateCreated
                            = reader.GetDateTime(3),
                        DateModified
                            = reader.IsDBNull(4) ? DateTime.MinValue :
                                reader.GetDateTime(4)
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

            return contributions;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <returns></returns>
        public IEnumerable<NeedContribution> RetrieveAcceptedContributions()
        {
            List<NeedContribution> contributions = new List<NeedContribution>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Needs.spSelectUsersMetNeeds", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    contributions.Add(new NeedContribution()
                    {
                        NeedContributionID
                            = reader.GetInt32(0),
                        Need = new GardenNeed()
                        {
                            GardenNeedId = reader.GetInt32(1)
                        },
                        Description
                            = reader.GetString(2),
                        DateCreated
                            = reader.GetDateTime(3),
                        DateModified
                            = reader.IsDBNull(4) ? DateTime.MinValue :
                                reader.GetDateTime(4)
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

            return contributions;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <returns></returns>
        public IEnumerable<NeedContribution> RetrieveDeclinedContributions()
        {
            List<NeedContribution> contributions = new List<NeedContribution>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Needs.spSelectUsersMetNeeds", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID",
                this.userId);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    contributions.Add(new NeedContribution()
                    {
                        NeedContributionID
                            = reader.GetInt32(0),
                        Need = new GardenNeed()
                        {
                            GardenNeedId
                                = reader.GetInt32(1)
                        },
                        Description
                            = reader.GetString(2),
                        DateCreated
                            = reader.GetDateTime(3),
                        DateModified
                            = reader.IsDBNull(4) ? DateTime.MinValue :
                                reader.GetDateTime(4)
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

            return contributions;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GardenNeed> RetrieveAvailableNeeds()
        {
            List<GardenNeed> needs = new List<GardenNeed>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    needs.Add(new GardenNeed()
                    {

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

            return needs;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <param name="needContribution"></param>
        /// <returns></returns>
        public int SendContribution(NeedContribution needContribution)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Needs.spInsertContributions", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NeedID",
                needContribution.Need.GardenNeedId);
            cmd.Parameters.AddWithValue("@Description",
                needContribution.Description);
            cmd.Parameters.AddWithValue("@UserID",
                this.userId);

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
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <param name="needContributionId"></param>
        /// <returns></returns>
        public int CancelPendingContribution(int needContributionId)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Needs.spUpdateCancelContribution", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(@"@ContributionID",
                needContributionId);

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
