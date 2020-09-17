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
    public class GeneralService : IGeneralService
    {
        private readonly ISqlService _sqlService;
        public GeneralService(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }
        public CGeneralUser Login(string UserName, string PassWord)
        {
            CGeneralUser oResult = new CGeneralUser();
            try
            {
                string ePassword = _sqlService.GetEncryptString(PassWord);
                DataSet dsLogin = _sqlService.GetDataSet("TLogin", "uspLogin '" + UserName + "', '" + ePassword + "'");
                using (DataTable dtLogin = dsLogin.Tables["TLogin"])
                {
                    if (dtLogin != null && dtLogin.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dtLogin.Rows[0]["Success"]))
                        {
                            // TRUE FOR ORGANIZATION
                            oResult.DisplayName = dtLogin.Rows[0]["DisplayName"].ToString();
                            oResult.UserID = Convert.ToInt32(dtLogin.Rows[0]["UserID"]);
                            oResult.LoginTypeCode = Convert.ToInt32(dtLogin.Rows[0]["LoginTypeCode"]);
                            oResult.LastLogged = Convert.ToDateTime(dtLogin.Rows[0]["LastLogged"]);
                            oResult.Success = Convert.ToBoolean(dtLogin.Rows[0]["Success"]);
                            oResult.WasSuccessful = Convert.ToInt32(dtLogin.Rows[0]["Success"]);
                            oResult.Exception = dtLogin.Rows[0]["ErrorSuccessMsg"].ToString();
                        }
                        // FAILED TO LOGIN
                        else
                        {
                            oResult.Success = Convert.ToBoolean(dtLogin.Rows[0]["Success"]);
                            oResult.WasSuccessful = Convert.ToInt32(dtLogin.Rows[0]["Success"]);
                            oResult.Exception = dtLogin.Rows[0]["ErrorSuccessMsg"].ToString();
                        }
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

        public CImages GetImages(int type, int id = 0)
        {
            CImages oResult = new CImages();
            try
            {
                // SELECT THE INFORMATION FROM THE STORE-PROCEDURE AND SET INFORMATION TO THE DATASET
                // 1: Category, 2: Ornaments 3: Position 
                string spParameter = type + ", "
                                   + id;
                DataSet dsImages = _sqlService.GetDataSet("TImages", "uspGetImages " + spParameter);

                using (DataTable dtImages = dsImages.Tables["TImages"])
                {
                    if (dtImages.Rows.Count > 0)
                    {
                        for (int iRow = 0; iRow <= dtImages.Rows.Count - 1; iRow++)
                        {
                            // SET THE DATASET INFORMATION TO THE RETURN VALUE
                            oResult.ImagesList.Add(_sqlService.ImagePathGet(dtImages.Rows[iRow]["ImgPath"].ToString(), 0, string.Empty));
                        }
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
    }
}
