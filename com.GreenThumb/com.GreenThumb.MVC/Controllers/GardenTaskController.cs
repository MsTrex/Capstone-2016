using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using com.GreenThumb.BusinessLogic;
using com.GreenThumb.MVC.Models;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.MVC.Controllers
{
    [Authorize]
    public class GardenTaskController : Controller
    {
        
        // GET: GardenTask
        public ActionResult Index()
        {

            int userId = RetrieveUserId();
            try
            {
                IEnumerable<Group> model = new com.GreenThumb.BusinessLogic.GardenManager().GetGardenByUser(userId);
                GardenTaskViewModel modelObj = new GardenTaskViewModel();
                modelObj.GroupsList = model;
                return View(modelObj);
            }
            catch
            {
                throw;
            }
        }

        public ActionResult ViewTask(int gardenID)
        {
            int userId = RetrieveUserId();
            try
            {
                IEnumerable<Group> groups = new com.GreenThumb.BusinessLogic.GardenManager().GetGardenByUser(userId);
                IEnumerable<Job> jobs = new com.GreenThumb.BusinessLogic.JobManager().RetrieveJobByGardenId(gardenID);
                GardenTaskViewModel modelObj = new GardenTaskViewModel();
                modelObj.GroupsList = groups;
                modelObj.JobList = jobs;
                return View("Index", modelObj);
            }
            catch
            {
                throw;
            }
        }

        public ActionResult SaveTask(int gardenID,string description, bool isActive)
        {
            int userId = RetrieveUserId();
            try
            {
                Job jobData = new Job();
                jobData.GardenID = gardenID;
                jobData.Description = description;
                jobData.Active = isActive;
                var result = new com.GreenThumb.BusinessLogic.JobManager().AddNewTask(jobData);
                IEnumerable<Group> groups = new com.GreenThumb.BusinessLogic.GardenManager().GetGardenByUser(userId);
                GardenTaskViewModel modelObj = new GardenTaskViewModel();
                modelObj.GroupsList = groups;
                return View("Index", modelObj);
            }
            catch
            {
                throw;
            }
        }

        private int RetrieveUserId()
        {
            int userId = 0;

            var userName = User.Identity.GetUserName();

            if (null != userName)
            {
                userId = new UserManager().GetUserId(userName);
            }

            return userId;
        }
    }
}