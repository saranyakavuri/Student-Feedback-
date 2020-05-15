using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gios_mvcSolution.Models;

namespace Gios_mvcSolution.Areas.UseCase.Controllers
{
    public class UseCaseController : Controller
    {
        // GET: UseCase/UseCase
        public ActionResult Index()
        {
            return View();
        }

        // GET: UseCase/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UseCase/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UseCase/Create
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

        // GET: UseCase/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UseCase/Edit/5
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

        // GET: UseCase/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UseCase/Delete/5
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