using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gios_mvcSolution.Models;

namespace Gios_mvcSolution.Areas.GIOS.Controllers
{
    public class SharedSVCTableController : Controller
    {
        SVC svcDao = new SVC();
        //GET: Location Hier DATA ASYNC
        // level represents Level to retrieve
        [HttpPost]
        public ActionResult GetLocationHierData(string level, string id)
        {
            if (level == "BU")
            {
                BusinessUnit BU = new BusinessUnit();
                BU.BuList = new SelectList(svcDao.GetBusinessUnits(), "strBUID", "strBUID");
                return Json(BU.BuList, JsonRequestBehavior.AllowGet);
            }
            else if (level == "Seg")
            {
                BUSegment Segment = new BUSegment
                {
                    SegmentSelectList = new SelectList(svcDao.GetSegments(id), "strSegmentID", "strSegmentID")
                };
                return Json(Segment.SegmentSelectList, JsonRequestBehavior.AllowGet);
            }
            else if (level == "Plant")
            {
                Plant Plant = new Plant();
                Plant.PlantList = new SelectList(svcDao.GetPlants(id), "intPlantID", "strPlantName");
                return Json(Plant.PlantList, JsonRequestBehavior.AllowGet);
            }
            else if (level == "Proc")
            {
                ProcessPlant Process = new ProcessPlant();
                Process.ProcessPlantList = new SelectList(svcDao.GetProcessPlants(Convert.ToInt32(id)), "intProductID", "strProcessID");
                return Json(Process.ProcessPlantList, JsonRequestBehavior.AllowGet);
            }
            else if(level == "Line")
            {
                ProcessLine Line = new ProcessLine();
                Line.ProcessLineList = new SelectList(svcDao.GetProcessLines(Convert.ToInt32(id)), "ProductProcessID", "strLineID");
                return Json(Line.ProcessLineList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View("PageNotFound");
            }


        }
    }
}