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
    /// Retrieve, select and update a task for a garden
    /// Created By: Nasr Mohammed 3/4/2016 
    /// Modified on: 3/15/2016
    /// </summary>
    public class JobManager
    {

        public bool AddNewTask(Job job)
        {

            try
            {
                if (JobAccessor.CreateTask(job) == 1)
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

        public List<Job> GetTaskList()
        {

            try
            {
                return JobAccessor.FetchTasks();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(" No records Found!.", ex);
            }

        }

        public bool ChangeTask(Job job, Job oldJob)
        {

            try
            {
                bool myJob = JobAccessor.UpdateTask(job, oldJob);
                Console.WriteLine(myJob);
                return myJob;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Job FetchJob(int jobId)
        {
            return JobAccessor.RetrieveJob(jobId);
        }

        public List<Job> RetrieveJobByUserId(int userId)
        {
            try
            {                
                return JobAccessor.RetrieveJobByUserId(userId);
            }
            catch (Exception ex)
            {
                
                throw new ApplicationException(" No records Found!", ex);
            }
        }

        public List<Job> RetrieveJobByGardenId(int gardenId) 
        {
            try
            {
                return JobAccessor.RetrieveJobByGardenId(gardenId);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("No records found!", ex);
            }
        }

        public List<int> RetrieveGardenIdByUserId(int userId) 
        {
            try
            {
                return JobAccessor.RetrieveGardenIdByUserId(userId);
            }
            catch (Exception ex)
            {
                
                throw new ApplicationException("No records found!", ex);
            }
        }

        /// <summary>
        /// This method adds a test user to validate the functionality of the CompleteTask things.
        /// Steve Hoover 3-24-16
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public void AddTestUser(int userId) 
        {
            var testJob = new Job {
                    GardenID = 1000,
                    Description = "Digging holes",
                    DateAssigned = DateTime.Now,
                    DateCompleted = DateTime.Now,
                    AssignedTo = userId,
                    AssignedFrom = 1002,
                    UserNotes = "Dig holes until you can't feel your arms."
                };
                try 
	            {
                    Console.WriteLine("Test job commented out in JobManager");
                    //JobAccessor.CreateTask(testJob);

	            }
	            catch (Exception)
	            {
		
		            throw;
	            }
        }
    }
}
