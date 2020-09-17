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
    public class OrnamentsPositionService : IOrnamentsPositionService
    {
        private readonly ISqlService _sqlService;
        public OrnamentsPositionService(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }
        public List<COrnamentsPosition> OrnamentsPositionDetailList()
        {
            List<COrnamentsPosition> oResult = new List<COrnamentsPosition>();
            try
            {
                // SELECT THE INFORMATION FROM THE STORE-PROCEDURE AND SET INFORMATION TO THE DATASET
                DataSet dsOrnamentsPositionInfo = _sqlService.GetDataSet("TOrnamentsPositionInfo", "uspOrnamentsPositionInfoGet 0");

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
                                ImgPath = _sqlService.ImagePathGet(dtOrnamentsPositionInfo.Rows[iRow]["LogoImage"].ToString(), 0, string.Empty)
                            });
                        }
                    }
                    return oResult;
                }
            }
            catch (Exception ex)
            {
                oResult.Add(new COrnamentsPosition()
                {
                    Success = false,
                    WasSuccessful = 0,
                    Exception = ex.Message
                });
                return oResult;
            }
        }

        public COrnamentsPosition OrnamentsPositionDetailGetById(int OrnamentsPositionID)
        {
            COrnamentsPosition oResult = new COrnamentsPosition();
            try
            {
                // SELECT THE INFORMATION FROM THE STORE-PROCEDURE AND SET INFORMATION TO THE DATASET
                DataSet dsOrnamentsPositionInfo = _sqlService.GetDataSet("TOrnamentsPositionInfo", "uspOrnamentsPositionInfoGet " + OrnamentsPositionID);

                using (DataTable dtOrnamentsPositionInfo = dsOrnamentsPositionInfo.Tables["TOrnamentsPositionInfo"])
                {
                    if (dtOrnamentsPositionInfo.Rows.Count > 0 && OrnamentsPositionID > 0)
                    {
                        oResult.OrnamentPositionID = Convert.ToInt32(dtOrnamentsPositionInfo.Rows[0]["PositionID"].ToString());
                        oResult.CategoryID = Convert.ToInt32(dtOrnamentsPositionInfo.Rows[0]["CategoryID"].ToString());
                        oResult.Name = dtOrnamentsPositionInfo.Rows[0]["Name"].ToString();
                        oResult.Description = dtOrnamentsPositionInfo.Rows[0]["Description"].ToString();
                        oResult.ImgPath = _sqlService.ImagePathGet(dtOrnamentsPositionInfo.Rows[0]["LogoImage"].ToString(), 0, string.Empty);
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

        public CSQLResult OrnamentsPositionDetailRemove(int id)
        {
            CSQLResult oResult = new CSQLResult();
            try
            {
                DataSet dsOrnamentsPositionInfo = _sqlService.GetDataSet("TOrnamentsPositionInfo", "uspOrnamentPositionDelete " + id);
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
                oResult.Success = false;
                oResult.WasSuccessful = 0;
                oResult.Exception = ex.Message;
            }
            return oResult;
        }
    }
}
