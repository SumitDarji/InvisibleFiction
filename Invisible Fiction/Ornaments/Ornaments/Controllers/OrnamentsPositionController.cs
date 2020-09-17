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
    public class OrnamentsPositionController : Controller
    {
        #region # PROPERTIES #
        // SERVER MAP PATH
        public string ServerMapPath = ConfigurationManager.AppSettings["ServerMapPath"].ToString();
        public string PathRemoveString = ConfigurationManager.AppSettings["iPathRemoveString"].ToString();
        public string DirNamePositionImgSave = ConfigurationManager.AppSettings["DirNamePositionImgSave"].ToString();
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
            List<COrnamentsPosition> oResult = CFOrnamentsPosition.OrnamentsPositionDetailList();
            ViewBag.OrnamentsPositionList = oResult;
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
            return View(new OrnamentsPositionModel());
        }

        [CheckedLoggedIn]
        [HttpPost]
        public ActionResult Add(OrnamentsPositionModel ornamentsPositionModel)
        {
            try
            {
                #region # SAVE IMAGE #

                string imgDBSavePath = ornamentsPositionModel.ImgPath;
                string sfileName = "";
                string sFilePath = "";

                if (ornamentsPositionModel.ImgFile != null)
                {
                    if (ornamentsPositionModel.ImgFile.ContentLength > 0)
                    {
                        string sFileExt = System.IO.Path.GetExtension(ornamentsPositionModel.ImgFile.FileName);

                        sfileName = ornamentsPositionModel.Name + "-" + DateTime.Now.ToString("ddMMyyHHmmss") + sFileExt;
                        sfileName = sfileName.Replace(" ", String.Empty);

                        imgDBSavePath = DirNamePositionImgSave + "/" + sfileName;

                        string CombineServerMapPath = ServerMapPath + DirNamePositionImgSave;
                        sFilePath = Server.MapPath(CombineServerMapPath);
                        sFilePath = sFilePath.Replace("\\" + PathRemoveString.ToString(), "");

                        if (!Directory.Exists(sFilePath))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(sFilePath);
                        }
                        var path = Path.Combine(sFilePath, sfileName);
                        if (!String.IsNullOrEmpty(ornamentsPositionModel.ImgPath))
                        {
                            //SAVE FILE ON DISK
                            ornamentsPositionModel.ImgFile.SaveAs(path);
                            var RemoveOldImage = ornamentsPositionModel.ImgPath.Replace("/", "\\");
                            RemoveOldImage = sFilePath + RemoveOldImage.Replace(DirNamePositionImgSave, "");
                            //CHEK FILE IS EXIST ON DISK?
                            if (System.IO.File.Exists(RemoveOldImage))
                            {
                                //IF YES THEN SLEEP THREAD FOR 5 SEC AND DELETED EXISTING FILE
                                System.IO.File.Delete(RemoveOldImage);
                            }
                        }
                        else
                        {
                            //SAVE FILE ON DISK
                            ornamentsPositionModel.ImgFile.SaveAs(path);
                        }
                        // SET ORG LOGO PATH
                        ornamentsPositionModel.ImgPath = imgDBSavePath;
                    }
                }

                #endregion
                fnSetProperties();
                CSQLResult oResult = CFOrnamentsPosition.OrnamentsPositionDetailSave(ornamentsPositionModel, ModifiedBy, LoginTypeCode);

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
                return View(ornamentsPositionModel);
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
                
                COrnamentsPosition ornamentsPosition = CFOrnamentsPosition.OrnamentsPositionDetailGetById(id);
                OrnamentsPositionModel objOrnamentsPositionModel = new OrnamentsPositionModel(ornamentsPosition);
                return View(objOrnamentsPositionModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { Message = ex.Message, InnerException = ex.InnerException });
            }
        }

        [CheckedLoggedIn]
        [HttpPost]
        public ActionResult Edit(OrnamentsPositionModel ornamentsPositionModel)
        {
            try
            {
                #region # SAVE IMAGE #

                string imgDBSavePath = ornamentsPositionModel.ImgPath;
                string sfileName = "";
                string sFilePath = "";

                if (ornamentsPositionModel.ImgFile != null)
                {
                    if (ornamentsPositionModel.ImgFile.ContentLength > 0)
                    {
                        string sFileExt = System.IO.Path.GetExtension(ornamentsPositionModel.ImgFile.FileName);

                        sfileName = ornamentsPositionModel.Name + "-" + DateTime.Now.ToString("ddMMyyHHmmss") + sFileExt;
                        sfileName = sfileName.Replace(" ", String.Empty);

                        imgDBSavePath = DirNamePositionImgSave + "/" + sfileName;

                        string CombineServerMapPath = ServerMapPath + DirNamePositionImgSave;
                        sFilePath = Server.MapPath(CombineServerMapPath);
                        sFilePath = sFilePath.Replace("\\" + PathRemoveString.ToString(), "");

                        if (!Directory.Exists(sFilePath))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(sFilePath);
                        }
                        var path = Path.Combine(sFilePath, sfileName);
                        if (!String.IsNullOrEmpty(ornamentsPositionModel.ImgPath))
                        {
                            //SAVE FILE ON DISK
                            ornamentsPositionModel.ImgFile.SaveAs(path);
                            var RemoveOldImage = ornamentsPositionModel.ImgPath.Replace("/", "\\");
                            RemoveOldImage = sFilePath + RemoveOldImage.Replace(DirNamePositionImgSave, "");
                            //CHEK FILE IS EXIST ON DISK?
                            if (System.IO.File.Exists(RemoveOldImage))
                            {
                                //IF YES THEN SLEEP THREAD FOR 5 SEC AND DELETED EXISTING FILE
                                System.IO.File.Delete(RemoveOldImage);
                            }
                        }
                        else
                        {
                            //SAVE FILE ON DISK
                            ornamentsPositionModel.ImgFile.SaveAs(path);
                        }
                        // SET ORG LOGO PATH
                        ornamentsPositionModel.ImgPath = imgDBSavePath;
                    }
                }

                #endregion

                fnSetProperties();
                CSQLResult oResult = CFOrnamentsPosition.OrnamentsPositionDetailSave(ornamentsPositionModel, ModifiedBy, LoginTypeCode);

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
                return View(ornamentsPositionModel);
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
                CSQLResult oResult = CFOrnamentsPosition.OrnamentsPositionDetailRemove(id);
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