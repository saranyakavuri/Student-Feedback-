using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gios_mvcSolution.Models;

namespace Gios_mvcSolution.Areas.Admin.Controllers
{
    public class LocationAdminController : Controller
    {
        SVC svcDao = new SVC();

        // GET: Admin/LocationAdmin
        public ActionResult Index()
        {
            BusinessUnit BU = new BusinessUnit();
            BU.BUs = svcDao.ListBusinessUnits().ToList();
            return View(BU.BUs);
        }
        public ActionResult ListSegment()
        {
            BUSegment Seg = new BUSegment();
            Seg.Segments = svcDao.ListSegments().ToList();
            return PartialView(Seg.Segments);
        }

        // GET: LocationAdmin/Create
        public ActionResult CreateBU()
        {
            return PartialView();
        }

        // POST: LocationAdmin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBU([Bind(Include = "strBUID, strBUDescr, strGroupID, strCreatorID")] BusinessUnit NewBU)
        {

            try
            {
                string message = svcDao.CreateEditBUsinessUnit("I", NewBU);

                ViewBag.Err = message;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: LocationAdmin/Edit/5
        public ActionResult EditBU(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var businessUnitToUpdate = svcDao.FindBusinessUnit(id);
            BusinessUnit BU = businessUnitToUpdate;
            if (BU == null)
            {
                return HttpNotFound();
            }
            return PartialView(BU);
        }

        // POST: LocationAdmin/Edit/5
        [HttpPost]
        public ActionResult EditBU(string id, [Bind(Include = "strBUID, strBUDescr, strGroupID,dtmCreated, strCreatorID, dtmModified, strModifierID")] BusinessUnit modifiedBU)
        {
            try
            {
                string message = svcDao.CreateEditBUsinessUnit("U", modifiedBU);

                ViewBag.Err = message;
                return RedirectToAction("Index");


            }
            catch
            {
                return View();
            }
        }

        // GET: LocationAdmin/Delete/5
        public ActionResult DeleteBU(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var businessUnitToUpdate = svcDao.FindBusinessUnit(id);
            BusinessUnit BU = businessUnitToUpdate;
            if (BU == null)
            {
                return HttpNotFound();
            }
            return PartialView(BU);
        }

        // POST: LocationAdmin/Delete/5
        //[HttpPost]
        //public ActionResult DeleteBU(string id, [Bind(Include ="strBUID")] BusinessUnit deleteBU)
        //{
        //    //try
        //    //{
        //    //    string message = svcDao.RemoveBusinessUnit("D", deleteBU);

        //    //    ViewBag.Err = message;
        //    //    return RedirectToAction("Index");


        //    //}
        //    //catch
        //    //{
        //    //    return View();
        //    //}
        //}
    }
}