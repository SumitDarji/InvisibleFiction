using Ornaments.BusinessObject;
using Ornaments.Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Ornaments.BusinessObject
{
    public static class CFCode
    {
        public static List<CCode> getCodeByCodeType(int CodeTypeId)
        {
            try
            {
                CShared oDBShared = new CShared();
                List<CCode> oCode = new List<CCode>();
                DataSet dsCode = oDBShared.GetDataSet("TCode", "uspQueryGet " + CodeTypeId);

                using (DataTable dtCode = dsCode.Tables["TCode"])
                {
                    if (dtCode != null && dtCode.Rows.Count > 0)
                    {
                        oCode = CShared.DataTableToList<CCode>(dtCode);
                    }
                }

                return oCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool ValiddateCountryCode(int CountryCode)
        {
            try
            {
                List<CCode> oCode = new List<CCode>();
                DataSet dsCode = CGlobal.oDBShared.GetDataSet("TCode", "uspQueryGet_Web 'ValiddateCountryCode'," + CountryCode);

                using (DataTable dtCode = dsCode.Tables["TCode"])
                {
                    if (dtCode != null && dtCode.Rows.Count > 0)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool ValiddateUsrname(string Username)
        {
            try
            {
                List<CCode> oCode = new List<CCode>();
                DataSet dsCode = CGlobal.oDBShared.GetDataSet("TCode", "uspQueryGet_Web 'ValiddateUsername','" + Username + "'");

                using (DataTable dtCode = dsCode.Tables["TCode"])
                {
                    if (dtCode != null && dtCode.Rows.Count > 0)
                    {
                        return !Convert.ToBoolean(dsCode.Tables["TCode"].Rows[0][0]);
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool ValiddateEmail(string Email)
        {
            try
            {
                List<CCode> oCode = new List<CCode>();
                DataSet dsCode = CGlobal.oDBShared.GetDataSet("TCode", "uspQueryGet_Web 'ValiddateEmail','" + Email + "'");

                using (DataTable dtCode = dsCode.Tables["TCode"])
                {
                    if (dtCode != null && dtCode.Rows.Count > 0)
                    {
                        return !Convert.ToBoolean(dsCode.Tables["TCode"].Rows[0][0]);
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public static List<CCountry> GetCountry()
        //{
        //    try
        //    {
        //        List<CCountry> oCountry = new List<CCountry>();
        //        DataSet dsCode = CGlobal.oDBShared.GetDataSet("TCountry", "uspQueryGet_Web 'GetCountry'");

        //        using (DataTable dtCode = dsCode.Tables["TCountry"])
        //        {
        //            if (dtCode != null && dtCode.Rows.Count > 0)
        //            {
        //                oCountry = CShared.DataTableToList<CCountry>(dtCode);
        //            }
        //        }

        //        return oCountry;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public static List<CCompany> GetComapnyList(int AdminUserId)
        {
            try
            {
                List<CCompany> oCompany = new List<CCompany>();
                CShared oDBShared = new CShared();

                DataSet dsCompany = oDBShared.GetDataSet("TCompanyInfo", "uspCompanyInfoGet 0");

                using (DataTable dtCompany = dsCompany.Tables["TCompanyInfo"])
                {
                    if (dtCompany != null && dtCompany.Rows.Count > 0)
                    {
                        oCompany = CShared.DataTableToList<CCompany>(dtCompany);
                    }
                }

                return oCompany;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        ///* FOR TEMPLATE ITEM TASK GET*/
        //public static List<CTemplateActionItem> GetProjectTemplateTask(int TemplateID)
        //{
        //    try
        //    {
        //        List<CTemplateActionItem> oTempTask = new List<CTemplateActionItem>();

        //        DataSet dsTempTask = CGlobal.oDBShared.GetDataSet("TTemplateActionItem", "uspQueryGet_Web 'TemplateItems', " + TemplateID + "");

        //        using (DataTable dtTempTask = dsTempTask.Tables["TTemplateActionItem"])
        //        {
        //            if (dtTempTask != null && dtTempTask.Rows.Count > 0)
        //            {
        //                oTempTask = CShared.DataTableToList<CTemplateActionItem>(dtTempTask);
        //            }
        //        }
        //        return oTempTask;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public static string ShowBreadCrumbs(string sValue)
        {
            /* string  sBreadCrumbs = string.Empty;
            if (!(String.IsNullOrEmpty(sValue)))
            {
                string[] arrPair = sValue.Split(';');
                string[] arrKeyValue;
                int iPair = 0;
                sBreadCrumbs = "<ol class=\"breadcrumb pl-0\">";
                for (iPair = 0; iPair <= arrPair.Length - 1; iPair++)
                {
                    arrKeyValue = arrPair[iPair].ToString().Split('|');
                    if (arrKeyValue.Length == 2)
                    {
                        sBreadCrumbs = sBreadCrumbs  + "<li class=\"breadcrumb-item\"> " + "<a href=\"" + arrKeyValue[0] + "\">" + arrKeyValue[1] + "</a></li>";
                    }
                    else
                    {
                        sBreadCrumbs = sBreadCrumbs  + "<li class=\"breadcrumb-item active\"> " + arrKeyValue[0] + "</li>";
                    }
                }
                sBreadCrumbs = sBreadCrumbs + "</ol>";
            }*/

            string  sBreadCrumbs = string.Empty;
            if (!(String.IsNullOrEmpty(sValue)))
            {
                string[] arrPair = sValue.Split(';');
                string[] arrKeyValue;
                int iPair = 0;
                //sBreadCrumbs = "<div class=\"mt-2 mb-1\"><div id=\"bc1\" class=\"myBreadcrumb\">";
                sBreadCrumbs = "<div><div id=\"bc1\" class=\"myBreadcrumb\">";
                for (iPair = 0; iPair <= arrPair.Length - 1; iPair++)
                {
                    arrKeyValue = arrPair[iPair].ToString().Split('|');
                    if (arrKeyValue.Length == 2)
                    {
                        if (iPair == 0)
                        {
                            sBreadCrumbs = sBreadCrumbs + "<a class=\"active\" href=\"" + arrKeyValue[0] + "\"><i class=\"fa fa-home fa-2x\"></i></a>";
                            sBreadCrumbs = sBreadCrumbs + (arrPair.Length > 6 ? "<div>...</div>" : "");
                        }
                        else
                        {
                            sBreadCrumbs = sBreadCrumbs + "<a href=\"" + arrKeyValue[0] + "\"><div>" + arrKeyValue[1] + "</div></a>";
                        }
                    }
                    else
                    {
                        sBreadCrumbs = sBreadCrumbs  + "<div style=\"color: #00a652; font-weight:bold;\">" + arrKeyValue[0] + "</div>";
                    }
                }
                sBreadCrumbs = sBreadCrumbs + "</div></div>";
            }
            return sBreadCrumbs;
        }
    }
}