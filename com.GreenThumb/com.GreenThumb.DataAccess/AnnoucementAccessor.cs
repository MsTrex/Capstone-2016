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
        /// <summary>
        ///These comments were added byTRex 4/12/16 
        ///Changes to method by TRex 4/12/16
        ///This method allows an announcement to be updated
        /// </summary>
        /// <param name="AnnouncementID"></param>
        /// <param name="DateUpdated"></param>
        /// <param name="Announcement"></param>
        /// <param name="UserID"></param>
        /// <returns> rowCount </returns>

        public static int UpdateAnnoucements(int AnnouncementID, DateTime DateUpdated, string Announcement, int UserID)
        {
            int rowCount = 0;

            ///changes begin so that an existing stored procedure can be used.-TRex
            var conn = DBConnection.GetDBConnection();
            var query = @"Gardens.spUpdateAnnouncements";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@AnnouncementID", AnnouncementID);
            cmd.Parameters.AddWithValue("@DateUpdated", DateUpdated);
            cmd.Parameters.AddWithValue("@Announcement", Announcement);
            cmd.Parameters.AddWithValue("@UserID", UserID);


            try
            {
                conn.Open();
                rowCount = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            // end of changes-TRex

            //cmd.Parameters.Add(new SqlParameter("AnnouncementID", SqlDbType.Int));

            //cmd.Parameters.Add(new SqlParameter("Announcements", SqlDbType.VarChar, 250));

            //cmd.Parameters["AnnouncementID"].Value = AnnouncementID;
            //cmd.Parameters["Announcements"].Value = Announcements;

            //cmd.Parameters.Add(new SqlParameter("RowCount", SqlDbType.Int));
            //cmd.Parameters["RowCount"].Direction = ParameterDirection.ReturnValue;

            //try
            //{
            //    conn.Open();
            //    rowCount = (int)cmd.ExecuteNonQuery();
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            //finally
            //{
            //    conn.Close();
            //}

            return rowCount;
        }

        /// <summary>
        /// Comments by TRex 4/12/16
        /// Changes to this method by TRex 4/12/16
        /// </summary>
        /// <param name="announcement"></param>
        /// <returns></returns>
        public static int InsertAnnouncement(Announcements announcement)
        {
            int rowCount = 0;

            //changes begin so that database will update-TRex
            var conn = DBConnection.GetDBConnection();
            var query = @"Expert.spInsertBlogEntry";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", announcement.UserID);
            cmd.Parameters.AddWithValue("@Date", announcement.Date);
            cmd.Parameters.AddWithValue("@OrganizationID", announcement.OrganizationID);
            cmd.Parameters.AddWithValue("@Announcement", announcement.Announcement);

            try
            {
                conn.Open();
                rowCount = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            // End of changes-TRex

            //// get a connection
            //var conn = DBConnection.GetDBConnection();

            ////create a sql command
            //var cmd = new SqlCommand("spInsertAnnouncements", conn);

            //// set Command Type
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add(new SqlParameter("AnnouncementID", SqlDbType.Int));
            //cmd.Parameters.Add(new SqlParameter("Announcements", SqlDbType.VarChar, 250));

            //cmd.Parameters["AnnouncementID"].Value = AnnouncementID;
            //cmd.Parameters["Announcements"].Value = Announcements;


            //cmd.Parameters.Add(new SqlParameter("RowCount", SqlDbType.Int));
            //cmd.Parameters["RowCount"].Direction = ParameterDirection.ReturnValue;

            //try
            //{
            //    conn.Open();
            //    rowCount = (int)cmd.ExecuteNonQuery();
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            //finally
            //{
            //    conn.Close();
            //}
            return rowCount;
        }
    }
}
