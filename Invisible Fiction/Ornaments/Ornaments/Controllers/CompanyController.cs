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
    public class CompanyController : Controller
    {
        #region # PROPERTIES #

        // SERVER MAP PATH
        public string ServerMapPath = ConfigurationManager.AppSettings["ServerMapPath"].ToString();
        public string PathRemoveString = ConfigurationManager.AppSettings["iPathRemoveString"].ToString();
        public string DirNameCompanyLogoSave = ConfigurationManager.AppSettings["DirNameCompanyLogoSave"].ToString();

        public int UserID { get; set; }
        public int ModifiedBy { get; set; }
        public int LoginTypeCode { get; set; }

        public void fnSetProperties()
        {
            UserID = Convert.ToInt32(Session["UserID"].ToString());
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
            List<CCompany> oResult = CFCompany.CompanyDetailList(UserID);
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
            return View(oResult);
        }

        [CheckedLoggedIn]
        public ActionResult Add()
        {
            // USED IN POST METHOD
            ViewBag.IsSuccess = 0;
            ViewBag.Message = "";
            return View(new CompanyModel());
        }

        [CheckedLoggedIn]
        [HttpPost]
        public ActionResult Add(CompanyModel companyModel)
        {
            try
            {
                #region # SAVE COMPANY LOGO IMAGE #

                string imgDBSavePath = companyModel.LogoImgPath;
                string sfileName = "";
                string sFilePath = "";

                if (companyModel.LogoImgFile != null)
                {
                    if (companyModel.LogoImgFile.ContentLength > 0)
                    {
                        string sFileExt = System.IO.Path.GetExtension(companyModel.LogoImgFile.FileName);

                        sfileName = companyModel.Name + "-" + DateTime.Now.ToString("ddMMyyHHmmss") + sFileExt;
                        sfileName = sfileName.Replace(" ", String.Empty);
                        

                        imgDBSavePath = DirNameCompanyLogoSave + "/" + sfileName;

                        string CombineServerMapPath = ServerMapPath + DirNameCompanyLogoSave;
                        sFilePath = Server.MapPath(CombineServerMapPath);
                        sFilePath = sFilePath.Replace("\\" + PathRemoveString.ToString(), "");

                        if (!Directory.Exists(sFilePath))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(sFilePath);
                        }

                        var path = Path.Combine(sFilePath, sfileName);

                        if (!String.IsNullOrEmpty(companyModel.LogoImgPath))
                        {
                            //SAVE FILE ON DISK
                            companyModel.LogoImgFile.SaveAs(path);

                            var RemoveOldImage = companyModel.LogoImgPath.Replace("/", "\\");
                            RemoveOldImage = sFilePath + RemoveOldImage.Replace(DirNameCompanyLogoSave, "");

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
                            companyModel.LogoImgFile.SaveAs(path);
                        }
                        // SET ORG LOGO PATH
                        companyModel.LogoImgPath = imgDBSavePath;
                    }
                }

                #endregion

                fnSetProperties();
                CSQLResult oResult = CFCompany.CompanyDetailSave(companyModel, ModifiedBy);

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
                return View(companyModel);
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
                
                CCompany company = CFCompany.CompanyDetailGetById(id);
                CompanyModel objCompanyModel = new CompanyModel(company);
                return View(objCompanyModel);
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
                CCompany company = CFCompany.CompanyDetailGetById(id);
                CompanyModel objCompanyModel = new CompanyModel(company);
                return View("Edit", objCompanyModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { Message = ex.Message, InnerException = ex.InnerException });
            }
        }

        [CheckedLoggedIn]
        [HttpPost]
        public ActionResult Edit(CompanyModel companyModel)
        {
            try
            {
                ViewBag.Header = "Edit";
                #region # SAVE COMPANY LOGO IMAGE #

                string imgDBSavePath = companyModel.LogoImgPath;
                string sfileName = "";
                string sFilePath = "";

                if (companyModel.LogoImgFile != null)
                {
                    if (companyModel.LogoImgFile.ContentLength > 0)
                    {
                        string sFileExt = System.IO.Path.GetExtension(companyModel.LogoImgFile.FileName);

                        sfileName = companyModel.Name + "-" + DateTime.Now.ToString("ddMMyyHHmmss") + sFileExt;
                        sfileName = sfileName.Replace(" ", String.Empty);


                        imgDBSavePath = DirNameCompanyLogoSave + "/" + sfileName;

                        string CombineServerMapPath = ServerMapPath + DirNameCompanyLogoSave;
                        sFilePath = Server.MapPath(CombineServerMapPath);
                        sFilePath = sFilePath.Replace("\\" + PathRemoveString.ToString(), "");

                        if (!Directory.Exists(sFilePath))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(sFilePath);
                        }

                        var path = Path.Combine(sFilePath, sfileName);

                        if (!String.IsNullOrEmpty(companyModel.LogoImgPath))
                        {
                            //SAVE FILE ON DISK
                            companyModel.LogoImgFile.SaveAs(path);

                            var RemoveOldImage = companyModel.LogoImgPath.Replace("/", "\\");
                            RemoveOldImage = sFilePath + RemoveOldImage.Replace(DirNameCompanyLogoSave, "");

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
                            companyModel.LogoImgFile.SaveAs(path);
                        }
                        // SET ORG LOGO PATH
                        companyModel.LogoImgPath = imgDBSavePath;
                    }
                }

                #endregion

                fnSetProperties();
                CSQLResult oResult = CFCompany.CompanyDetailSave(companyModel, ModifiedBy);

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
                return View(companyModel);
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
                CSQLResult oResult = CFCompany.CompanyDetailRemove(id);
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