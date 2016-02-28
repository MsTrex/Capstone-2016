// Added By Poonam Dubey on 02/27/2016
//Update file in project

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class Task
    {
        public int TaskID { get; set; }
        public string TaskDescription { get; set; }
        public bool TaskActive { get; set; }
        //public DateTime TaskLastRevision { get; set; }

        public Task() { }
        public Task(int taskID,
                     string taskDescription,
                     bool taskActive)
        {
            TaskID = taskID;
            TaskDescription = taskDescription;
            TaskActive = taskActive;
        }
    }
}
