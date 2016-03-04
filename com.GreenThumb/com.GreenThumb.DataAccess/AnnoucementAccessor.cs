using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;


//Dat Tran
namespace com.GreenThumb.DataAccess
{
    public class AnnoucementsAccessor
    {
        public static int UpdateAnnoucements(int AnnouncementID, string Announcements)
        {
            int rowCount = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sqUpdateAnnouncement", conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("AnnouncementID", SqlDbType.Int));

            cmd.Parameters.Add(new SqlParameter("Announcements", SqlDbType.VarChar, 250));

            cmd.Parameters["AnnouncementID"].Value = AnnouncementID;
            cmd.Parameters["Announcements"].Value = Announcements;

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
        public static int InsertAnnouncement(int AnnouncementID, string Announcements)
        {
            int rowCount = 0;

            // get a connection
            var conn = DBConnection.GetDBConnection();

            //create a sql command
            var cmd = new SqlCommand("spInsertAnnouncements", conn);

            // set Command Type
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("AnnouncementID", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("Announcements", SqlDbType.VarChar, 250));

            cmd.Parameters["AnnouncementID"].Value = AnnouncementID;
            cmd.Parameters["Announcements"].Value = Announcements;


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
    }
}
