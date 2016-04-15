using System;
namespace com.GreenThumb.BusinessLogic.Interfaces
{
    /// <summary>
    /// Ryan Taylor
    /// Created: 4/14/2016
    /// </summary>
    public interface IMessageManager
    {
        System.Collections.Generic.List<com.GreenThumb.BusinessObjects.Message> GetUserMessages(string Username);
        bool MarkMessageDeletedReceiver(string Username, int MessageID);
        bool MarkMessageDeletedSender(string Username, int MessageID);
        bool MarkMessageRead(string Username, int MessageID);
        bool SendMessage(string MessageContent, string Subject, string SenderUsername, string ReceiverUsername);
    }
}
