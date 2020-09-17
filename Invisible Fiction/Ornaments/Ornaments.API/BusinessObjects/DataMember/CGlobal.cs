using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Ornaments
{
    public class CSQLResult 
    {
        public CSQLResult()
        {
            WasSuccessful = 0;
            Exception = "";
            Success = false;
            OtherParameter = "";
            SQLError = "";
        }
        public int WasSuccessful { get; set; }
        public string Exception { get; set; }
        public Boolean Success { get; set; }
        public string OtherParameter { get; set; }
        public string SQLError { get; set; }
    }
}