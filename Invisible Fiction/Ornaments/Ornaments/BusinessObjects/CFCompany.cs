using System;
using System.Collections.Generic;
using System.Data;
using Ornaments.Code;
using Ornaments.Models;

namespace Ornaments.BusinessObject
{
    public static class CFCompany
    {
        public static CSQLResult CompanyDetailSave(CompanyModel companyModel, int ModifiedBy)
        {
            CSQLResult oResult = new CSQLResult();
            try
            {
                CShared oDBShared = new CShared();
                string ePassword = CShared.GetEncryptString(companyModel.MasterPassword);

                string spParameter = companyModel.CompanyID + ", '"
                    + companyModel.Name + "','"
                    + companyModel.Address + "','"
                    + companyModel.Email + "','"
                    + companyModel.Mobile + "','"
                    + companyModel.LogoImgPath + "','"
                    + companyModel.MasterUsername + "','"
                    + ePassword + "',"
                    + ModifiedBy + ","
                    + ModifiedBy;

                DataSet dsCompany = oDBShared.GetDataSet("TCompany", "uspCompanySave " + spParameter);

                using (DataTable dtCompany = dsCompany.Tables["TCompany"])
                {
                    if (dtCompany != null && dtCompany.Rows.Count > 0)
                    {
                        oResult.Success = Convert.ToBoolean(dtCompany.Rows[0]["IsSuccess"]);
                        oResult.Exception = dtCompany.Rows[0]["ErrorSuccessMsg"].ToString();

                        if (oResult.Success)
                        {
                            // SET OTHER PARAMETER INFORMATION
                            //oResult.OtherParameter = dtCompany.Rows[0]["OrgPrjID"].ToString();
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

        // SAVE ORGANIZATION PROJECT SEARCH
        public static List<CCompany> CompanyDetailList(int AdminUserId)
        {
            try
            {
                List<CCompany> oResult = new List<CCompany>();
                CShared oDBShared = new CShared();

                // SELECT THE INFORMATION FROM THE STORE-PROCEDURE AND SET INFORMATION TO THE DATASET
                DataSet dsCompanyInfo = oDBShared.GetDataSet("TCompanyInfo", "uspCompanyInfoGet " + 0);

                using (DataTable dtCompanyInfo = dsCompanyInfo.Tables["TCompanyInfo"])
                {
                    if (dtCompanyInfo.Rows.Count > 0)
                    {
                        for (int iRow = 0; iRow <= dtCompanyInfo.Rows.Count - 1; iRow++)
                        {
                            // SET THE DATASET INFORMATION TO THE RETURN VALUE
                            oResult.Add(new CCompany()
                            {
                                CompanyID = Convert.ToInt64(dtCompanyInfo.Rows[iRow]["CompanyID"].ToString()),
                                Name = dtCompanyInfo.Rows[iRow]["Name"].ToString(),
                                Address = dtCompanyInfo.Rows[iRow]["Address"].ToString(),
                                Email = dtCompanyInfo.Rows[iRow]["Email"].ToString(),
                                Mobile = dtCompanyInfo.Rows[iRow]["Mobile"].ToString(),
                                LogoImgPath = oDBShared.ImagePathGet(dtCompanyInfo.Rows[iRow]["LogoImage"].ToString(), 0, string.Empty),
                                Status = dtCompanyInfo.Rows[iRow]["Status"].ToString(),
                                LastLogged = Convert.ToDateTime(dtCompanyInfo.Rows[iRow]["LastLogged"].ToString()),
                                LoginBeforeLastLogged = Convert.ToDateTime(dtCompanyInfo.Rows[iRow]["LoginBeforeLastLogged"].ToString())
                            });
                        }
                    }
                    return oResult;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CCompany CompanyDetailGetById(int CompanyId)
        {
            try
            {
                CCompany oResult = new CCompany();
                CShared oDBShared = new CShared();

                // SELECT THE INFORMATION FROM THE STORE-PROCEDURE AND SET INFORMATION TO THE DATASET
                DataSet dsCompanyInfo = oDBShared.GetDataSet("TCompanyInfo", "uspCompanyInfoGet " + CompanyId);

                using (DataTable dtCompanyInfo = dsCompanyInfo.Tables["TCompanyInfo"])
                {
                    if (dtCompanyInfo.Rows.Count > 0)
                    {
                        oResult.CompanyID = Convert.ToInt64(dtCompanyInfo.Rows[0]["CompanyID"].ToString());
                        oResult.Name = dtCompanyInfo.Rows[0]["Name"].ToString();
                        oResult.Address = dtCompanyInfo.Rows[0]["Address"].ToString();
                        oResult.Email = dtCompanyInfo.Rows[0]["Email"].ToString();
                        oResult.Mobile = dtCompanyInfo.Rows[0]["Mobile"].ToString();
                        oResult.LogoImgPath = oDBShared.ImagePathGet(dtCompanyInfo.Rows[0]["LogoImage"].ToString(), 0, string.Empty);
                        oResult.MasterUsername = dtCompanyInfo.Rows[0]["Username"].ToString();
                        oResult.MasterPassword = "xxxxxxxxxx";
                        oResult.Status = dtCompanyInfo.Rows[0]["Status"].ToString();
                        oResult.LastLogged = Convert.ToDateTime(dtCompanyInfo.Rows[0]["LastLogged"].ToString());
                        oResult.LoginBeforeLastLogged = Convert.ToDateTime(dtCompanyInfo.Rows[0]["LoginBeforeLastLogged"].ToString());
                    }
                    return oResult;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CSQLResult CompanyDetailRemove(int id)
        {
            CSQLResult oResult = new CSQLResult();
            try
            {
                CShared oDBShared = new CShared();
                DataSet dsCompanyInfo = oDBShared.GetDataSet("TCompany", "uspCompanyDelete " + id);
                using (DataTable dtCompanyInfo = dsCompanyInfo.Tables["TCompany"])
                {
                    if (dtCompanyInfo != null && dtCompanyInfo.Rows.Count > 0)
                    {
                        oResult.Success = Convert.ToBoolean(dtCompanyInfo.Rows[0]["IsSuccess"]);
                        oResult.Exception = dtCompanyInfo.Rows[0]["ErrorSuccessMsg"].ToString();
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
    }
}