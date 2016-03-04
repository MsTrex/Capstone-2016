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
    public class JobAccessor
    {
        public static Job SelectTaskByTaskID(int taskID, Active recordType = Active.active)
        {
            Job job;
            var conn = DBConnection.GetDBConnection();
            var query = @"spSelectTasks";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@taskID", taskID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    job = new Job()
                    {
                        JobID = reader.GetInt32(0),
                        Description = reader.GetString(1),
                        Active = reader.GetBoolean(2)
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
            return job;
        }

        public static int UpdateTak(int taskID, string description, bool active)
        {
            int rowCount = 0;

            // get a connection
            var conn = DBConnection.GetDBConnection();

            // we need a command object (the command text is in the stored procedure)
            var cmd = new SqlCommand("spUpdateTasks", conn);

            // set the command type for stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // we need to manually add any input or output parameters
            cmd.Parameters.Add(new SqlParameter("JobID", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("Description", SqlDbType.VarChar, 100));
            cmd.Parameters.Add(new SqlParameter("Active", SqlDbType.Bit));

            cmd.Parameters["taskID"].Value = taskID;
            cmd.Parameters["description"].Value = description;
            cmd.Parameters["active"].Value = active;

            cmd.Parameters.Add(new SqlParameter("RowCount", SqlDbType.Int));
            cmd.Parameters["RowCount"].Direction = ParameterDirection.ReturnValue;

            try
            {
                conn.Open();
                rowCount = (int)cmd.ExecuteNonQuery();
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

        public static int InsertTak(Job task)
        {
            int rowCount = 0;

            // get a connection
            var conn = DBConnection.GetDBConnection();

            string cmdText = @"INSERT INTO Task " +
             @"(JobID, Description, Active) " +
              @"VALUES " +
                @"( '" + task.JobID + "' ,'" + task.Description + "' ,'" + task.Active + "') ";


            var cmd = new SqlCommand(cmdText, conn);



            try
            {
                conn.Open();
                rowCount = (int)cmd.ExecuteNonQuery();
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
