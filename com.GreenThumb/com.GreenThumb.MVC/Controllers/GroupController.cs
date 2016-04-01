using com.GreenThumb.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using com.GreenThumb.BusinessObjects;

using com.GreenThumb.BusinessLogic;


namespace com.GreenThumb.MVC.Controllers
{
    public class GroupController : Controller
    {
        // GET: Group
        public ActionResult Index()
        {
            return View();
        }

        // GET: Group/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Group/Create
        public ActionResult Create()
        {
            var userName = User.Identity.GetUserName();
            var model = new com.GreenThumb.BusinessLogic.UserManager().GetUserByUserName(userName);
            return View(model);
        }

        // POST: Group/Create
        [HttpPost]
       
        
        //take in userid and group name/create something to bind these too.
       public ActionResult Create([Bind(Include="GroupName")]string groupName)
         {
            if (ModelState.IsValid)
            {
                UserManager userManager = new UserManager();
                var user = userManager.GetUserByUserName(User.Identity.GetUserName());
                
                try
                {
                    com.GreenThumb.BusinessLogic.GroupManager groupManager = new BusinessLogic.GroupManager();
                    groupManager.AddGroup(user.UserID, groupName); //hard coded garbage data-need to replace supplied by request
                    ViewBag.StatusMessage = "Your group was created!";
                    
                }
                catch
                {
                    return View();
                }
                
                
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Group/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Group/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Group/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Group/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
