using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ornaments.API.Code
{
    public interface ISqlService
    {
        string GetEncryptString(string sValue);
        string GetDecryptString(string sValue);

        string GetUrlEncryptString(string sValue);

        string GetUrlDecryptString(string sValue);

        string GetQueryString(string sQuery, string sValue);

        DataSet GetDataSet(string dstTable, string dstSQL);

        dynamic GetColumnValueFromDataTable(DataTable dt, string ConditionColumnName, dynamic ConditionValue, string GetColumnName);

        List<T> DataTableToList<T>(DataTable dt);

        string ImagePathGet(string ImagePath, int ImageTypeCode, string ImgName);
    }
}
