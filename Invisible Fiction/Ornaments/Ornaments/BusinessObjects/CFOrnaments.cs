using System;
using System.Collections.Generic;
using System.Data;
using Ornaments.BusinessObjects.Enum;
using Ornaments.Code;
using Ornaments.Models;

namespace Ornaments.BusinessObject
{
    public static class CFOrnaments
    {
        public static CSQLResult OrnamentsDetailSave(OrnamentsModel ornamentsModel, int ModifiedBy, int ModifiedSourceCode)
        {
            CSQLResult oResult = new CSQLResult();
            try
            {
                CShared oDBShared = new CShared();
                DataTable dtImage = new DataTable();
                dtImage.Columns.Add("Id", typeof(int));
                dtImage.Columns.Add("ImgPath", typeof(string));

                ornamentsModel.OrnamentsImgPath.ForEach(element =>
                {
                    dtImage.Rows.Add(new Object[] { Convert.ToInt32(element.IndexOf(element)) + 1, element });
                });

                string spParameter = ornamentsModel.OrnamentID + "," 
                    + ornamentsModel.CategoryID + ", "
                    + ornamentsModel.OrnamentPositionID + ", '"
                    + ornamentsModel.Name + "','"
                    + ornamentsModel.Description + "','"
                    + ornamentsModel.Weight + "',"
                    + ornamentsModel.Cost + ","
                    + dtImage + ","
                    + ModifiedBy + ","
                    + ModifiedBy + ","
                    + ModifiedSourceCode;
                DataSet dsOrnaments = oDBShared.getSPDataSet("uspOrnamentDetailSave", "@OrnamentID", ornamentsModel.OrnamentID, "@CategoryID", ornamentsModel.CategoryID, "@PositionID", ornamentsModel.OrnamentPositionID,
                                                        "@Name", ornamentsModel.Name, "@Description", ornamentsModel.Description, "@Weight", ornamentsModel.Weight,
                                                        "@Cost", ornamentsModel.Cost, "@LogoImgPath", dtImage, "@CreatedBy", ModifiedBy,
                                                        "@ModifiedBy", ModifiedBy, "@ModifiedSourceCode", ModifiedSourceCode);

                using (DataTable dtOrnaments = dsOrnaments.Tables["Table"])
                {
                    if (dtOrnaments != null && dtOrnaments.Rows.Count > 0)
                    {
                        oResult.Success = Convert.ToBoolean(dtOrnaments.Rows[0]["IsSuccess"]);
                        oResult.Exception = dtOrnaments.Rows[0]["ErrorSuccessMsg"].ToString();

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

        public static List<COrnaments> OrnamentsDetailList()
        {
            try
            {
                List<COrnaments> oResult = new List<COrnaments>();
                CShared oDBShared = new CShared();

                // SELECT THE INFORMATION FROM THE STORE-PROCEDURE AND SET INFORMATION TO THE DATASET
                DataSet dsOrnamentsInfo = oDBShared.GetDataSet("TOrnamentsInfo, TImages", "uspOrnamentsInfoGet 0");

                using (DataTable dtOrnamentsInfo = dsOrnamentsInfo.Tables["TOrnamentsInfo"])
                {
                    if (dtOrnamentsInfo.Rows.Count > 0)
                    {
                        for (int iRow = 0; iRow <= dtOrnamentsInfo.Rows.Count - 1; iRow++)
                        {
                            
                            // SET THE DATASET INFORMATION TO THE RETURN VALUE
                            oResult.Add(new COrnaments()
                            {
                                CategoryID = Convert.ToInt32(dtOrnamentsInfo.Rows[iRow]["CategoryID"].ToString()),
                                OrnamentPositionID = Convert.ToInt32(dtOrnamentsInfo.Rows[iRow]["PositionID"].ToString()),
                                OrnamentID = Convert.ToInt32(dtOrnamentsInfo.Rows[iRow]["OrnamentID"].ToString()),
                                Name = dtOrnamentsInfo.Rows[iRow]["Name"].ToString(),
                                CategoryName = dtOrnamentsInfo.Rows[iRow]["CategoryName"].ToString(),
                                Description = dtOrnamentsInfo.Rows[iRow]["Description"].ToString(),
                                OrnamentPositionName = dtOrnamentsInfo.Rows[iRow]["PositionName"].ToString(),
                                Weight = dtOrnamentsInfo.Rows[iRow]["Weight"].ToString(),
                                Cost = Convert.ToDecimal(dtOrnamentsInfo.Rows[iRow]["Cost"].ToString())
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

        public static COrnaments OrnamentsDetailGetById(int OrnamentsID)
        {
            try
            {
                COrnaments oResult = new COrnaments();
                CShared oDBShared = new CShared();

                // SELECT THE INFORMATION FROM THE STORE-PROCEDURE AND SET INFORMATION TO THE DATASET
                DataSet dsOrnamentsInfo = oDBShared.GetDataSet("TOrnamentsInfo", "uspOrnamentsInfoGet " + OrnamentsID);

                using (DataTable dtOrnamentsInfo = dsOrnamentsInfo.Tables["TOrnamentsInfo"])
                {
                    if (dtOrnamentsInfo.Rows.Count > 0)
                    {
                        oResult.OrnamentID = Convert.ToInt32(dtOrnamentsInfo.Rows[0]["OrnamentID"].ToString());
                        oResult.OrnamentPositionID = Convert.ToInt32(dtOrnamentsInfo.Rows[0]["PositionID"].ToString());
                        oResult.CategoryID = Convert.ToInt32(dtOrnamentsInfo.Rows[0]["CategoryID"].ToString());
                        oResult.Name = dtOrnamentsInfo.Rows[0]["Name"].ToString();
                        oResult.Description = dtOrnamentsInfo.Rows[0]["Description"].ToString();
                        oResult.OrnamentPositionName = dtOrnamentsInfo.Rows[0]["PositionName"].ToString();
                        oResult.Weight = dtOrnamentsInfo.Rows[0]["Weight"].ToString();
                        oResult.Cost = Convert.ToDecimal(dtOrnamentsInfo.Rows[0]["Cost"].ToString());
                    }
                    return oResult;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CSQLResult OrnamentsDetailRemove(int id)
        {
            CSQLResult oResult = new CSQLResult();
            try
            {
                CShared oDBShared = new CShared();
                DataSet dsOrnamentsInfo = oDBShared.GetDataSet("TOrnamentsInfo", "uspOrnamentsDelete " + id);
                using (DataTable dtOrnamentsInfo = dsOrnamentsInfo.Tables["TOrnamentsInfo"])
                {
                    if (dtOrnamentsInfo != null && dtOrnamentsInfo.Rows.Count > 0)
                    {
                        oResult.Success = Convert.ToBoolean(dtOrnamentsInfo.Rows[0]["IsSuccess"]);
                        oResult.Exception = dtOrnamentsInfo.Rows[0]["ErrorSuccessMsg"].ToString();
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