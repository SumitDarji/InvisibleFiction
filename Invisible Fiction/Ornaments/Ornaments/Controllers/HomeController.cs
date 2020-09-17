using Ornaments.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Ornaments.FilterConfig;

namespace Ornaments.Controllers
{
    public class HomeController : Controller
    {
        [CheckedLoggedIn]
        public ActionResult Index()
        {
            CDashboard oResult = CFGeneral.DashBoardDetail();
            return View(oResult);
        }

        [CheckedLoggedIn]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [CheckedLoggedIn]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}