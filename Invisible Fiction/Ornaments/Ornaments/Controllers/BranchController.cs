using Ornaments.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using Ornaments.BusinessObject;
using System.Configuration;
using static Ornaments.FilterConfig;

namespace Ornaments.Controllers
{
    public class BranchController : Controller
    {
        #region # PROPERTIES #

        public int UserID { get; set; }
        public int ModifiedBy { get; set; }
        public int LoginTypeCode { get; set; }

        public void fnSetProperties()
        {
            UserID = Convert.ToInt32(Session["UserID"]);
            ModifiedBy = Convert.ToInt32(Session["UserID"]);
            LoginTypeCode = Convert.ToInt32(Session["LoginTypeCode"]);
        }

        #endregion
        // GET: Company
        [CheckedLoggedIn]
        public ActionResult Index()
        {
            fnSetProperties();
            // USED IN POST METHOD
            ViewBag.IsSuccess = 0;
            ViewBag.Message = "";
            List<CBranch> oResult = CFBranch.BranchDetailList(UserID, LoginTypeCode);
            ViewBag.BranchList = oResult;
            if (TempData["Message"] != null)
            {
                if (Convert.ToBoolean(TempData["IsSuccess"]))
                {
                    ViewBag.IsSuccess = 1;
                    ViewBag.Message = TempData["Message"];
                }
                else
                {
                    ViewBag.IsSuccess = 0;
                    ViewBag.Message = TempData["Message"];
                }
            }
            return View();
        }

        [CheckedLoggedIn]
        public ActionResult Partial_BranchDetailList(int companyID)
        {
            try
            {
                fnSetProperties();
                fnSetProperties();
                List<CBranch> oResult = CFBranch.BranchDetailList(companyID, LoginTypeCode);
                return PartialView(oResult);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { Message = ex.Message, InnerException = ex.InnerException });
            }
        }

        [CheckedLoggedIn]
        public ActionResult Add()
        {
            // USED IN POST METHOD
            ViewBag.IsSuccess = 0;
            ViewBag.Message = "";
            return View(new BranchModel());
        }

        [CheckedLoggedIn]
        [HttpPost]
        public ActionResult Add(BranchModel branchModel)
        {
            try
            {
                fnSetProperties();
                
                CSQLResult oResult = CFBranch.BranchDetailSave(branchModel, ModifiedBy, LoginTypeCode);

                if (oResult.Success)
                {
                    ViewBag.IsSuccess = 1;
                    ViewBag.Message = oResult.Exception;
                }
                else
                {
                    ViewBag.IsSuccess = 0;
                    ViewBag.Message = oResult.Exception;
                }
                return View(branchModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { Message = ex.Message, InnerException = ex.InnerException, StackTrace = ex.StackTrace });
            }
        }


        [CheckedLoggedIn]
        public ActionResult Edit(int id)
        {
            try
            {
                // USED IN POST METHOD
                ViewBag.Header = "Edit";
                ViewBag.IsSuccess = 0;
                ViewBag.Message = "";
                fnSetProperties();
                
                CBranch branch = CFBranch.BranchDetailGetById(id);
                BranchModel objBranchModel = new BranchModel(branch);
                return View(objBranchModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { Message = ex.Message, InnerException = ex.InnerException });
            }
        }

        [CheckedLoggedIn]
        public ActionResult View(int id)
        {
            try
            {
                // USED IN POST METHOD
                ViewBag.Header = "View";
                ViewBag.IsSuccess = 0;
                ViewBag.Message = "";
                fnSetProperties();

                CBranch branch = CFBranch.BranchDetailGetById(id);
                BranchModel objBranchModel = new BranchModel(branch);
                return View("Edit", objBranchModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { Message = ex.Message, InnerException = ex.InnerException });
            }
        }

        [CheckedLoggedIn]
        [HttpPost]
        public ActionResult Edit(BranchModel branchModel)
        {
            try
            {
                ViewBag.Header = "Edit";
                fnSetProperties();
                CSQLResult oResult = CFBranch.BranchDetailSave(branchModel, ModifiedBy, LoginTypeCode);

                if (oResult.Success)
                {
                    ViewBag.IsSuccess = 1;
                    ViewBag.Message = oResult.Exception;
                }
                else
                {
                    ViewBag.IsSuccess = 0;
                    ViewBag.Message = oResult.Exception;
                }
                return View(branchModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new
                {
                    Message = ex.Message,
                    InnerException = ex.InnerException,
                    StackTrace = ex.StackTrace
                });
            }
        }

        [CheckedLoggedIn]
        public JsonResult Delete(int id)
        {
            try
            {
                // USED IN POST METHOD
                ViewBag.IsSuccess = 0;
                ViewBag.Message = "";
                fnSetProperties();
                CSQLResult oResult = CFBranch.BranchDetailRemove(id);
                return Json(oResult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                CSQLResult oResult = new CSQLResult();
                oResult.Exception = ex.Message;
                oResult.Success = false;
                oResult.WasSuccessful = 0;
                oResult.OtherParameter = ex.InnerException.ToString();
                return Json(oResult, JsonRequestBehavior.AllowGet);
            }
        }
    }
}