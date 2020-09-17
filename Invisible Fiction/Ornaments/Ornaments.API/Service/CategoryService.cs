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
    public class CategoryService : ICategoryService
    {
        private readonly ISqlService _sqlService;
        public CategoryService(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }
        public List<CCategory> CategoryDetailList()
        {
            List<CCategory> oResult = new List<CCategory>();
            try
            {
                // SELECT THE INFORMATION FROM THE STORE-PROCEDURE AND SET INFORMATION TO THE DATASET
                DataSet dsCategoryInfo = _sqlService.GetDataSet("TCategoryInfo", "uspCategoryInfoGet 0");

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
                                ImgPath = _sqlService.ImagePathGet(dtCategoryInfo.Rows[iRow]["LogoImage"].ToString(), 0, string.Empty)
                            });
                        }
                    }
                    return oResult;
                }
            }
            catch (Exception ex)
            {
                oResult.Add(new CCategory()
                {
                    Success = false,
                    WasSuccessful = 0,
                    Exception = ex.Message
                });
                return oResult;
            }
        }

        public  CCategory CategoryDetailGetById(int CategoryID)
        {
            CCategory oResult = new CCategory();
            try
            {
                // SELECT THE INFORMATION FROM THE STORE-PROCEDURE AND SET INFORMATION TO THE DATASET
                DataSet dsCategoryInfo = _sqlService.GetDataSet("TCategoryInfo", "uspCategoryInfoGet " + CategoryID);

                using (DataTable dtCategoryInfo = dsCategoryInfo.Tables["TCategoryInfo"])
                {
                    if (dtCategoryInfo.Rows.Count > 0 && CategoryID > 0)
                    {
                        oResult.CategoryID = Convert.ToInt32(dtCategoryInfo.Rows[0]["CategoryID"].ToString());
                        oResult.Name = dtCategoryInfo.Rows[0]["Name"].ToString();
                        oResult.Description = dtCategoryInfo.Rows[0]["Description"].ToString();
                        oResult.ImgPath = _sqlService.ImagePathGet(dtCategoryInfo.Rows[0]["LogoImage"].ToString(), 0, string.Empty);
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
        public CSQLResult CategoryDetailRemove(int id)
        {
            CSQLResult oResult = new CSQLResult();
            try
            {
                DataSet dsCategory = _sqlService.GetDataSet("TCategory", "uspCategoryDelete " + id);
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
                oResult.Success = false;
                oResult.WasSuccessful = 0;
                oResult.Exception = ex.Message;

            }
            return oResult;
        }
    }
}