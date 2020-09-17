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
    public class BranchService : IBranchService
    {
        private readonly ISqlService _sqlService;
        public BranchService(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public List<CBranch> BranchDetailList(int CompanyID)
        {
            List<CBranch> oResult = new List<CBranch>();
            try
            {
                // SELECT THE INFORMATION FROM THE STORE-PROCEDURE AND SET INFORMATION TO THE DATASET
                DataSet dsBranchInfo = _sqlService.GetDataSet("TBranchInfo", "uspBranchInfoGet " + CompanyID + ", 0");

                using (DataTable dtBranchInfo = dsBranchInfo.Tables["TBranchInfo"])
                {
                    if (dtBranchInfo.Rows.Count > 0)
                    {
                        for (int iRow = 0; iRow <= dtBranchInfo.Rows.Count - 1; iRow++)
                        {
                            // SET THE DATASET INFORMATION TO THE RETURN VALUE
                            oResult.Add(new CBranch()
                            {
                                BranchID = Convert.ToInt32(dtBranchInfo.Rows[iRow]["BranchID"].ToString()),
                                CompanyID = Convert.ToInt32(dtBranchInfo.Rows[iRow]["CompanyID"].ToString()),
                                Name = dtBranchInfo.Rows[iRow]["Name"].ToString(),
                                Address = dtBranchInfo.Rows[iRow]["Address"].ToString(),
                                Email = dtBranchInfo.Rows[iRow]["Email"].ToString(),
                                Location = dtBranchInfo.Rows[iRow]["Location"].ToString(),
                                Mobile = dtBranchInfo.Rows[iRow]["Mobile"].ToString(),
                                Status = dtBranchInfo.Rows[iRow]["Status"].ToString(),
                                LastLogged = Convert.ToDateTime(dtBranchInfo.Rows[iRow]["LastLogged"].ToString()),
                                LoginBeforeLastLogged = Convert.ToDateTime(dtBranchInfo.Rows[iRow]["LoginBeforeLastLogged"].ToString())
                            });
                        }
                    }
                    return oResult;
                }
            }
            catch (Exception ex)
            {
                oResult.Add(new CBranch()
                {
                    Success = false,
                    WasSuccessful = 0,
                    Exception = ex.Message
                });
                return oResult;
            }
        }

        public CBranch BranchDetailGetById(int BranchID)
        {
            CBranch oResult = new CBranch();
            try
            {
                string spParameter = 0 + ", "
                                    + BranchID;

                // SELECT THE INFORMATION FROM THE STORE-PROCEDURE AND SET INFORMATION TO THE DATASET
                DataSet dsBranchInfo = _sqlService.GetDataSet("TBranchInfo", "uspBranchInfoGet " + spParameter);

                using (DataTable dtBranchInfo = dsBranchInfo.Tables["TBranchInfo"])
                {
                    if (dtBranchInfo.Rows.Count > 0 && BranchID > 0)
                    {
                        oResult.BranchID = Convert.ToInt32(dtBranchInfo.Rows[0]["BranchID"].ToString());
                        oResult.CompanyID = Convert.ToInt32(dtBranchInfo.Rows[0]["CompanyID"].ToString());
                        oResult.Name = dtBranchInfo.Rows[0]["Name"].ToString();
                        oResult.Address = dtBranchInfo.Rows[0]["Address"].ToString();
                        oResult.Email = dtBranchInfo.Rows[0]["Email"].ToString();
                        oResult.Location = dtBranchInfo.Rows[0]["Location"].ToString();
                        oResult.Mobile = dtBranchInfo.Rows[0]["Mobile"].ToString();
                        oResult.Username = dtBranchInfo.Rows[0]["Username"].ToString();
                        oResult.Password = "xxxxxxxxxx";
                        oResult.Status = dtBranchInfo.Rows[0]["Status"].ToString();
                        oResult.LastLogged = Convert.ToDateTime(dtBranchInfo.Rows[0]["LastLogged"].ToString());
                        oResult.LoginBeforeLastLogged = Convert.ToDateTime(dtBranchInfo.Rows[0]["LoginBeforeLastLogged"].ToString());
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
                oResult.Success = false;
                oResult.WasSuccessful = 0;
                oResult.Exception = ex.Message;
                return oResult;
            }
        }

        public CSQLResult BranchDetailRemove(int id)
        {
            CSQLResult oResult = new CSQLResult();
            try
            {
                DataSet dsBranchInfo = _sqlService.GetDataSet("TBranchInfo", "uspBranchDelete " + id);
                using (DataTable dtBranchInfo = dsBranchInfo.Tables["TBranchInfo"])
                {
                    if (dtBranchInfo != null && dtBranchInfo.Rows.Count > 0)
                    {
                        oResult.Success = Convert.ToBoolean(dtBranchInfo.Rows[0]["IsSuccess"]);
                        oResult.Exception = dtBranchInfo.Rows[0]["ErrorSuccessMsg"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                oResult.Success = false;
                oResult.WasSuccessful = 0;
                oResult.Exception = ex.Message;
            }
            return oResult;
        }
    }
}
