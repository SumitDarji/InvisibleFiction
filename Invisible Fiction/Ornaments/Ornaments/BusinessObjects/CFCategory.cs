using System;
using System.Collections.Generic;
using System.Data;
using Ornaments.BusinessObjects.Enum;
using Ornaments.Code;
using Ornaments.Models;

namespace Ornaments.BusinessObject
{
    public static class CFCategory
    {
        public static CSQLResult CategoryDetailSave(CategoryModel categoryModel, int ModifiedBy, int ModifiedSourceCode)
        {
            CSQLResult oResult = new CSQLResult();
            try
            {
                CShared oDBShared = new CShared();

                string spParameter = categoryModel.CategoryID + ", '"
                    + categoryModel.Name + "','"
                    + categoryModel.Description + "','"
                    + categoryModel.ImgPath + "',"
                    + ModifiedBy + ","
                    + ModifiedBy + ","
                    + ModifiedSourceCode;

                DataSet dsCategory = oDBShared.GetDataSet("TCategory", "uspCategoryDetailSave " + spParameter);

                using (DataTable dtCategory = dsCategory.Tables["TCategory"])
                {
                    if (dtCategory != null && dtCategory.Rows.Count > 0)
                    {
                        oResult.Success = Convert.ToBoolean(dtCategory.Rows[0]["IsSuccess"]);
                        oResult.Exception = dtCategory.Rows[0]["ErrorSuccessMsg"].ToString();

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

        public static List<CCategory> CategoryDetailList()
        {
            try
            {
                List<CCategory> oResult = new List<CCategory>();
                CShared oDBShared = new CShared();

                // SELECT THE INFORMATION FROM THE STORE-PROCEDURE AND SET INFORMATION TO THE DATASET
                DataSet dsCategoryInfo = oDBShared.GetDataSet("TCategoryInfo", "uspCategoryInfoGet 0");

                using (DataTable dtCategoryInfo = dsCategoryInfo.Tables["TCategoryInfo"])
                {
                    if (dtCategoryInfo.Rows.Count > 0)
                    {
                        for (int iRow = 0; iRow <= dtCategoryInfo.Rows.Count - 1; iRow++)
                        {
                            // SET THE DATASET INFORMATION TO THE RETURN VALUE
                            oResult.Add(new CCategory()
                            {
                                CategoryID = Convert.ToInt32(dtCategoryInfo.Rows[iRow]["CategoryID"].ToString()),
                                Name = dtCategoryInfo.Rows[iRow]["Name"].ToString(),
                                Description = dtCategoryInfo.Rows[iRow]["Description"].ToString(),
                                ImgPath = oDBShared.ImagePathGet(dtCategoryInfo.Rows[iRow]["LogoImage"].ToString(), 0, string.Empty),
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

        public static CCategory CategoryDetailGetById(int CategoryID)
        {
            try
            {
                CCategory oResult = new CCategory();
                CShared oDBShared = new CShared();

                // SELECT THE INFORMATION FROM THE STORE-PROCEDURE AND SET INFORMATION TO THE DATASET
                DataSet dsCategoryInfo = oDBShared.GetDataSet("TCategoryInfo", "uspCategoryInfoGet " + CategoryID);

                using (DataTable dtCategoryInfo = dsCategoryInfo.Tables["TCategoryInfo"])
                {
                    if (dtCategoryInfo.Rows.Count > 0)
                    {
                        oResult.CategoryID = Convert.ToInt32(dtCategoryInfo.Rows[0]["CategoryID"].ToString());
                        oResult.Name = dtCategoryInfo.Rows[0]["Name"].ToString();
                        oResult.Description = dtCategoryInfo.Rows[0]["Description"].ToString();
                        oResult.ImgPath = oDBShared.ImagePathGet(dtCategoryInfo.Rows[0]["LogoImage"].ToString(), 0, string.Empty);
                    }
                    return oResult;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CSQLResult CategoryDetailRemove(int id)
        {
            CSQLResult oResult = new CSQLResult();
            try
            {
                CShared oDBShared = new CShared();
                DataSet dsCategory = oDBShared.GetDataSet("TCategory", "uspCategoryDelete " + id);
                using (DataTable dtCategory = dsCategory.Tables["TCategory"])
                {
                    if (dtCategory != null && dtCategory.Rows.Count > 0)
                    {
                        oResult.Success = Convert.ToBoolean(dtCategory.Rows[0]["IsSuccess"]);
                        oResult.Exception = dtCategory.Rows[0]["ErrorSuccessMsg"].ToString();
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