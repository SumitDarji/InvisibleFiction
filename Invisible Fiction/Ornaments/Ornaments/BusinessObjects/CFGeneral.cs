using System;
using System.Data;
using Ornaments.Code;
namespace Ornaments.BusinessObject
{
    public static class CFGeneral
    {
        public static CGeneralUser Login(string UserName, string PassWord)
        {
            CGeneralUser oResult = new CGeneralUser();
            try
            {
                CShared oDBShared = new CShared();
                string ePassword = CShared.GetEncryptString(PassWord);

                DataSet dsLogin = oDBShared.GetDataSet("TLogin", "uspLogin '" + UserName + "', '" + ePassword + "'");
                using (DataTable dtLogin = dsLogin.Tables["TLogin"])
                {
                    if (dtLogin != null && dtLogin.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dtLogin.Rows[0]["Success"]))
                        {
                            // TRUE FOR ORGANIZATION
                            oResult.DisplayName = dtLogin.Rows[0]["DisplayName"].ToString();
                            oResult.UserID = Convert.ToInt32(dtLogin.Rows[0]["UserID"]);
                            oResult.LoginTypeCode = Convert.ToInt32(dtLogin.Rows[0]["LoginTypeCode"]);
                            oResult.LastLogged = Convert.ToDateTime(dtLogin.Rows[0]["LastLogged"]).ToString("MM/dd/yyyy h:mm tt");
                            oResult.Success = Convert.ToBoolean(dtLogin.Rows[0]["Success"]);
                            oResult.WasSuccessful = Convert.ToInt32(dtLogin.Rows[0]["Success"]);
                            oResult.Exception = dtLogin.Rows[0]["ErrorSuccessMsg"].ToString();
                        }
                        // FAILED TO LOGIN
                        else
                        {
                            oResult.Success = Convert.ToBoolean(dtLogin.Rows[0]["Success"]);
                            oResult.WasSuccessful = Convert.ToInt32(dtLogin.Rows[0]["Success"]);
                            oResult.Exception = dtLogin.Rows[0]["ErrorSuccessMsg"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //oResult.Success = false;
                //oResult.WasSuccessful = 0;
                //oResult.Exception = ex.Message;

                throw ex;
            }
            return oResult;
        }

        public static CDashboard DashBoardDetail()
        {
            CDashboard oResult = new CDashboard();
            try
            {
                CShared oDBShared = new CShared();
                DataSet dsDashboard = oDBShared.GetDataSet("TDashBoard", "uspDashboardDetail");
                using (DataTable dtDashboard = dsDashboard.Tables["TDashBoard"])
                {
                    if (dtDashboard != null && dtDashboard.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dtDashboard.Rows[0]["Success"]))
                        {
                            // TRUE FOR ORGANIZATION
                            oResult.CompanyCount = Convert.ToInt32(dtDashboard.Rows[0]["CompanyCount"]);
                            oResult.CategoryCount = Convert.ToInt32(dtDashboard.Rows[0]["CategoryCount"]);
                            oResult.PositionCount = Convert.ToInt32(dtDashboard.Rows[0]["OrnamentPositionCount"]);
                            oResult.OrnamentCount = Convert.ToInt32(dtDashboard.Rows[0]["OrnamentCount"]);
                            oResult.Success = Convert.ToBoolean(dtDashboard.Rows[0]["Success"]);
                            oResult.WasSuccessful = Convert.ToInt32(dtDashboard.Rows[0]["Success"]);
                            oResult.Exception = dtDashboard.Rows[0]["ErrorSuccessMsg"].ToString();
                        }
                        else
                        {
                            oResult.Success = Convert.ToBoolean(dtDashboard.Rows[0]["Success"]);
                            oResult.WasSuccessful = Convert.ToInt32(dtDashboard.Rows[0]["Success"]);
                            oResult.Exception = dtDashboard.Rows[0]["ErrorSuccessMsg"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //oResult.Success = false;
                //oResult.WasSuccessful = 0;
                //oResult.Exception = ex.Message;

                throw ex;
            }
            return oResult;
        }

        public static CGeneralUser ForgotPassword(string Username, string Email)
        {
            CGeneralUser oResult = new CGeneralUser();
            try
            {
                CShared oDBShared = new CShared();
                DataSet dsLogin = oDBShared.GetDataSet("TForgotPassword", "uspForgotPassword '" + Username + "', '" + Email + "'");

                using (DataTable dtForgot = dsLogin.Tables["TForgotPassword"])
                {
                    if (dtForgot != null && dtForgot.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dtForgot.Rows[0]["Success"]))
                        {

                            // SEND EMAIL INCULUDING PASSCODE ON USER'S EMAIL ADDRESS
                            string sUserEmail = dtForgot.Rows[0]["Email"].ToString();

                            string sPasscode = CShared.GetDecryptString(dtForgot.Rows[0]["Password"].ToString());
                            string sEmailBody = "<p>Greetings, <p>" +
                                                "<p>This is your passcode: <b> " + sPasscode + "</b> for Ornament Application.<p>" +
                                                "<p>Please use this passcode and login with Ornament app!<p>" +
                                                "<p>Regards,</p>" +
                                                "<p>Invisible Fiction.</p>";

                            //SEND EMAIL WITH NEW PASSWORD
                            oDBShared.SendEmail("Forgot Password|" + sUserEmail + "", "", "", "Your Passcode For Ornament App.", sEmailBody);

                            oResult.Success = Convert.ToBoolean(dtForgot.Rows[0]["Success"]);
                            oResult.WasSuccessful = Convert.ToInt32(dtForgot.Rows[0]["Success"]);
                            oResult.Exception = dtForgot.Rows[0]["ErrorSuccessMsg"].ToString();
                        }
                        else
                        {
                            oResult.Success = Convert.ToBoolean(dtForgot.Rows[0]["Success"]);
                            oResult.WasSuccessful = Convert.ToInt32(dtForgot.Rows[0]["Success"]);
                            oResult.Exception = dtForgot.Rows[0]["ErrorSuccessMsg"].ToString();
                        }
                    }
                }
                return oResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public static CSQLResult Registration(RegistrationModel oRM)
        //{
        //    CSQLResult oResult = new CSQLResult();

        //    try
        //    {
        //        string spParameter = "'" + oRM.SponsorKey + "','" +
        //                            CGlobal.oDBShared.GenerateOrganizationUserUniqueID(oRM.Username) + "','" +
        //                            oRM.Firstname + "','" +
        //                            oRM.Lastname + "','" +
        //                            oRM.Email + "','" +
        //                            oRM.CountryCodeMobile + "','" +
        //                            oRM.Mobile + "'," +
        //                            oRM.Gender + "," +
        //                            oRM.Designation + ",'" +
        //                            oRM.DateOfBirth + "','" +
        //                            oRM.Username + "','" +
        //                            CShared.GetEncryptString(oRM.Password) + "'," + 
        //                            CGlobal.ModifiedSourceCode;

        //        DataSet dsRegister = CGlobal.oDBShared.GetDataSet("TRegister", "uspOrganizationUserRegisterBySponsor_Web " + spParameter);

        //        using (DataTable dtRegister = dsRegister.Tables["TRegister"])
        //        {
        //            if (dtRegister != null && dtRegister.Rows.Count > 0)
        //            {
        //                oResult.Success = Convert.ToBoolean(dtRegister.Rows[0]["Success"]);
        //                oResult.Exception = dtRegister.Rows[0]["ErrorSuccessMsg"].ToString();
        //            }
        //        }

        //        return oResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public static bool ContactUs(ContactUsModel oCUM)
        //{
        //    try
        //    {
        //        string sEmailBody = "<p>Greetings, </p>" +
        //                            "<p>" + oCUM.Firstname + " " +  oCUM.Lastname + " will contact us using ContactUs form of ReplicantWeb. </p>" +
        //                            "<p>Other Information: <p>" +
        //                            "<p><b>Email: </b>" + oCUM.Email + "</p>" +
        //                            "<p><b>Comment: </b>" + oCUM.Comment + "</p>" +
        //                            "<p>Regards,</p>" +
        //                            "<p>Replicant.</p>";

        //        //SEND EMAIL ON REPLICANT CONTACT EMAIL
        //        //CGlobal.oDBShared.SendEmail("ContactUs|" + CGlobal.ReplicantContactEmail + "", "", "", "ReplicantWeb - ContactUs Form.", sEmailBody);

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public static CSQLResult InviteFriend(InviteFriendModel oIFM)
        //{
        //    try
        //    {
        //        CSQLResult oResult = new CSQLResult();

        //        string spParameter = "" + oIFM.SponsorID + ",'" +
        //                            oIFM.SponsorKey + "','" +
        //                            oIFM.Firstname + "','" +
        //                            oIFM.Lastname + "','" +
        //                            oIFM.Email + "','" +
        //                            oIFM.CountryCodeMobile + "','" +
        //                            oIFM.Mobile + "','" +
        //                            oIFM.isOrganization + "'," +                                    
        //                            CGlobal.ModifiedSourceCode;

        //        DataSet dsInvite = CGlobal.oDBShared.GetDataSet("TInvite", "uspInviteFriend_Web " + spParameter);

        //        using (DataTable dtRegister = dsInvite.Tables["TInvite"])
        //        {
        //            if (dtRegister != null && dtRegister.Rows.Count > 0)
        //            {
        //                oResult.Success = Convert.ToBoolean(dtRegister.Rows[0]["Success"]);
        //                oResult.Exception = dtRegister.Rows[0]["ErrorSuccessMsg"].ToString();
        //            }
        //        }

        //        if (oResult.Success)
        //        {
        //            if (oIFM.isOrganization)
        //            {
        //                bool isSuccess = CFActivity.OrganizationActivitySave(6, oIFM.SponsorID, "Send New Invitation To Oganization User: " + oIFM.Firstname + " " + oIFM.Lastname);
        //            }

        //            string sSubject = "Replicant Mobile Application";
        //            string sInvitationText = oIFM.InvitationText + "- Use Replicant Mobile App to manage all your organization and personal work. The application will help you find out progress and," +
        //                                "efforts you have made for the tasks. Download the application today from https://googleplay.com " +
        //                                "Use the following information to register and get access to the application: " + oIFM.SponsorKey + "";                   
        //            string sTO = (oIFM.Firstname + " " + oIFM.Lastname + "|" + oIFM.Email);

        //            //SEND EMAIL TO INVITE PERSON-USER
        //            CGlobal.oDBShared.SendEmail(sTO, "", "", sSubject, sInvitationText);
        //        }
                
        //        return oResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public static CSQLResult ContactAdmin(ContactAdminModel oCAM, int ModifiedBy)
        //{
        //    try
        //    {
        //        CSQLResult oResult = new CSQLResult();

        //        string spParameter = "'" + oCAM.Firstname + "','" +
        //                            oCAM.Lastname + "','" +
        //                            oCAM.Email + "','" +
        //                            oCAM.Comment + "'," +
        //                            ModifiedBy;

        //        DataSet dsInvite = CGlobal.oDBShared.GetDataSet("TContact", "uspContactAdmin_Web " + spParameter);

        //        using (DataTable dtRegister = dsInvite.Tables["TContact"])
        //        {
        //            if (dtRegister != null && dtRegister.Rows.Count > 0)
        //            {
        //                oResult.Success = Convert.ToBoolean(dtRegister.Rows[0]["Success"]);
        //                oResult.Exception = dtRegister.Rows[0]["ErrorSuccessMsg"].ToString();
        //            }
        //        }
                
        //        if (oResult.Success)
        //        {
        //            string sEmail = ConfigurationManager.AppSettings["ReportEmail"].ToString();
        //            string scallbackUrl = "http://replicantweb.com/ReplicantAdmin/Home/Login";
        //            string sEmailBody = "<p>Hello, <p>" +
        //                                   "<p>One of the Organization user '" + oCAM.Firstname + " " + oCAM.Lastname + "' has sent you a message using Contact Administrator Screen.<p>" +
        //                                   "<p>Please <a href=\"" + scallbackUrl + "\">Login</a> to Replicant Administrator web site for more information.<p>" +
        //                                   "<p><b>Regards,</b></p>" +
        //                                   "<p><b>Replicant</b></p>";

        //            //SEND EMAIL
        //            CGlobal.oDBShared.SendEmail(sEmail + "", "", "", "Subject: Replicant - New Message from Organization", sEmailBody);

        //            //SAVE ORGANIZATION ACTIVITY - 14 Contact Administrator
        //            bool isSuccess = CFActivity.OrganizationActivitySave(14, ModifiedBy, "Contact Administrator: " + sEmail);
        //        }

        //        return oResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}        
    }
}