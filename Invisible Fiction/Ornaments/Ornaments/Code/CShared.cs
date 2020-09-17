using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Ornaments.Code
{
    public class CShared
    {
        #region "Global Declaration"
        private static string _sCryptoPass = "Ornaments";
        private byte[] _byKey = { 1, 2, 3, 4, 5, 6, 7, 8 };
        private static byte[] _byVector = { 65, 110, 68, 26, 69, 178, 200, 219 };
        // Create Connection Object for the SQL Connection
        protected SqlConnection connectionObject
        {
            get
            {
                SqlConnection SQLConn = new SqlConnection();
                SQLConn.ConnectionString = connectionString;
                return SQLConn;
            }
        }
        protected string connectionString
        {
            get
            {
                //return GetDecryptString(ConfigurationManager.AppSettings["SQLCn"]);
                return ConfigurationManager.AppSettings["OrnamentsSqlConnection"];
            }
        }
        #endregion

        #region "Encription Functions"
        public static string GetEncryptString(string sValue)
        {
            if (sValue != "")
            {
                TripleDESCryptoServiceProvider loCryptoClass = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider loCryptoProvider = new MD5CryptoServiceProvider();
                byte[] lbtBuffer;

                lbtBuffer = System.Text.Encoding.ASCII.GetBytes(sValue);
                loCryptoClass.Key = loCryptoProvider.ComputeHash(ASCIIEncoding.ASCII.GetBytes(_sCryptoPass));
                loCryptoClass.IV = _byVector;

                string sReturn = Convert.ToBase64String(loCryptoClass.CreateEncryptor().TransformFinalBlock(lbtBuffer, 0, lbtBuffer.Length));

                loCryptoClass.Clear();
                loCryptoProvider.Clear();
                loCryptoClass = null;
                loCryptoProvider = null;

                //Replace "=" symbol
                sReturn = sReturn.Replace("=", "{a61}");
                //sReturn = sReturn.Replace("+", "{a43}");

                return sReturn;
            }
            else
            {
                return "";
            }
        }

        public static string GetDecryptString(string sValue)
        {
            try
            {
                //ERROR: Not supported in C#: OnErrorStatement
                //On Error goto ErrHandler;
                if (sValue != "")
                {
                    //Replace "a61" ascii 61 to "="
                    sValue = sValue.Replace("{a61}", "=");
                    //sValue = sValue.Replace("{a43}", "+");

                    byte[] buffer;
                    TripleDESCryptoServiceProvider loCryptoClass = new TripleDESCryptoServiceProvider();
                    MD5CryptoServiceProvider loCryptoProvider = new MD5CryptoServiceProvider();

                    buffer = Convert.FromBase64String(sValue);
                    loCryptoClass.Key = loCryptoProvider.ComputeHash(ASCIIEncoding.ASCII.GetBytes(_sCryptoPass));
                    loCryptoClass.IV = _byVector;

                    string sReturn = Encoding.ASCII.GetString(loCryptoClass.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length));

                    loCryptoClass.Clear();
                    loCryptoProvider.Clear();
                    loCryptoClass = null;
                    loCryptoProvider = null;

                    return sReturn;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetUrlEncryptString(string sValue)
        {
            if (sValue != "")
            {
                TripleDESCryptoServiceProvider loCryptoClass = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider loCryptoProvider = new MD5CryptoServiceProvider();
                byte[] lbtBuffer;

                lbtBuffer = System.Text.Encoding.ASCII.GetBytes(sValue);
                loCryptoClass.Key = loCryptoProvider.ComputeHash(ASCIIEncoding.ASCII.GetBytes(_sCryptoPass));
                loCryptoClass.IV = _byVector;

                string sReturn = Convert.ToBase64String(loCryptoClass.CreateEncryptor().TransformFinalBlock(lbtBuffer, 0, lbtBuffer.Length));

                loCryptoClass.Clear();
                loCryptoProvider.Clear();
                loCryptoClass = null;
                loCryptoProvider = null;

                //Replace "=" symbol
                sReturn = sReturn.Replace("=", "{a61}");
                sReturn = sReturn.Replace("+", "{a43}");

                return sReturn;
            }
            else
            {
                return "";
            }
        }

        public static string GetUrlDecryptString(string sValue)
        {
            try
            {
                //ERROR: Not supported in C#: OnErrorStatement
                //On Error goto ErrHandler;
                if (sValue != "")
                {
                    //Replace "a61" ascii 61 to "="
                    sValue = sValue.Replace("{a61}", "=");
                    sValue = sValue.Replace("{a43}", "+");

                    byte[] buffer;
                    TripleDESCryptoServiceProvider loCryptoClass = new TripleDESCryptoServiceProvider();
                    MD5CryptoServiceProvider loCryptoProvider = new MD5CryptoServiceProvider();

                    buffer = Convert.FromBase64String(sValue);
                    loCryptoClass.Key = loCryptoProvider.ComputeHash(ASCIIEncoding.ASCII.GetBytes(_sCryptoPass));
                    loCryptoClass.IV = _byVector;

                    string sReturn = Encoding.ASCII.GetString(loCryptoClass.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length));

                    loCryptoClass.Clear();
                    loCryptoProvider.Clear();
                    loCryptoClass = null;
                    loCryptoProvider = null;

                    return sReturn;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetQueryString(string sQuery, string sValue)
        {
            if (!String.IsNullOrEmpty(sQuery))
            {
                //var dQuery = GetDecryptString(sQuery);
                var dQuery = GetUrlDecryptString(sQuery);
                var arrQuery = dQuery.Split('&');
                string sGetValue = "";

                if (arrQuery.Length > 0)
                {
                    for (var i = 0; i <= arrQuery.Length - 1; i++)
                    {
                        var sData = arrQuery[i].Split('=');

                        if (sData[0] == sValue)
                        {
                            sGetValue = sData[1];
                        }
                    }
                }

                return sGetValue;
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region "Send Email Function"
        public bool SendEmail(string sTo, string sBCC, string sFrom, string sSubject, string sBody)
        {
            bool functionReturnValue = false;
            MailMessage oMessage = new MailMessage();

            try
            {
                MailAddress oMailAddress;

                //Recipient
                string[] arrTo;
                string[] arrToItem;
                int iTo;
                //Split Name/Address pair
                if (sTo != "")
                {
                    arrTo = sTo.Split(';');
                    for (iTo = 0; iTo <= arrTo.Length - 1; iTo++)
                    {
                        //Add Item
                        arrToItem = arrTo[iTo].Split('|');
                        oMailAddress = new MailAddress(arrToItem[1], arrToItem[0]);
                        oMessage.To.Add(oMailAddress);
                    }
                }

                //Blind Carbon Copy
                if (sBCC != "")
                {
                    string[] arrBCC;
                    string[] arrBCCItem;
                    int iB;
                    //Split Name/Address pair 
                    arrBCC = sBCC.Split(';');
                    for (iB = 0; iB <= arrBCC.Length - 1; iB++)
                    {
                        //Add Item
                        arrBCCItem = arrBCC[iB].Split('|');
                        oMailAddress = new MailAddress(arrBCCItem[1], arrBCCItem[0]);
                        oMessage.Bcc.Add(oMailAddress);
                    }
                }

                //Sender Email
                string[] arrFrom;
                if (sFrom == "")
                {
                    sFrom = ConfigurationManager.AppSettings["fromEmail"];
                    arrFrom = sFrom.Split('|');
                }
                else
                {
                    arrFrom = sFrom.Split('|');
                }
                oMailAddress = new MailAddress(arrFrom[1], arrFrom[0]);
                oMessage.From = oMailAddress;
                oMessage.Subject = sSubject;
                oMessage.Body = sBody;
                oMessage.IsBodyHtml = true;

                //SMTP Mail Client
                SmtpClient oSMTPClient = new SmtpClient();

                //SMTP Mail Client SLL/TLS
                oSMTPClient.EnableSsl = true;

                oSMTPClient.Host = ConfigurationManager.AppSettings["smtpHost"];
                if (ConfigurationManager.AppSettings["smtpPort"] != null)
                {
                    oSMTPClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]);
                }

                oSMTPClient.Credentials = new NetworkCredential(GetDecryptString(ConfigurationManager.AppSettings["smtpUserName"]), GetDecryptString(ConfigurationManager.AppSettings["smtpPassword"]));
                oSMTPClient.Send(oMessage);

                functionReturnValue = true;
                //SendEmail = true;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                oMessage = null;
            }
            return functionReturnValue;
        }
        #endregion

        #region "DATABASE STORE PROCEDURE CONNECT FUNCTION"

        public DataSet GetDataSet(string dstTable, string dstSQL)
        {
            DataSet dst = new DataSet();
            DataSet dstReturn;
            SqlConnection SQLConn = new SqlConnection();
            SqlDataAdapter SQLdad;

            try
            {
                SQLConn = connectionObject;
                SQLdad = new SqlDataAdapter(dstSQL, SQLConn);
                SQLdad.Fill(dst);

                string[] arrTable = dstTable.Split(',');
                int iPos;
                if (dst.Tables.Count > 0)
                {
                    for (iPos = 0; iPos <= arrTable.Length - 1; iPos++)
                    {
                        dst.Tables[iPos].TableName = arrTable[iPos];
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SQLdad = null;
                if ((SQLConn != null))
                    SQLConn.Close();
                dstReturn = dst;
                dst.Dispose();
                dst = null;
            }
            return dstReturn;
        }

        // Execute a stored procedure and return a DataSet.
        ///////////////////////////////////////////////////
        public DataSet getSPDataSet(string SPName, params object[] Parameters)
        {
            SqlCommand sqlCommand = null;
            DataSet dataset = new DataSet();
            try
            {
                SqlConnection SQLConn = new SqlConnection();
                SQLConn = connectionObject;
                sqlCommand = new SqlCommand(SPName, SQLConn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                if (SPName.StartsWith("rpt"))
                {
                    sqlCommand.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["SqlCommandTimeout"]);
                }
                // 01-12-2014 MP - Line added to expand time-out period when spd stored procedure or CBR page stored procedure is run
                else if (SPName.StartsWith("spd") || SPName.Equals("getRes"))
                {
                    sqlCommand.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["SqlCommandTimeout"]);
                }
                for (int index = 0; index < Parameters.Length; index += 2)
                    sqlCommand.Parameters.AddWithValue(Parameters[index].ToString(), Parameters[index + 1]);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataset);
                sqlCommand.Connection.Close();
            }
            catch (Exception ex)
            {
                //LastError = ex.Message; // 20/05/14 - DFS - Store the error message so it is accessible from the calling method
                //log.Error(ex);
                throw ex;
                // dataset = null;
            }
            finally
            {
                if (sqlCommand != null)
                    sqlCommand.Connection.Close();
            }
            return dataset;
        }

        // NOT WORKING
        public static dynamic GetColumnValueFromDataTable(DataTable dt, string ConditionColumnName, dynamic ConditionValue, string GetColumnName)
        {
            dynamic dyValue = (from DataRow dr in dt.Rows
                               where dr[ConditionColumnName] == ConditionValue
                               select dr[GetColumnName]).FirstOrDefault();

            return dyValue;
        }

        #endregion

        #region OTHER GENERAL FUNCTIONS

        public string GenerateOrganizationUniqueID(string MasterUsername)
        {
            string UniqueId = "REPORG" + MasterUsername.Trim().Substring(0, 3).ToUpper() + DateTime.Now.ToString("ddMMyyHHmmss");
            return UniqueId;
        }

        public string GenerateOrganizationUserUniqueID(string Username)
        {
            string UserUniqueId = "REPUSR" + Username.Trim().Substring(0, 3).ToUpper() + DateTime.Now.ToString("ddMMyyHHmmss"); ;
            return UserUniqueId;
        }

        public string SetEmptyString(dynamic Value)
        {
            string sValue = Convert.ToString(Value);
            return String.IsNullOrEmpty(sValue) ? "" : sValue;
        }

        public string SetEmptyDateTime(string ParameterValue, string DateFormat)
        {
            return String.IsNullOrEmpty(ParameterValue) ? "" : Convert.ToDateTime(ParameterValue).ToString(DateFormat);
        }

        public string ImagePathGet(string ImagePath, int ImageTypeCode, string ImgName)
        {
            try
            {
                string sReturnStr = null;

                //In TCodeType: 16 - Image Type Code
                //In TCode: 61 - Organization
                //In TCode: 62 - Organization Project
                //In TCode: 63 - Organization User
                //In TCode: 64 - Default Replicant Admin 
                //In TCode: 83 - Default Replicant Template

                // DataSet ds = GetDataSet("dTable", "SELECT ImgPath FROM TImages WHERE ImageTypeCode = " + ImageTypeCode + " and ImgName = '" + ImgName + "'");

                // IF IMAGE PATH IS NOT EMPTY OR NULL THEN
                if (!(String.IsNullOrEmpty(ImagePath)))
                {
                    sReturnStr = ConfigurationManager.AppSettings["iPath"].ToString() + ImagePath;
                }
                // IF IMAGE PATH IS EMPTY AND oIMAGE CONTAIN DEFAULT INFORMATION THEN
                //else if (ds.Tables["dTable"].Rows.Count > 0 && ds.Tables["dTable"] != null && (String.IsNullOrEmpty(ImagePath)))
                //{
                //    sReturnStr = ConfigurationManager.AppSettings["iPath"].ToString() + ds.Tables["dTable"].Rows[0]["ImgPath"].ToString();
                //}
                else
                {
                    // WEBSITE IMAGE DEFAULT PATH
                    sReturnStr = ConfigurationManager.AppSettings["iPath"].ToString() + "Default//Default.png";
                }

                return sReturnStr;
            }
            catch (Exception Ex)
            {
                return Ex.Message;
            }
        }

        public string MsgAttachmentPathGet(string ImagePath)
        {
            try
            {
                string sReturnStr = null;

                // IF IMAGE PATH IS NOT EMPTY OR NULL THEN
                if (!(String.IsNullOrEmpty(ImagePath)))
                {
                    sReturnStr = ConfigurationManager.AppSettings["iPath"].ToString() + ImagePath;
                }
                return sReturnStr;
            }
            catch (Exception Ex)
            {
                return Ex.Message;
            }
        }

        // Get TrainingModulePAth
        public string TMDocumentPathGet(string TMDocumentPath)
        {
            string AppDPath = string.Empty;
            if (!String.IsNullOrEmpty(TMDocumentPath))
            {
                AppDPath = ConfigurationManager.AppSettings["iPath"].ToString();
                AppDPath = AppDPath + (TMDocumentPath.Replace('\\', '/'));
            }
            return AppDPath;
        }

        public string ConvertToPrjActionDateRange(string ScreenView, string FromDate, string ToDate)
        {
            string sReturn = "";

            // DAILY VIEW - FRI, 26 FEB, 2016 - CHANGE AFTER DISCUSS WITH MANISH ON 26FEB2016
            if (ScreenView == "Daily")
            {
                sReturn = Convert.ToDateTime(FromDate).ToString("ddd, dd MMM, yyyy");
                return sReturn;
            }
            // WEEKLY VIEW : 22 FEB 2016 - 28 FEB 2016 - CHANGE AFTER DISCUSS WITH MANISH ON 26FEB2016
            else if (ScreenView == "Weekly")
            {
                sReturn = Convert.ToDateTime(FromDate).ToString("dd MMM yyyy") + " - " + Convert.ToDateTime(ToDate).ToString("dd MMM yyyy");
                return sReturn;
            }
            // MONTHLY VIEW: 01 FEB 2016 - 29 FEB 2016 - CHANGE AFTER DISCUSS WITH MANISH ON 26FEB2016
            else if (ScreenView == "Monthly")
            {
                sReturn = Convert.ToDateTime(FromDate).ToString("dd MMM yyyy") + " - " + Convert.ToDateTime(ToDate).ToString("dd MMM yyyy");
                return sReturn;
            }

            return sReturn;
        }

        //START CALCULATE EndOn Date FOR THE ORGANIZATION PROJECT and/or PROJECT ACTION ITEMS 
        public DateTime? FindEndOnDate(int iTenureType, decimal iTenure, string sStartOn)
        {
            var TenureType = iTenureType;
            double Tenure = Convert.ToDouble(iTenure);
            DateTime StartOn = Convert.ToDateTime(sStartOn);
            DateTime? EndOn = null;

            // 41 - HOURS
            if (TenureType == 41)
            {
                EndOn = StartOn.AddHours(Tenure);
            }
            // 42 - DAYS
            else if (TenureType == 42)
            {
                EndOn = StartOn.AddDays(Tenure);
            }
            // 43 - WEEKS
            else if (TenureType == 43)
            {
                Tenure = Convert.ToInt32(Tenure) * 7;
                EndOn = StartOn.AddDays(Tenure);
            }
            // 44 - MONTHS
            else if (TenureType == 44)
            {
                EndOn = StartOn.AddMonths(Convert.ToInt32(Tenure));
            }
            // 45 - YEARS
            else if (TenureType == 45)
            {
                EndOn = StartOn.AddYears(Convert.ToInt32(Tenure));
            }

            return EndOn;
        }

        // RETREIVE THE INFOMATION OF WEEKS OR MONTH FOR THE SCREEN - 7.7.4
        public static Dictionary<DateTime, String> getDateRange(DateTime lowerDate, DateTime higherDate, String frequency)
        {
            DateTime startDate, endDate;
            startDate = Convert.ToDateTime(lowerDate.ToString("MM/dd/yyyy"));
            endDate = Convert.ToDateTime(higherDate.ToString("MM/dd/yyyy"));

            Dictionary<DateTime, String> returnDict = new Dictionary<DateTime, String>();

            if (frequency.Equals("Weekly"))
            {
                var firstDayOfWeek = "Monday"; // CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;

                while (startDate.DayOfWeek.ToString() != firstDayOfWeek)
                {
                    startDate = startDate.AddDays(-1);
                }
            }

            while (frequency.Equals("Weekly") ? (startDate.AddDays(7) <= endDate) : (startDate.AddMonths(1) <= endDate))
            {
                if (frequency.Equals("Weekly"))
                {
                    returnDict.Add(startDate, startDate + "-" + startDate.AddDays(7));
                    startDate = startDate.AddDays(8);
                }
                if (frequency.Equals("Monthly"))
                {
                    returnDict.Add(startDate, startDate + "-" + startDate.AddMonths(1));
                    startDate = startDate.AddMonths(1).AddDays(1);
                }
            }

            returnDict.Add(startDate, startDate + " - " + endDate);

            return returnDict;
        }

        // Get TrainingModulePAth
        public static string GetFilePathOfServer(string sPath)
        {
            string AppDPath = string.Empty;
            if (!String.IsNullOrEmpty(sPath))
            {
                AppDPath = ConfigurationManager.AppSettings["iPath"].ToString();
                AppDPath = AppDPath + (sPath.Replace('\\', '/'));
            }
            return AppDPath;
        }
        #endregion

        #region "CONVERT DATA TABLE TO LIST"
        public static List<T> DataTableToList<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        /*public static DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }

            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }*/
        #endregion
    }

    #region "DATE TIME EXTENSION"
    public static partial class DateTimeExtensions
    {
        public static DateTime FirstDayOfWeek(this DateTime dt)
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = dt.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-diff).Date;
        }

        public static DateTime LastDayOfWeek(this DateTime dt)
        {
            return dt.FirstDayOfWeek().AddDays(6);
        }

        public static DateTime FirstDayOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }

        public static DateTime LastDayOfMonth(this DateTime dt)
        {
            return dt.FirstDayOfMonth().AddMonths(1).AddDays(-1);
        }

        public static DateTime FirstDayOfNextMonth(this DateTime dt)
        {
            return dt.FirstDayOfMonth().AddMonths(1);
        }
    }
    #endregion
}