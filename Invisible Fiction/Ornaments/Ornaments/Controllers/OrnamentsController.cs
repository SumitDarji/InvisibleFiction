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
    public class OrnamentsController : Controller
    {
        #region # PROPERTIES #
        // SERVER MAP PATH
        public string ServerMapPath = ConfigurationManager.AppSettings["ServerMapPath"].ToString();
        public string PathRemoveString = ConfigurationManager.AppSettings["iPathRemoveString"].ToString();
        public string DirNameOrnamentImgSave = ConfigurationManager.AppSettings["DirNameOrnamentImgSave"].ToString();

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
        // GET: Category
        [CheckedLoggedIn]
        public ActionResult Index()
        {
            fnSetProperties();
            // USED IN POST METHOD
            ViewBag.IsSuccess = 0;
            ViewBag.Message = "";
            List<COrnaments> oResult = CFOrnaments.OrnamentsDetailList();
            ViewBag.OrnamentsList = oResult;
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
        public ActionResult Add()
        {
            // USED IN POST METHOD
            ViewBag.IsSuccess = 0;
            ViewBag.Message = "";
            return View(new OrnamentsModel());
        }

        [CheckedLoggedIn]
        [HttpPost]
        public ActionResult Add(OrnamentsModel ornamentsModel)
        {
            try
            {
                #region # SAVE ORNAMENT IMAGE #

                string imgDBSavePath = string.Empty;
                string sfileName = "";
                string sFilePath = "";

                if (ornamentsModel.OrnamentsImgFile != null)
                {
                    foreach (HttpPostedFileBase file in ornamentsModel.OrnamentsImgFile)
                    {
                        if (file.ContentLength > 0)
                        {
                            string sFileExt = System.IO.Path.GetExtension(file.FileName);

                            sfileName = ornamentsModel.Name + "-" + DateTime.Now.Ticks + sFileExt;
                            sfileName = sfileName.Replace(" ", String.Empty);

                            imgDBSavePath = DirNameOrnamentImgSave + "/" + sfileName;

                            string CombineServerMapPath = ServerMapPath + DirNameOrnamentImgSave;
                            sFilePath = Server.MapPath(CombineServerMapPath);
                            sFilePath = sFilePath.Replace("\\" + PathRemoveString.ToString(), "");
                            if (!Directory.Exists(sFilePath))
                            {
                                DirectoryInfo di = Directory.CreateDirectory(sFilePath);
                            }
                            var path = Path.Combine(sFilePath, sfileName);
                            //SAVE FILE ON DISK
                            file.SaveAs(path);
                            // SET Image PATH
                            ornamentsModel.OrnamentsImgPath.Add(imgDBSavePath);
                        }
                    }
                }

                #endregion

                fnSetProperties();
                CSQLResult oResult = CFOrnaments.OrnamentsDetailSave(ornamentsModel, ModifiedBy, LoginTypeCode);

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
                return View(ornamentsModel);
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
                ViewBag.IsSuccess = 0;
                ViewBag.Message = "";
                fnSetProperties();
                
                COrnaments ornaments = CFOrnaments.OrnamentsDetailGetById(id);
                OrnamentsModel objOrnamentsModel = new OrnamentsModel(ornaments);
                return View(objOrnamentsModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { Message = ex.Message, InnerException = ex.InnerException });
            }
        }

        [CheckedLoggedIn]
        [HttpPost]
        public ActionResult Edit(OrnamentsModel ornamentsModel)
        {
            try
            {
                #region # SAVE ORNAMENT IMAGE #

                string imgDBSavePath = string.Empty;
                string sfileName = "";
                string sFilePath = "";

                if (ornamentsModel.OrnamentsImgFile != null)
                {
                    foreach (HttpPostedFileBase file in ornamentsModel.OrnamentsImgFile)
                    {
                        if (file.ContentLength > 0)
                        {
                            string sFileExt = System.IO.Path.GetExtension(file.FileName);

                            sfileName = ornamentsModel.Name + "-" + DateTime.Now.Ticks + sFileExt;
                            sfileName = sfileName.Replace(" ", String.Empty);

                            imgDBSavePath = DirNameOrnamentImgSave + "/" + sfileName;

                            string CombineServerMapPath = ServerMapPath + DirNameOrnamentImgSave;
                            sFilePath = Server.MapPath(CombineServerMapPath);
                            sFilePath = sFilePath.Replace("\\" + PathRemoveString.ToString(), "");
                            if (!Directory.Exists(sFilePath))
                            {
                                DirectoryInfo di = Directory.CreateDirectory(sFilePath);
                            }
                            var path = Path.Combine(sFilePath, sfileName);
                            //SAVE FILE ON DISK
                            file.SaveAs(path);
                            // SET Image PATH
                            ornamentsModel.OrnamentsImgPath.Add(imgDBSavePath);
                        }
                    }
                }
                #endregion

                fnSetProperties();
                CSQLResult oResult = CFOrnaments.OrnamentsDetailSave(ornamentsModel, ModifiedBy, LoginTypeCode);

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
                return View(ornamentsModel);
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
                CSQLResult oResult = CFOrnaments.OrnamentsDetailRemove(id);
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