using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObjects;
using System.Collections.Generic;

namespace com.GreenThumb.UnitTests
{
    /// <summary>
    /// Test class to test Data Insertion 
    /// Created By: Nasr Mohammed 3/20/2016 
    /// </summary>
    [TestClass]
    public class JobUnitTests
    {
        JobManager jobManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            jobManager = new JobManager();
        }

        [TestMethod]
        public void TestInsertData()
        {
            Job job = new Job();
            // arrange
            job.JobID = 1000;
            job.GardenID = 1000;
            job.Description = "Gardening";
            job.DateAssigned = new DateTime(1998, 04, 30);
            job.DateCompleted = new DateTime(1998, 04, 30);
            job.AssignedTo = 1000;
            job.AssignedFrom = 1000;
            job.UserNotes = "Gardening is done";
            job.Active = true;

            // act
            bool result = jobManager.AddNewTask(job);

            // assert 
            Assert.AreEqual(result, true);

        }
    }
}
