using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gios_mvcSolution.Areas.Mios;
using Gios_mvcSolution.Models;

namespace Gios_mvcSolution.Areas.Mios.Controllers
{
    public class MiosController : Controller
    {
        SVC svcDao = new SVC();
        // GET: Mios/Mios
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DataEntry()
        {

            BusinessUnit BU = new BusinessUnit();

            BU.BuList = new SelectList(svcDao.GetBusinessUnits(), "strBUID", "strBUID");


            return View(BU);
        }


        //GET: Location Hier DATA ASYNC
        // level represents Level to retrieve
        //[HttpPost]
        //public ActionResult GetLocationHierData(string level, string id)
        //{
        //    if (level == "Seg")
        //    {
        //        BUSegment Segment = new BUSegment();
        //        Segment.SegmentSelectList = new SelectList(svcDao.GetSegments(id), "strSegmentID", "strSegmentDescr");
        //        return Json(Segment.SegmentSelectList, JsonRequestBehavior.AllowGet);
        //    }
        //    else if (level == "Loc")
        //    {
        //        Location Loc = new Location();
        //        Loc.LocList = new SelectList(svcDao.GetLocations(id), "strLocationID", "strLocationDescr");
        //        return Json(Loc.LocList, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return View("DUMMY");
        //    }


        //}

        // GET: Mios/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Mios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mios/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Mios/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Mios/Edit/5
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

        // GET: Mios/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Mios/Delete/5
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