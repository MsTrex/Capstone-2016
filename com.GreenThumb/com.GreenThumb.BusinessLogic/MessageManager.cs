using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessLogic
{
    /// <summary>
    /// Added by Sara Nanke on 03/04/2016
    /// This Class manages message objects to send to the view
    /// </summary>
    public class MessageManager
    {
        /// <summary>
        /// Added by Sara Nanke on 03/04/2016
        /// This method retrieves messages
        /// </summary>
        public List<Message> GetUserMessages()
        {
            List<Message> messages = new List<Message>();
            messages = MessageAccessor.RetrieveAdminMessages();

            return messages;
        }
    }
}
