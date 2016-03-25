using com.GreenThumb.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.DataAccess;

namespace com.GreenThumb.DataAccess
{
    /// <summary>
    /// Retrieve, select and update a task for a garden
    /// Created By: Nasr Mohammed 3/4/2016 
    /// Modified on: 3/15/2016
    /// </summary>
    public class JobAccessor
    {
        /// <summary>
        /// Retrieve a list of tasks.
        /// Created By: Nasr Mohammed 3/4/2016 Modified 3/15/2016
        /// </summary>
        /// <returns>A list of tasks.</returns>
        public static List<Job> FetchTasks()
        {

            var jobs = new List<Job>();
            var conn = DBConnection.GetDBConnection();
            // var query = @"spSelectTasks";
            string query = @"SELECT TaskID, GardenID,  Description , DateAssigned, DateCompleted, AssignedTo, AssignedFrom, UserNotes, Active " +
                         @"FROM Gardens.Tasks ";

            var cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var job = new Job();

                        job.JobID = reader.GetInt32(0);
                        job.GardenID = reader.GetInt32(1);
                        job.Description = reader.GetString(2);
                        job.DateAssigned = reader.GetDateTime(3);
                        job.DateCompleted = reader.GetDateTime(4);
                        job.AssignedTo = reader.GetInt32(5);
                        job.AssignedFrom = reader.GetInt32(6);
                        job.UserNotes = reader.GetString(7);
                        job.Active = reader.GetBoolean(8);

                        jobs.Add(job);
                    }
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
            return jobs;
        }

        /// <summary>
        /// Update a task in a garden.
        /// Created By: Nasr Mohammed 3/4/2016 
        /// Modified on: 3/15/2016
        /// </summary>
        /// <param name="job">The task field that should be updated </param>
        /// <param name="originalJob">The orignial taks field</param>
        /// <returns>A boolean if the task updated successfully</returns>
        public static bool UpdateTask(Job job, Job originalJob)
        {

            var conn = DBConnection.GetDBConnection();
            var query = "Gardens.spUpdateTasks";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TaskID", job.JobID);
            cmd.Parameters.AddWithValue("@Description", job.Description);
            cmd.Parameters.AddWithValue("@dateAssigned", job.DateAssigned);
            cmd.Parameters.AddWithValue("@dateCompleted", job.DateCompleted);
            cmd.Parameters.AddWithValue("@assignedTo", job.AssignedTo);
            cmd.Parameters.AddWithValue("@assignedFrom", job.AssignedFrom);
            cmd.Parameters.AddWithValue("@userNotes", job.UserNotes);
            cmd.Parameters.AddWithValue("@active", job.Active);

            //cmd.Parameters.AddWithValue("@OriginalgardenID", originalJob.GardenID);
            //cmd.Parameters.AddWithValue("@OriginalDescription", originalJob.Description);
            //cmd.Parameters.AddWithValue("@OriginalActive", originalJob.Active);

            bool flag = false;

            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() != 0)
                {
                    Console.WriteLine("Accessor works");
                    flag = true;
                }
                else {
                    Console.WriteLine("Accessor broken");
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
            return flag;
        }

        /// <summary>
        /// Insert a task in a garden.
        /// Created By: Nasr Mohammed 3/4/2016 
        /// Modified on: 3/15/2016
        /// </summary>
        /// <param name="job">The task that should be created </param>
        /// <returns>A rowsAffected if it's inserted successfully</returns>
        public static int CreateTask(Job job)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"Gardens.spInsertTasks";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@gardenID", job.GardenID);
            cmd.Parameters.AddWithValue("@description", job.Description);
            cmd.Parameters.AddWithValue("@dateAssigned", job.DateAssigned);
            cmd.Parameters.AddWithValue("@dateCompleted", job.DateCompleted);
            cmd.Parameters.AddWithValue("@assignedTo", job.AssignedTo);
            cmd.Parameters.AddWithValue("@assignedFrom", job.AssignedFrom);
            cmd.Parameters.AddWithValue("@userNotes", job.UserNotes);

            try
            {
                conn.Open();
                rowsAffected = (int)cmd.ExecuteNonQuery();
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

        /// <summary>
        /// Select a task based on task ID.
        /// Created By: Nasr Mohammed 3/4/2016 Modified 3/15/2016
        /// </summary>
        /// <param name="job">The taskID should be passed to retrive a task </param>
        /// <returns>specific task.</returns>
        public static Job RetrieveJob(int jobId)
        {
            Job job = new Job();

            var conn = DBConnection.GetDBConnection();
            var query = @"SELECT TaskID, GardenID, Description , DateAssigned, DateCompleted, AssignedTo, AssignedFrom, UserNotes, Active " +
                         @"FROM Gardens.Tasks ";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@JobID", jobId);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    job = new Job()
                    {
                        JobID = reader.GetInt32(0),
                        GardenID = reader.GetInt32(1),
                        Description = reader.GetString(2),
                        DateAssigned = reader.GetDateTime(3),
                        DateCompleted = reader.GetDateTime(4),
                        AssignedTo = reader.GetInt32(5),
                        AssignedFrom = reader.GetInt32(6),
                        UserNotes = reader.GetString(7),
                        Active = reader.GetBoolean(8)
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
            return job;
        }

        public static List<Job> RetrieveJobByUserId(int userId)
        {
            var jobs = new List<Job>();
            var conn = DBConnection.GetDBConnection();
            // need to send Chris stored procedure
            var query = @"SELECT TaskID, GardenID, Description , DateAssigned, DateCompleted, AssignedTo, AssignedFrom, UserNotes, Active " +
                         @"FROM Gardens.Tasks " +
                         @"WHERE AssignedTo=" + userId + "AND Active=1";
            var cmd = new SqlCommand(query, conn);

            //cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var job = new Job();

                        job.JobID = reader.GetInt32(0);
                        job.GardenID = reader.GetInt32(1);
                        job.Description = reader.GetString(2);
                        job.DateAssigned = reader.GetDateTime(3);
                        job.DateCompleted = reader.GetDateTime(4);
                        job.AssignedTo = reader.GetInt32(5);
                        job.AssignedFrom = reader.GetInt32(6);
                        job.UserNotes = reader.GetString(7);
                        job.Active = reader.GetBoolean(8);

                        jobs.Add(job);
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
            return jobs;
        }

    }
}
