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
    /// Added by Sara Nanke on 03/04/2016
    /// This class contains the SQL for the message object.
    /// </summary>
    public class MessageAccessor
    {
        /// <summary>
        /// Added by Sara Nanke on 03/04/2016
        /// This method contains the SQL for retrieving a message from the database.
        /// </summary>
        public static List<Message> fetchAdminMessages()
        {
            var messages = new List<Message>();
            var conn = DBConnection.GetDBConnection();
            var query = @"Admin.spDisplayMessages";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    Message message = new Message()
                    {
                        //MessageID, MessageContent, MessageDate, Subject, MessageSender, Active
                        MessageID = reader.GetInt32(0),
                        MessageContent = reader.GetString(1),
                        MessageDate = reader.GetDateTime(2),
                        MessageSubject = reader.GetString(3),
                        MessageSender = reader.GetInt32(4),
                        Active = reader.GetBoolean(5)
                    };

                    messages.Add(message);
                }
                else
                {
                    throw new ApplicationException("Data not found");
                }
            }
            catch (Exception)
            {
                //there are no admin messages, do nothing
            }
            finally
            {
                conn.Close();
            }

            return messages;
        }
    }
}
