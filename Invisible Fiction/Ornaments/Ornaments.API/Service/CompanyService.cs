using Ornaments.API.Code;
using Ornaments.API.Service.Interface;
using Ornaments.BusinessObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Ornaments.API.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly ISqlService _sqlService;
        public CompanyService(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public List<CCompany> CompanyDetailList()
        {
            List<CCompany> oResult = new List<CCompany>();
            try
            {
                // SELECT THE INFORMATION FROM THE STORE-PROCEDURE AND SET INFORMATION TO THE DATASET
                DataSet dsCompanyInfo = _sqlService.GetDataSet("TCompanyInfo", "uspCompanyInfoGet 0");

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
                                LogoImgPath = _sqlService.ImagePathGet(dtCompanyInfo.Rows[iRow]["LogoImage"].ToString(), 0, string.Empty),
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
                oResult.Add(new CCompany()
                {
                    Success = false,
                    WasSuccessful = 0,
                    Exception = ex.Message
                });
                return oResult;
            }
        }

        public CCompany CompanyDetailGetById(int CompanyId)
        {
            try
            {
                CCompany oResult = new CCompany();

                // SELECT THE INFORMATION FROM THE STORE-PROCEDURE AND SET INFORMATION TO THE DATASET
                DataSet dsCompanyInfo = _sqlService.GetDataSet("TCompanyInfo", "uspCompanyInfoGet " + CompanyId);

                using (DataTable dtCompanyInfo = dsCompanyInfo.Tables["TCompanyInfo"])
                {
                    if (dtCompanyInfo.Rows.Count > 0 && CompanyId > 0)
                    {
                        oResult.CompanyID = Convert.ToInt64(dtCompanyInfo.Rows[0]["CompanyID"].ToString());
                        oResult.Name = dtCompanyInfo.Rows[0]["Name"].ToString();
                        oResult.Address = dtCompanyInfo.Rows[0]["Address"].ToString();
                        oResult.Email = dtCompanyInfo.Rows[0]["Email"].ToString();
                        oResult.Mobile = dtCompanyInfo.Rows[0]["Mobile"].ToString();
                        oResult.LogoImgPath = _sqlService.ImagePathGet(dtCompanyInfo.Rows[0]["LogoImage"].ToString(), 0, string.Empty);
                        oResult.MasterUsername = dtCompanyInfo.Rows[0]["Username"].ToString();
                        oResult.MasterPassword = "xxxxxxxxxx";
                        oResult.Status = dtCompanyInfo.Rows[0]["Status"].ToString();
                        oResult.LastLogged = Convert.ToDateTime(dtCompanyInfo.Rows[0]["LastLogged"].ToString());
                        oResult.LoginBeforeLastLogged = Convert.ToDateTime(dtCompanyInfo.Rows[0]["LoginBeforeLastLogged"].ToString());
                        oResult.Success = true;
                        oResult.WasSuccessful = 1;
                    }
                    else
                    {
                        oResult.Success = false;
                        oResult.WasSuccessful = 0;
                        oResult.Exception = "No Record Found!";
                    }
                    return oResult;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CSQLResult CompanyDetailRemove(int id)
        {
            CSQLResult oResult = new CSQLResult();
            try
            {
               // CShared oDBShared = new CShared();
                DataSet dsCompanyInfo = _sqlService.GetDataSet("TCompany", "uspCompanyDelete " + id);
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
