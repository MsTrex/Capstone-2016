using com.GreenThumb.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace com.GreenThumb.DataAccess
{
    /// <summary>
    /// Garden Accessor by Poonam Dubey
    /// </summary>
    public class GardenAccessor
    {
        /// <summary>
        /// Function used to create Gardens
        /// Author: Poonam Dubey
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


        /// <summary>
        /// Accessor function fetch all gardens : Poonam Dubey  (20th March 2016)
        /// </summary> 
        /// <returns></returns>
        public static List<Garden> GetGardens()
        {
            // create a list to hold the returned data
            var gardenList = new List<Garden>();

            // get a connection to the database
            var conn = DBConnection.GetDBConnection();

            // create a query to send through the connection
            string query = "sp_GetGardens";

            // create a command object - SP
            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // try-catch
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
                        Garden garden = new Garden()
                        {
                            //CustomerID = reader.GetInt32(0),
                            //FirstName = reader.GetString(1),
                            //LastName = reader.GetString(2),
                            //EmailID = reader.GetString(3),
                            //PhoneNo1 = reader.GetString(4),
                            //Address1 = reader.GetString(5),
                            //PostalCode = reader.GetString(6),
                            //City = reader.GetString(7),
                            //State = reader.GetString(8)
                        };


                        gardenList.Add(garden);
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
            return gardenList;
        }
    }
}
