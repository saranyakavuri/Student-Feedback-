using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gios_mvcSolution.Areas.GIOS.Controllers
{
    public class HomeController : Controller
    {
        // GET: GIOS/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PageNotFound()
        {
            ViewBag.Error = TempData["Error"].ToString();
            return View();
        }
    }
}