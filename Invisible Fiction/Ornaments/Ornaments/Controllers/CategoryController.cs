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
    public class CategoryController : Controller
    {
        #region # PROPERTIES #
        // SERVER MAP PATH
        public string ServerMapPath = ConfigurationManager.AppSettings["ServerMapPath"].ToString();
        public string PathRemoveString = ConfigurationManager.AppSettings["iPathRemoveString"].ToString();
        public string DirNameCategoryImgSave = ConfigurationManager.AppSettings["DirNameCategoryImgSave"].ToString();
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
            List<CCategory> oResult = CFCategory.CategoryDetailList();
            ViewBag.CategoryList = oResult;
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
            return View(new CategoryModel());
        }

        [CheckedLoggedIn]
        [HttpPost]
        public ActionResult Add(CategoryModel categoryModel)
        {
            try
            {
                #region # SAVE IMAGE #

                string imgDBSavePath = categoryModel.ImgPath;
                string sfileName = "";
                string sFilePath = "";

                if (categoryModel.ImgFile != null)
                {
                    if (categoryModel.ImgFile.ContentLength > 0)
                    {
                        string sFileExt = System.IO.Path.GetExtension(categoryModel.ImgFile.FileName);

                        sfileName = categoryModel.Name + "-" + DateTime.Now.ToString("ddMMyyHHmmss") + sFileExt;
                        sfileName = sfileName.Replace(" ", String.Empty);

                        imgDBSavePath = DirNameCategoryImgSave + "/" + sfileName;

                        string CombineServerMapPath = ServerMapPath + DirNameCategoryImgSave;
                        sFilePath = Server.MapPath(CombineServerMapPath);
                        sFilePath = sFilePath.Replace("\\" + PathRemoveString.ToString(), "");

                        if (!Directory.Exists(sFilePath))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(sFilePath);
                        }
                        var path = Path.Combine(sFilePath, sfileName);
                        if (!String.IsNullOrEmpty(categoryModel.ImgPath))
                        {
                            //SAVE FILE ON DISK
                            categoryModel.ImgFile.SaveAs(path);
                            var RemoveOldImage = categoryModel.ImgPath.Replace("/", "\\");
                            RemoveOldImage = sFilePath + RemoveOldImage.Replace(DirNameCategoryImgSave, "");
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
                            categoryModel.ImgFile.SaveAs(path);
                        }
                        // SET ORG LOGO PATH
                        categoryModel.ImgPath = imgDBSavePath;
                    }
                }

                #endregion 
                fnSetProperties();
                CSQLResult oResult = CFCategory.CategoryDetailSave(categoryModel, ModifiedBy, LoginTypeCode);

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
                return View(categoryModel);
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
                
                CCategory category = CFCategory.CategoryDetailGetById(id);
                CategoryModel objCategoryModel = new CategoryModel(category);
                return View(objCategoryModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { Message = ex.Message, InnerException = ex.InnerException });
            }
        }

        [CheckedLoggedIn]
        [HttpPost]
        public ActionResult Edit(CategoryModel categoryModel)
        {
            try
            {
                #region # SAVE IMAGE #

                string imgDBSavePath = categoryModel.ImgPath;
                string sfileName = "";
                string sFilePath = "";

                if (categoryModel.ImgFile != null)
                {
                    if (categoryModel.ImgFile.ContentLength > 0)
                    {
                        string sFileExt = System.IO.Path.GetExtension(categoryModel.ImgFile.FileName);

                        sfileName = categoryModel.Name + "-" + DateTime.Now.ToString("ddMMyyHHmmss") + sFileExt;
                        sfileName = sfileName.Replace(" ", String.Empty);

                        imgDBSavePath = DirNameCategoryImgSave + "/" + sfileName;

                        string CombineServerMapPath = ServerMapPath + DirNameCategoryImgSave;
                        sFilePath = Server.MapPath(CombineServerMapPath);
                        sFilePath = sFilePath.Replace("\\" + PathRemoveString.ToString(), "");

                        if (!Directory.Exists(sFilePath))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(sFilePath);
                        }
                        var path = Path.Combine(sFilePath, sfileName);
                        if (!String.IsNullOrEmpty(categoryModel.ImgPath))
                        {
                            //SAVE FILE ON DISK
                            categoryModel.ImgFile.SaveAs(path);
                            var RemoveOldImage = categoryModel.ImgPath.Replace("/", "\\");
                            RemoveOldImage = sFilePath + RemoveOldImage.Replace(DirNameCategoryImgSave, "");
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
                            categoryModel.ImgFile.SaveAs(path);
                        }
                        // SET ORG LOGO PATH
                        categoryModel.ImgPath = imgDBSavePath;
                    }
                }

                #endregion
                fnSetProperties();
                CSQLResult oResult = CFCategory.CategoryDetailSave(categoryModel, ModifiedBy, LoginTypeCode);

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
                return View(categoryModel);
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
                CSQLResult oResult = CFCategory.CategoryDetailRemove(id);
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