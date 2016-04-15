using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessLogic.Interfaces;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.BusinessLogic.FakeStuff
{
    /// <summary>
    /// Ryan Taylor
    /// Cerated: 4/14/2016
    /// 
    /// Fake class to test MessageManager Interface
    /// </summary>
    public class FakeMessageManager : IMessageManager
    {

        public List<BusinessObjects.Message> GetUserMessages(string Username)
        {
            List<Message> msg = new List<Message>();
            if (Username == "jeffb")
            {
                msg.Add(new Message()
                {
                    MessageID = 1000
                });
            }
            return msg;
        }

        public bool MarkMessageDeletedReceiver(string Username, int MessageID)
        {
            bool result = false;

            if (Username == "jeffb" && MessageID == 1000)
            {
                result = true;
            }

            return result;
        }

        public bool MarkMessageDeletedSender(string Username, int MessageID)
        {
            bool result = false;

            if (Username == "jeffb" && MessageID == 1000)
            {
                result = true;
            }

            return result;
        }

        public bool MarkMessageRead(string Username, int MessageID)
        {
            bool result = false;

            if (Username == "jeffb" && MessageID == 1000)
            {
                result = true;
            }

            return result;
        }

        public bool SendMessage(string MessageContent, string Subject, string SenderUsername, string ReceiverUsername)
        {
            bool result = false;

            if (MessageContent == "content" && Subject == "subject" && SenderUsername == "jeffb" && ReceiverUsername == "guy")
            {
                result = true;
            }

            return result;
        }
    }
}
