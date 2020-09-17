using System;
using System.Collections.Generic;
using System.Data;
using Ornaments.BusinessObjects.Enum;
using Ornaments.Code;
using Ornaments.Models;

namespace Ornaments.BusinessObject
{
    public static class CFBranch
    {
        public static CSQLResult BranchDetailSave(BranchModel branchModel, int ModifiedBy, int ModifiedSourceCode)
        {
            CSQLResult oResult = new CSQLResult();
            try
            {
                CShared oDBShared = new CShared();
                string ePassword = CShared.GetEncryptString(branchModel.Password);

                string spParameter = branchModel.BranchID + ", "
                    + branchModel.CompanyID + ",'"
                    + branchModel.Name + "','"
                    + branchModel.Address + "','"
                    + branchModel.Email + "','"
                    + branchModel.Location + "','"
                    + branchModel.Mobile + "','"
                    + branchModel.Username + "','"
                    + ePassword + "',"
                    + ModifiedBy + ","
                    + ModifiedBy + ","
                    + ModifiedSourceCode;

                DataSet dsBranch = oDBShared.GetDataSet("TBranch", "uspBranchDetailSave " + spParameter);

                using (DataTable dtBranch = dsBranch.Tables["TBranch"])
                {
                    if (dtBranch != null && dtBranch.Rows.Count > 0)
                    {
                        oResult.Success = Convert.ToBoolean(dtBranch.Rows[0]["IsSuccess"]);
                        oResult.Exception = dtBranch.Rows[0]["ErrorSuccessMsg"].ToString();

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

        public static List<CBranch> BranchDetailList(int CompanyID, int loginType)
        {
            try
            {
                List<CBranch> oResult = new List<CBranch>();
                CShared oDBShared = new CShared();
                string spParameter = string.Empty;
                if (loginType != Convert.ToInt32(LoginTypeCode.BRNACH_USER)) {
                    spParameter = CompanyID + ", "
                                    + 0;
                } else
                {
                    spParameter = 0 + ", "
                                    + CompanyID;
                }

                // SELECT THE INFORMATION FROM THE STORE-PROCEDURE AND SET INFORMATION TO THE DATASET
                DataSet dsBranchInfo = oDBShared.GetDataSet("TBranchInfo", "uspBranchInfoGet " + spParameter);

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
                throw ex;
            }
        }

        public static CBranch BranchDetailGetById(int BranchID)
        {
            try
            {
                CBranch oResult = new CBranch();
                CShared oDBShared = new CShared();
                string spParameter = 0 + ", "
                                    + BranchID;

                // SELECT THE INFORMATION FROM THE STORE-PROCEDURE AND SET INFORMATION TO THE DATASET
                DataSet dsBranchInfo = oDBShared.GetDataSet("TBranchInfo", "uspBranchInfoGet " + spParameter);

                using (DataTable dtBranchInfo = dsBranchInfo.Tables["TBranchInfo"])
                {
                    if (dtBranchInfo.Rows.Count > 0)
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
                    }
                    return oResult;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CSQLResult BranchDetailRemove(int id)
        {
            CSQLResult oResult = new CSQLResult();
            try
            {
                CShared oDBShared = new CShared();
                DataSet dsBranchInfo = oDBShared.GetDataSet("TBranchInfo", "uspBranchDelete " + id);
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
                //oResult.Success = false;
                //oResult.WasSuccessful = 0;
                //oResult.Exception = ex.Message;
                throw ex;
            }
            return oResult;
        }
    }
}