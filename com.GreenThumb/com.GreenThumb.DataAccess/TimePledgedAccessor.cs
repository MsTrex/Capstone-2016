using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using com.GreenThumb.BusinessObjects;
using System.Data;

namespace com.GreenThumb.DataAccess
{
    //<summary>
    //Emily West
    //Access time pledged stored procedures
    //</summary>

    class TimePledgedAccessor
    {
    //    public static int PledgeVolunteerHours(TimePledgeDonated timeDonated, int UserID)
    //    {
    //        int rowCount = 0;
    //        var conn = DBConnection.GetDBConnection();
    //        var query = "Donations.spInsertTimePledge";
    //        var cmd = new SqlCommand(query, conn);

    //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@UserID", timeDonated.UserID);
    //        cmd.Parameters.AddWithValue("@StartTime", timeDonated.StartTime);
    //        cmd.Parameters.AddWithValue("@FinishTime", timeDonated.FinishTime);
    //        cmd.Parameters.AddWithValue("@Affiliation", timeDonated.GardenAffiliation);
    //        cmd.Parameters.AddWithValue("@Location", timeDonated.Location);
    //        cmd.Parameters.AddWithValue("@Date",timeDonated.DateCreated);
    //        cmd.Parameters.Add(new SqlParameter("RowCount", SqlDbType.Int));
    //        cmd.Parameters["RowCount"].Direction = ParameterDirection.ReturnValue;

    //        try
    //        {
    //            conn.Open();
    //            rowCount = (int)cmd.ExecuteNonQuery();
    //        }
    //        catch (Exception)
    //        {
    //            throw new ApplicationException("Invalid Selection!");
    //        }
    //        finally
    //        {
    //            conn.Close();
    //        }

    //        return rowCount;
    //    }
    }
}
