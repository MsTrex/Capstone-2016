using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using com.GreenThumb.MVC.Models;
using Microsoft.AspNet.Identity;
using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObjects;

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

        /// <summary>
        /// Created by: Kristine Johnson
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet]
        public ActionResult CreateGarden(int? id)
        {
            ActionResult view = RedirectToAction("Details", "Group", new { @id = id });

            if (id.HasValue)
            {
                GardenCreationViewModel model = new GardenCreationViewModel()
                {
                    GroupID = id.Value
                };

                view = View(model);
            }

            return view;
        }

        [HttpPost]
        public ActionResult CreateGarden(GardenCreationViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserManager userManager = new UserManager();

                Garden garden = new Garden();

                ///using Trent's helper method to get a userID
                garden.UserID = RetrieveUserId();
                garden.GardenDescription = model.GardenDescription;
                garden.GroupID = model.GroupID;


                GardenManager gardenManager = new GardenManager();

                if (gardenManager.AddGarden(garden))
                {
                    ViewBag.StatusMessage = "Your garden was created!";
                }
            }

            return RedirectToAction("Index", "Garden");
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