using Gios_mvcSolution.Areas.UseCase.Models;
using Gios_mvcSolution.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Gios_mvcSolution.Areas.UseCase.Controllers
{
    public class AdminController : Controller
    {
        UseCase_DAL useCase_DAL = new UseCase_DAL(); 
        
        // GET: UseCase/Admin
        public ActionResult Index()
        {
            return View();
        }

        #region Required Support Types List, Create, Modify
        public ActionResult ListRequiredSupport()
        {
            RequiredSupportTypes requiredSupportTypes = new RequiredSupportTypes();
            requiredSupportTypes.RequiredSupportList = useCase_DAL.ListrequiredSupportTypes().ToList();
            return PartialView(requiredSupportTypes.RequiredSupportList);
        }


        // GET: UseCase/Admin/CreateRequiredSupport
        public ActionResult CreateRequiredSupport()
        {
            return PartialView();
        }

        // POST: UseCase/Admin/CreateRequiredSupport
        [HttpPost]
        public ActionResult CreateRequiredSupport([Bind(Include = "strSupportType, strSupportTypeDescr")] RequiredSupportTypes newSupportType)
        {
            try
            {

                newSupportType.strCreatorID = GetUserID();
                string message = useCase_DAL.CreateEditRequiredSupportType("I", newSupportType);
                ViewBag.Err = message;
                return RedirectToAction("Index");
            }
            catch
            {
               // ViewBag.Err(ex.Message);
                return View();
            }
        }



        // GET: UseCase/Admin/Edit/5
        public ActionResult UpdateRequiredSupport(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var supportTypeToEdit = useCase_DAL.FindRequiredSupportType(id);
            RequiredSupportTypes reqSupType = supportTypeToEdit;
            return PartialView(reqSupType);
        }

        // POST: UseCase/Admin/Edit/5
        [HttpPost]
        public ActionResult UpdateRequiredSupport(int id, [Bind(Include = "intSupportTypeID, strSupportType, strSupportTypeDescr, strCreatorID")] RequiredSupportTypes modSupportType)
        {
            try
            {
                // TODO: Add update logic here
                modSupportType.strModifierID = GetUserID();
                string message = useCase_DAL.CreateEditRequiredSupportType("U", modSupportType);

                ViewBag.Err = message;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #endregion

        #region File Types List,Create,Modify
        public ActionResult ListFileTypes()
        {
            FileTypes fileTypes = new FileTypes();
            fileTypes.FileTypeList = useCase_DAL.ListFileTypes().ToList();
            return PartialView(fileTypes.FileTypeList);
        }

        public ActionResult CreateFileType()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult CreateFileType ([Bind(Include = "strFileType, strFileDescription")] FileTypes newFileType)
        {
            try
            {
                newFileType.strCreatorID = GetUserID();
                string message = useCase_DAL.CreateEditFileType("I", newFileType);
                ViewBag.Err = message;
                return RedirectToAction("Index");
            }
            catch
            {
                // ViewBag.Err(ex.Message);
                return View();
            }
        }

        public ActionResult UpdateFileType(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var fileTypeToEdit = useCase_DAL.FindFileType(id);
            FileTypes fileTypes = fileTypeToEdit;
            return PartialView(fileTypes);
        }

        [HttpPost]
        public ActionResult UpdateFileType(string id, [Bind(Include ="strFileType, strFileDescription, strCreatorID")] FileTypes modFileType)
        {
            try
            {
                // TODO: Add update logic here
                modFileType.strModifierID = GetUserID();
                string message = useCase_DAL.CreateEditFileType("U", modFileType);

                ViewBag.Err = message;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        #endregion

        #region Status List, Create, Modify

        public ActionResult ListStatus()
        {
            Status status = new Status();
            status.StatusList = useCase_DAL.ListStatuses().ToList();
            return PartialView(status.StatusList);
        }

        public ActionResult CreateStatus()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult CreateStatus([Bind(Include ="strStatus, strDescription, yesActive")] Status newStatus)
        {
            try
            {
                newStatus.strCreatorID = GetUserID();
                string message = useCase_DAL.CreateEditStatus("I", newStatus);
                ViewBag.Err = message;
                return RedirectToAction("Index");
            }
            catch
            {
                // ViewBag.Err(ex.Message);
                return View();
            }
        }

        public ActionResult UpdateStatus(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var StatusToEdit = useCase_DAL.FindStatus(id);
            Status status = StatusToEdit;
            return PartialView(status);
        }

        [HttpPost]
        public ActionResult UpdateStatus(string id, [Bind(Include ="strStatus, strDescription, yesActive, strCreatorID")] Status modStatus)
        {
            try
            {
                // TODO: Add update logic here
                modStatus.strModifierID = GetUserID();
                string message = useCase_DAL.CreateEditStatus("U", modStatus);
                ViewBag.Err = message;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        #endregion

        #region Impact KPI List, Create, Modify
        public ActionResult ListImpactKPI()
        {
            ImpactKPI impactKPI = new ImpactKPI()
            {
                impactKPIList = useCase_DAL.ListImpactKPIs().ToList()
            };
            return PartialView(impactKPI.impactKPIList);
        }

        public ActionResult CreateImpactKPI()
        {
            return PartialView();

        }

        [HttpPost]
        public ActionResult CreateImpactKPI([Bind(Include = "strKPI, strKPIDescr")] ImpactKPI newImpactKPI)
        {
            try
            {
                newImpactKPI.strCreatorID = GetUserID();
                string message = useCase_DAL.CreateEditImpactKPI("I", newImpactKPI);
                ViewBag.Err = message;
                return RedirectToAction("Index");
            }
            catch
            {
                // ViewBag.Err(ex.Message);
                return View();
            }
        }

        public ActionResult UpdateImpactKPI(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var KPIToEdit = useCase_DAL.FindImpactKPI(id);
            ImpactKPI impact = KPIToEdit;
            return PartialView(impact);
        }

        [HttpPost]
        public ActionResult UpdateImpactKPI(string id, [Bind(Include = "strKPI, strKPIDescr, strCreatorID")] ImpactKPI impactKPI)
        {
            try
            {
                // TODO: Add update logic here
                impactKPI.strModifierID = GetUserID();
                string message = useCase_DAL.CreateEditImpactKPI("U", impactKPI);
                ViewBag.Err = message;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #endregion


        #region Compass Cat List, Create, Modify
        public ActionResult ListCompassCat()
        {
            CompassCat compassCat = new CompassCat
            {
                CategoryList = useCase_DAL.ListCompassCats().ToList()
            };
            return PartialView(compassCat.CategoryList);
        }

        public ActionResult CreateCompassCat()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult CreateCompassCat([Bind(Include = "strCategoryID, strCategoryDescr")] CompassCat newCompassCat)
        {
            try
            {
                newCompassCat.strCreatorID = GetUserID();
                string message = useCase_DAL.CreateEditCompassCat("I", newCompassCat);
                ViewBag.Err = message;
                return RedirectToAction("Index");
            }
            catch
            {
                // ViewBag.Err(ex.Message);
                return View();
            }
        }

        public ActionResult UpdateCompassCat(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var CompassCatToEdit = useCase_DAL.FindCompassCat(id);
            CompassCat compassCat = CompassCatToEdit;
            return PartialView(compassCat);
        }

        [HttpPost]
        public ActionResult UpdateCompassCat(string id, [Bind(Include = "strCategoryID, strCategoryDescr, strCreatorID")] CompassCat modCompassCat)
        {
            try
            {
                // TODO: Add update logic here
                modCompassCat.strModifierID = GetUserID();
                string message = useCase_DAL.CreateEditCompassCat("U", modCompassCat);
                ViewBag.Err = message;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #endregion


        #region USE CASE APPROVALS

        public ActionResult ApproveUseCases()
        {
            UseCaseApprovals needsApproval = new UseCaseApprovals()
            {
                lstUseCaseApprovals = useCase_DAL.ListUseCaseApprovals().ToList()
            };
            //RequiredSupportTypes requiredSupportTypes = new RequiredSupportTypes();
            //requiredSupportTypes.RequiredSupportList = useCase_DAL.ListrequiredSupportTypes().ToList();

            return View(needsApproval.lstUseCaseApprovals);
        }



        #endregion

        private string GetUserID()
        {
            return Request.LogonUserIdentity.Name.Split('\\').Last();
        }

        #region AJAC FUNCS????
        [HttpPost]
        public ActionResult DDLCompassCat(string id)
        {
            CompassCat compassCat = new CompassCat
            {
                CategoryDDL = new SelectList(useCase_DAL.ListCompassCats(), "strCategoryID", "strCategoryDescr")
            };
            return Json(compassCat.CategoryDDL, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RequiredSupportList()
        {
            RequiredSupportTypes requiredSupportTypes = new RequiredSupportTypes
            {
                RequiredSupportList = useCase_DAL.ListrequiredSupportTypes().ToList()
            };
            return Json(requiredSupportTypes.RequiredSupportList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult FileTypeList(string param)
        {
            FileTypes fileTypes = new FileTypes
            {
                FileTypeList = useCase_DAL.ListFileTypes().ToList()
            };
            return Json(fileTypes.FileTypeList, JsonRequestBehavior.AllowGet); 
        }

        #endregion
    }
}
