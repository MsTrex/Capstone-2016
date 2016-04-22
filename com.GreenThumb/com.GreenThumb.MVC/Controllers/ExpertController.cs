using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace com.GreenThumb.MVC.Controllers
{
    [Authorize]
    public class ExpertController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Submits a Request to become an expert
        /// Date: 4/20/16
        /// Author: Chris Schwebach
        /// </summary>
        /// <param name="groupID, UserID"></param>
        /// <returns></returns>
        public ActionResult RequestJoinExpert()
        {
            int groupID = 1001; 
            GroupRequest request = new GroupRequest();
            request.UserID = RetrieveUserId();
            request.RequestDate = DateTime.Now;
            request.GroupID = (int)groupID;

            GroupManager manager = new GroupManager();
            try
            {
               if (manager.AddGroupMember(request) == 1)
                 {
                     return RedirectToAction("SuccessRequest", "Expert");
                 }                      

            }
            catch (Exception)
            {
                    //request failed
            }
            return RedirectToAction("AlreadyExpert", "Expert");
        }

        public ActionResult SuccessRequest()
        {
            return View();
        }

        public ActionResult AlreadyExpert()
        {
            return View();
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