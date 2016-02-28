//Added by Poonam Dubey on 02/27/2016

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class WorkLog
    {
        public int WorkLogID { get; set; }
        public int UserID { get; set; }
        public int TaskID { get; set; }
        public DateTime TimeBegin { get; set; }
        public DateTime? Timefinished { get; set; }

        public WorkLog() { }
        public WorkLog(int workLogID,
                        int userID,
                        int taskID,
                        DateTime timeBegin,
                        DateTime timeFinished)
        {
            WorkLogID = workLogID;
            UserID = userID;
            TaskID = taskID;
            TimeBegin = timeBegin;
            Timefinished = timeFinished;
        }
    }
}
