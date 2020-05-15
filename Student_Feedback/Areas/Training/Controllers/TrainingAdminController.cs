using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net;
using Gios_mvcSolution.Models;
using System.Web.Http;

namespace Gios_mvcSolution.Areas.Training.Controllers
{
    public class TrainingAdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return null;
        }
        
        }
}