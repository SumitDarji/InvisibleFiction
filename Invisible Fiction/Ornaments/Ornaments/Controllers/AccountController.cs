using Ornaments.BusinessObject;
using Ornaments.Models;
using System;
using System.Web.Mvc;

namespace Ornaments.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            try
            {
                if (Session["UserID"] == null)
                {
                    return View(new LoginModel());
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { Message = ex.Message, InnerException = ex.InnerException });
            }
        }

        // POST: /General/Login
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel oLM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CGeneralUser oUser = CFGeneral.Login(oLM.Username, oLM.Password);
                    if (oUser.Success)
                    {
                        Session["UserID"] = oUser.UserID;
                        Session["LoginTypeCode"] = oUser.LoginTypeCode;
                        Session["DisplayName"] = oUser.DisplayName;
                        Session["LastLogged"] = oUser.LastLogged;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.ErrorMsg = oUser.Exception;
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { Message = ex.Message, InnerException = ex.InnerException });
            }
        }

        // GET: /General/Logout
        public ActionResult Logout()
        {
            try
            {
                Session["UserID"] = null;
                Session["LoginTypeCode"] = null;
                Session["DisplayName"] = null;
                Session["LastLogged"] = null;
                Session.Abandon();
                Session.RemoveAll();
                Session.Clear();
                //FormsAuthentication.SignOut(); //YOU WRITE THIS WHEN YOU USE FORMSAUTHENTICATION

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { Message = ex.Message, InnerException = ex.InnerException });
            }
        }

        #region # FORGOT PASSWORD #

        // GET: /General/Forgot
        public ActionResult Forgot()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { Message = ex.Message, InnerException = ex.InnerException });
            }
        }

        // POST: /General/Forgot
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Forgot(ForgotModel oFM)
        {
            try
            {
                if (!String.IsNullOrEmpty(oFM.Username) || !String.IsNullOrEmpty(oFM.Email))
                {
                    CGeneralUser oUser = CFGeneral.ForgotPassword(oFM.Username, oFM.Email);
                    if (oUser.Success)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        ViewBag.ErrorMsg = oUser.Exception;
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { Message = ex.Message, InnerException = ex.InnerException });
            }
        }

        #endregion

    }
}