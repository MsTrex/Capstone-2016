using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class Task
    {
        /// <summary>
        /// Author: Poonam
        /// Data Transfer Object to represent a Task from the
        /// Database
        /// 
        /// Added 3/3 By Trevor Glisch
        /// </summary>
        public int TaskID { get; set; }
        public string TaskDescription { get; set; }
        public bool Active { get; set; }
        //public DateTime TaskLastRevision { get; set; }

        public Task() { }
        public Task(int taskID,
                     string taskDescription,
                     bool active)
        {
            TaskID = taskID;
            TaskDescription = taskDescription;
            Active = active;
        }
    }
}
