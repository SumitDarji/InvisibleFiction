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
    public class OrnamentsService : IOrnamentsService
    {
        private readonly ISqlService _sqlService;
        public OrnamentsService(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }
        public List<COrnaments> OrnamentsDetailList()
        {
            List<COrnaments> oResult = new List<COrnaments>();
            List<string> images = new List<string>();
            try
            {
                // SELECT THE INFORMATION FROM THE STORE-PROCEDURE AND SET INFORMATION TO THE DATASET
                DataSet dsOrnamentsInfo = _sqlService.GetDataSet("TOrnamentsInfo,TImages", "uspOrnamentsInfoGet 0");

                using (DataTable dtOrnamentsInfo = dsOrnamentsInfo.Tables["TOrnamentsInfo"])
                {
                    if (dtOrnamentsInfo.Rows.Count > 0)
                    {
                        for (int iRow = 0; iRow <= dtOrnamentsInfo.Rows.Count - 1; iRow++)
                        {
                            images = new List<string>();
                            int ornamentID = Convert.ToInt32(dtOrnamentsInfo.Rows[iRow]["OrnamentID"].ToString());
                            using (DataTable dtImagesInfo = dsOrnamentsInfo.Tables["TImages"])
                            {
                                if (dtImagesInfo.Rows.Count > 0)
                                {
                                    for (int jRow = 0; jRow <= dtImagesInfo.Rows.Count - 1; jRow++)
                                    {
                                        int imgOrnamnetID = Convert.ToInt32(dtImagesInfo.Rows[jRow]["OrnamentID"].ToString());
                                        if (ornamentID == imgOrnamnetID)
                                        images.Add(_sqlService.ImagePathGet(dtImagesInfo.Rows[jRow]["ImgPath"].ToString(), 0, string.Empty));
                                    }
                                }
                            }
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
                                Cost = Convert.ToDecimal(dtOrnamentsInfo.Rows[iRow]["Cost"].ToString()),
                                OrnamentsImgPath = images
                            });
                        }
                    }
                    return oResult;
                }
            }
            catch (Exception ex)
            {
                oResult.Add(new COrnaments()
                {
                    Success = false,
                    WasSuccessful = 0,
                    Exception = ex.Message
                });
                return oResult;
            }
        }

        public COrnaments OrnamentsDetailGetById(int OrnamentsID)
        {
            COrnaments oResult = new COrnaments();
            try
            {
                // SELECT THE INFORMATION FROM THE STORE-PROCEDURE AND SET INFORMATION TO THE DATASET
                DataSet dsOrnamentsInfo = _sqlService.GetDataSet("TOrnamentsInfo,TImages", "uspOrnamentsInfoGet " + OrnamentsID);
                List<string> images = new List<string>();
                using (DataTable dtOrnamentsInfo = dsOrnamentsInfo.Tables["TOrnamentsInfo"])
                {
                    if (dtOrnamentsInfo.Rows.Count > 0 && OrnamentsID > 0)
                    {
                        int ornamentID = Convert.ToInt32(dtOrnamentsInfo.Rows[0]["OrnamentID"].ToString());
                        using (DataTable dtImagesInfo = dsOrnamentsInfo.Tables["TImages"])
                        {
                            if (dtImagesInfo.Rows.Count > 0)
                            {
                                for (int jRow = 0; jRow <= dtImagesInfo.Rows.Count - 1; jRow++)
                                {
                                    int imgOrnamnetID = Convert.ToInt32(dtImagesInfo.Rows[jRow]["OrnamentID"].ToString());
                                    if (ornamentID == imgOrnamnetID)
                                        images.Add(_sqlService.ImagePathGet(dtImagesInfo.Rows[jRow]["ImgPath"].ToString(), 0, string.Empty));
                                }
                            }
                        }
                        oResult.OrnamentID = Convert.ToInt32(dtOrnamentsInfo.Rows[0]["OrnamentID"].ToString());
                        oResult.OrnamentPositionID = Convert.ToInt32(dtOrnamentsInfo.Rows[0]["PositionID"].ToString());
                        oResult.CategoryID = Convert.ToInt32(dtOrnamentsInfo.Rows[0]["CategoryID"].ToString());
                        oResult.Name = dtOrnamentsInfo.Rows[0]["Name"].ToString();
                        oResult.Description = dtOrnamentsInfo.Rows[0]["Description"].ToString();
                        oResult.OrnamentPositionName = dtOrnamentsInfo.Rows[0]["PositionName"].ToString();
                        oResult.Weight = dtOrnamentsInfo.Rows[0]["Weight"].ToString();
                        oResult.Cost = Convert.ToDecimal(dtOrnamentsInfo.Rows[0]["Cost"].ToString());
                        oResult.OrnamentsImgPath = images;
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

        public CSQLResult OrnamentsDetailRemove(int id)
        {
            CSQLResult oResult = new CSQLResult();
            try
            {
                DataSet dsOrnamentsInfo = _sqlService.GetDataSet("TOrnamentsInfo", "uspOrnamentsDelete " + id);
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
                oResult.Success = false;
                oResult.WasSuccessful = 0;
                oResult.Exception = ex.Message;
            }
            return oResult;
        }
    }
}
