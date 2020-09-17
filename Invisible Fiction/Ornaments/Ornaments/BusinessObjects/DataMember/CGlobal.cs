using Ornaments.Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Ornaments
{
    public static class CGlobal
    {
        public static CShared oDBShared = new CShared();
        public static  string dPath = ConfigurationManager.AppSettings["dpath"].ToString();
        public static string dPathRemoveString = ConfigurationManager.AppSettings["dPathRemoveString"].ToString();

        public static int ModifiedSourceCode = 1; // Convert.ToInt32(ConfigurationManager.AppSettings["WebApp"].ToString());
        //public static string ReplicantContactEmail = ConfigurationManager.AppSettings["ContactUsEmail"].ToString();

        //public static int ddlDefaultDesignation = Convert.ToInt32(ConfigurationManager.AppSettings["ddlDefaultDesignation"].ToString());
        //public static string DefaultOrgUserPassword = ConfigurationManager.AppSettings["DefaultOrgUserPassword"].ToString();

        //APPLICATION USER TYPE CODE : 1 - Organization Master Account, 2 - Organization User
        public static int ModifiedTypeCode_Organization = 1;
        public static int ModifiedTypeCode_OrgUser = 2;

        // DEFAULT ORGANIZATION IMAGE PARAMETER
        //public static int OrgImgCode = Convert.ToInt32(ConfigurationManager.AppSettings["OrgImgCode"].ToString());
        //public static string DefaultOrgImg = ConfigurationManager.AppSettings["DefaultOrgImg"].ToString();
        //public static string DirNameOrgImgSave = ConfigurationManager.AppSettings["DirNameOrgImgSave"].ToString();

        // DEFAULT ORGANIZATION PROJECT IMAGE PARAMETER
        //public static int OrgPrjImgCode = Convert.ToInt32(ConfigurationManager.AppSettings["OrgPrjImgCode"].ToString());
        //public static string DefaultOrgPrjImg = ConfigurationManager.AppSettings["DefaultOrgPrjImg"].ToString();
        public static string DirNameCompanyLogoSave = ConfigurationManager.AppSettings["DirNameCompanyLogoSave"].ToString();

        // DEFAULT ORGANIZATION USER IMAGE PARAMETER
        public static int OrgUserImgCode = 5; // Convert.ToInt32(ConfigurationManager.AppSettings["OrgUserImgCode"].ToString());
        public static string DefaultOrgUserImg = ""; // ConfigurationManager.AppSettings["DefaultOrgUserImg"].ToString();
        //public static string DirNameOrgUserImgSave = ConfigurationManager.AppSettings["DirNameOrgUserImgSave"].ToString();

        // DEFAULT MESSAGE CENTER SETTINGS
        //public static string LoginUsername = ConfigurationManager.AppSettings["LoginUsername"].ToString();
        //public static string LoginPassword = ConfigurationManager.AppSettings["LoginPassword"].ToString();

        // DEFAULT USERNAME AND PASSWORD
        //public static string DirNameMsgCommunication = ConfigurationManager.AppSettings["DirNameMsgCommunication"].ToString();
        //public static string DirNameMsgVideoSave = ConfigurationManager.AppSettings["DirNameMsgVideoSave"].ToString();
        //public static string DirNameMsgImageSave = ConfigurationManager.AppSettings["DirNameMsgImageSave"].ToString();
        //public static string DirNameMsgFileSave = ConfigurationManager.AppSettings["DirNameMsgFileSave"].ToString();

        // DEFAULT TEMPLATE IMAGE PARAMETERS
        //public static int TemplateImgCode = Convert.ToInt32(ConfigurationManager.AppSettings["TemplateImgCode"].ToString());
        //public static string DefaultTemplateImg = ConfigurationManager.AppSettings["DefaultTemplateImg"].ToString();
        //public static string DirNameTemplateImgSave = ConfigurationManager.AppSettings["DirNameTemplateImgSave"].ToString();

        // DEFAULT CHOP STRING LENGHT PARAMETERS
        //public static int DefaultChopLength = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultChopLength"].ToString());
    }

    public class CSQLResult
    {
        public CSQLResult()
        {
            WasSuccessful = 0;
            Exception = "";
            Success = false;
            OtherParameter = "";
            SQLError = "";
        }
        public int WasSuccessful { get; set; }
        public string Exception { get; set; }
        public Boolean Success { get; set; }
        public string OtherParameter { get; set; }
        public string SQLError { get; set; }
    }
}