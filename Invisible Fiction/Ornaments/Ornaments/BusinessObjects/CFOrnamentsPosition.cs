using System;
using System.Collections.Generic;
using System.Data;
using Ornaments.BusinessObjects.Enum;
using Ornaments.Code;
using Ornaments.Models;

namespace Ornaments.BusinessObject
{
    public static class CFOrnamentsPosition
    {
        public static CSQLResult OrnamentsPositionDetailSave(OrnamentsPositionModel ornamentsPositionModel, int ModifiedBy, int ModifiedSourceCode)
        {
            CSQLResult oResult = new CSQLResult();
            try
            {
                CShared oDBShared = new CShared();

                string spParameter = ornamentsPositionModel.OrnamentPositionID + "," 
                    + ornamentsPositionModel.CategoryID + ", '"
                    + ornamentsPositionModel.Name + "','"
                    + ornamentsPositionModel.Description + "','"
                    + ornamentsPositionModel.ImgPath + "',"
                    + ModifiedBy + ","
                    + ModifiedBy + ","
                    + ModifiedSourceCode;

                DataSet dsOrnamentsPosition = oDBShared.GetDataSet("TOrnamentsPosition", "uspPositionDetailSave " + spParameter);

                using (DataTable dtOrnamentsPosition = dsOrnamentsPosition.Tables["TOrnamentsPosition"])
                {
                    if (dtOrnamentsPosition != null && dtOrnamentsPosition.Rows.Count > 0)
                    {
                        oResult.Success = Convert.ToBoolean(dtOrnamentsPosition.Rows[0]["IsSuccess"]);
                        oResult.Exception = dtOrnamentsPosition.Rows[0]["ErrorSuccessMsg"].ToString();

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

        public static List<COrnamentsPosition> OrnamentsPositionDetailList()
        {
            try
            {
                List<COrnamentsPosition> oResult = new List<COrnamentsPosition>();
                CShared oDBShared = new CShared();

                // SELECT THE INFORMATION FROM THE STORE-PROCEDURE AND SET INFORMATION TO THE DATASET
                DataSet dsOrnamentsPositionInfo = oDBShared.GetDataSet("TOrnamentsPositionInfo", "uspOrnamentsPositionInfoGet 0");

                using (DataTable dtOrnamentsPositionInfo = dsOrnamentsPositionInfo.Tables["TOrnamentsPositionInfo"])
                {
                    if (dtOrnamentsPositionInfo.Rows.Count > 0)
                    {
                        for (int iRow = 0; iRow <= dtOrnamentsPositionInfo.Rows.Count - 1; iRow++)
                        {
                            // SET THE DATASET INFORMATION TO THE RETURN VALUE
                            oResult.Add(new COrnamentsPosition()
                            {
                                CategoryID = Convert.ToInt32(dtOrnamentsPositionInfo.Rows[iRow]["CategoryID"].ToString()),
                                OrnamentPositionID = Convert.ToInt32(dtOrnamentsPositionInfo.Rows[iRow]["PositionID"].ToString()),
                                Name = dtOrnamentsPositionInfo.Rows[iRow]["Name"].ToString(),
                                CategoryName = dtOrnamentsPositionInfo.Rows[iRow]["CategoryName"].ToString(),
                                Description = dtOrnamentsPositionInfo.Rows[iRow]["Description"].ToString(),
                                ImgPath = oDBShared.ImagePathGet(dtOrnamentsPositionInfo.Rows[iRow]["LogoImage"].ToString(), 0, string.Empty)
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

        public static COrnamentsPosition OrnamentsPositionDetailGetById(int OrnamentsPositionID)
        {
            try
            {
                COrnamentsPosition oResult = new COrnamentsPosition();
                CShared oDBShared = new CShared();

                // SELECT THE INFORMATION FROM THE STORE-PROCEDURE AND SET INFORMATION TO THE DATASET
                DataSet dsOrnamentsPositionInfo = oDBShared.GetDataSet("TOrnamentsPositionInfo", "uspOrnamentsPositionInfoGet " + OrnamentsPositionID);

                using (DataTable dtOrnamentsPositionInfo = dsOrnamentsPositionInfo.Tables["TOrnamentsPositionInfo"])
                {
                    if (dtOrnamentsPositionInfo.Rows.Count > 0)
                    {
                        oResult.OrnamentPositionID = Convert.ToInt32(dtOrnamentsPositionInfo.Rows[0]["PositionID"].ToString());
                        oResult.CategoryID = Convert.ToInt32(dtOrnamentsPositionInfo.Rows[0]["CategoryID"].ToString());
                        oResult.Name = dtOrnamentsPositionInfo.Rows[0]["Name"].ToString();
                        oResult.Description = dtOrnamentsPositionInfo.Rows[0]["Description"].ToString();
                        oResult.ImgPath = oDBShared.ImagePathGet(dtOrnamentsPositionInfo.Rows[0]["LogoImage"].ToString(), 0, string.Empty);
                    }
                    return oResult;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CSQLResult OrnamentsPositionDetailRemove(int id)
        {
            CSQLResult oResult = new CSQLResult();
            try
            {
                CShared oDBShared = new CShared();
                DataSet dsOrnamentsPositionInfo = oDBShared.GetDataSet("TOrnamentsPositionInfo", "uspOrnamentPositionDelete " + id);
                using (DataTable dtOrnamentsPositionInfo = dsOrnamentsPositionInfo.Tables["TOrnamentsPositionInfo"])
                {
                    if (dtOrnamentsPositionInfo != null && dtOrnamentsPositionInfo.Rows.Count > 0)
                    {
                        oResult.Success = Convert.ToBoolean(dtOrnamentsPositionInfo.Rows[0]["IsSuccess"]);
                        oResult.Exception = dtOrnamentsPositionInfo.Rows[0]["ErrorSuccessMsg"].ToString();
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