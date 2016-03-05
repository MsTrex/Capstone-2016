//Update file in project

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class Job
    {
        public int JobID { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        //public DateTime TaskLastRevision { get; set; }

        public Job() { }
        public Job(int jobID,
                     string taskDescription,
                     bool taskActive)
        {
            JobID = jobID;
            Description = taskDescription;
            Active = taskActive;
        }
    }
}
