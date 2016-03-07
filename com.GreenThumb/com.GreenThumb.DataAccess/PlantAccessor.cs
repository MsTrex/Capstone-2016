using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.DataAccess
{
    ///<summary>
    /// Rhett Allen
    /// Created: 2016/02/26
    /// 
    ///Calls stored procedures related to Plants in the database
    ///</summary>
    public class PlantAccessor
    {
        /// <summary>
        /// Rhett Allen
        /// Created: 2016/02/26
        /// 
        /// Get a list of all plants from the database
        /// </summary>
        /// <param name="active">Determines whether to pull active plants</param>
        /// <returns>List of plant objects</returns>
        public static List<Plant> RetrievePlantList(Active active)
        {
            List<Plant> plants = new List<Plant>();

            var conn = DBConnection.GetDBConnection();
            var query = @"Expert.spSelectPlants";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            if (active == Active.inactive)
            {
                cmd.Parameters.AddWithValue("@Active", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Active", 1);
            }

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Plant plant = new Plant()
                        {
                            PlantID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Type = reader.GetString(2),
                            Category = reader.GetString(3),
                            Description = reader.GetString(4),
                            Season = reader.GetString(5),
                            CreatedBy = reader.GetInt32(6),
                            CreatedDate = reader.GetDateTime(7),
                            ModifiedBy = reader.GetInt32(8),
                            ModifiedDate = reader.GetDateTime(9),
                            Active = reader.GetBoolean(10)
                        };

                        plants.Add(plant);
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
            return plants;
        }

        /// <summary>
        /// Rhett Allen
        /// Created: 2016/02/26
        /// 
        /// Get a single plant based on the id in the database
        /// </summary>
        /// <param name="plantID">The PlantID in the database</param>
        /// <returns>The specified plant object</returns>
        public static Plant RetrievePlant(int plantID)
        {
            Plant plant = new Plant();

            var conn = DBConnection.GetDBConnection();
            var query = @"Expert.spSelectPlant";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PlantID", plantID);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    plant = new Plant()
                    {
                        PlantID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Type = reader.GetString(2),
                        Category = reader.GetString(3),
                        Description = reader.GetString(4),
                        Season = reader.GetString(5),
                        CreatedBy = reader.GetInt32(6),
                        CreatedDate = reader.GetDateTime(7),
                        ModifiedBy = reader.GetInt32(8),
                        ModifiedDate = reader.GetDateTime(9),
                        Active = reader.GetBoolean(10)
                    };
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
            return plant;
        }

        /// <summary>
        /// Rhett Allen
        /// Created: 2016/02/26
        /// 
        /// Edits the data fields for a plant object in the database
        /// </summary>
        /// <param name="plant">The plant that includes all of the updated fields</param>
        /// <param name="originalPlant">The original plant object to be checked for concurrency</param>
        /// <returns>A boolean based on if the plant has been updated successfully</returns>
        public static bool EditPlant(Plant plant, Plant originalPlant)
        {
            var conn = DBConnection.GetDBConnection();
            var query = "Expert.spUpdatePlant";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PlantID", plant.PlantID);
            cmd.Parameters.AddWithValue("@Name", plant.Name);
            cmd.Parameters.AddWithValue("@Type", plant.Type);
            cmd.Parameters.AddWithValue("@Category", plant.Category);
            cmd.Parameters.AddWithValue("@Description", plant.Description);
            cmd.Parameters.AddWithValue("@Season", plant.Season);
            cmd.Parameters.AddWithValue("@CreatedBy", plant.CreatedBy);
            cmd.Parameters.AddWithValue("@CreatedDate", plant.CreatedDate);
            cmd.Parameters.AddWithValue("@ModifiedBy", plant.ModifiedBy);
            cmd.Parameters.AddWithValue("@ModifiedDate", plant.ModifiedDate);
            cmd.Parameters.AddWithValue("@Active", plant.Active);

            cmd.Parameters.AddWithValue("@OriginalName", originalPlant.Name);
            cmd.Parameters.AddWithValue("@OriginalType", originalPlant.Type);
            cmd.Parameters.AddWithValue("@OriginalCategory", originalPlant.Category);
            cmd.Parameters.AddWithValue("@OriginalDescription", originalPlant.Description);
            cmd.Parameters.AddWithValue("@OriginalSeason", originalPlant.Season);
            cmd.Parameters.AddWithValue("@OriginalCreatedBy", originalPlant.CreatedBy);
            cmd.Parameters.AddWithValue("@OriginalCreatedDate", originalPlant.CreatedDate);
            cmd.Parameters.AddWithValue("@OriginalModifiedBy", originalPlant.ModifiedBy);
            cmd.Parameters.AddWithValue("@OriginalModifiedDate", originalPlant.ModifiedDate);
            cmd.Parameters.AddWithValue("@OriginalActive", originalPlant.Active);

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
        /// Inserts a plant object into the database
        /// </summary>
        /// <param name="plant">The plant to be created</param>
        /// <returns>A boolean based on if the plant has been created successfully</returns>
        public static bool CreatePlant(Plant plant)
        {
            var conn = DBConnection.GetDBConnection();
            var query = "Expert.spInsertPlants";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", plant.Name);
            cmd.Parameters.AddWithValue("@Type", plant.Type);
            cmd.Parameters.AddWithValue("@Category", plant.Category);
            cmd.Parameters.AddWithValue("@Description", plant.Description);
            cmd.Parameters.AddWithValue("@Season", plant.Season);
            cmd.Parameters.AddWithValue("@CreatedBy", plant.CreatedBy);
            cmd.Parameters.AddWithValue("@CreatedDate", plant.CreatedDate);
            cmd.Parameters.AddWithValue("@ModifiedBy", plant.ModifiedBy);
            cmd.Parameters.AddWithValue("@ModifiedDate", plant.ModifiedDate);

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