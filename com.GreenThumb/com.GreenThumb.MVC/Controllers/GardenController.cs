using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.GreenThumb.MVC.Controllers
{
    public class GardenController : Controller
    {
        // GET: Garden
        public ActionResult Index()
        {
            return View();
        }
    }
}