using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using com.GreenThumb.MVC.Models;
using Microsoft.AspNet.Identity;
using com.GreenThumb.BusinessLogic;

namespace com.GreenThumb.MVC.Controllers
{
    public class GardenController : Controller
    {
        // GET: Garden
        //Created by: Chris Schwebach
        public ActionResult Index()
        {
            int userId = RetrieveUserId();
            try
            {
                var model = new com.GreenThumb.BusinessLogic.GardenManager().GetGardenByUser(userId);
                return View(model);
            }
            catch
            {
                throw;
            }
        }

        #region Helper Methods

        // Created by: Trent Cullinan 03/31/2016
        private int RetrieveUserId()
        {
            int userId = 0;

            var userName = User.Identity.GetUserName();

            if (null != userName)
            {
                userId = new UserManager().RetrieveUserId(userName);
            }

            return userId;
        }

        #endregion

    }
}