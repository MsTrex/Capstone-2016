using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessLogic
{
    public class JobManager
    {
        public bool AddNewTask(int taskID, string description, bool active)
        {
            var job = new Job()
            {
                JobID = taskID,
                Description = description,
                Active = active
            };

            try
            {
                if (JobAccessor.InsertTak(job) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

       
    }
}
