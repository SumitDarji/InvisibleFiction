using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ornaments.Controllers
{
    public class ErrorController : Controller
    {
        // GET: /Erro/
        public ActionResult Index(string Message, string InnerException)
        {
            ViewBag.Message = Message ?? "";
            ViewBag.InnerException = InnerException ?? "";

            return View();
        }

        //
        // GET: /Error/
        public ActionResult Error404()
        {
            return View();
        }
    }
}