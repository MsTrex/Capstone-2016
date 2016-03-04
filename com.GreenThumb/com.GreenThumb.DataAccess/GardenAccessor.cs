using com.GreenThumb.BusinessObjects;
using System;
using System.Data.SqlClient;

namespace com.GreenThumb.DataAccess
{
    public class GardenAccessor
    {
        /// <summary>
        /// Function used to create Gardens by Poonam Dubey
        /// </summary>
        /// <param name="garden"></param>
        /// <returns></returns>
        public static bool CreateGarden(Garden garden)
        {
            var conn = DBConnection.GetDBConnection();
            var query = "Gardens.spInsertGardens";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupID", garden.GroupID);
            cmd.Parameters.AddWithValue("@UserID", garden.UserID);
            cmd.Parameters.AddWithValue("@GardenDescription", garden.GardenDescription);
            cmd.Parameters.AddWithValue("@GardenRegion", garden.GardenRegion);
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
    }
}
