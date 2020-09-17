using System;

namespace Ornaments.BusinessObject
{
    public class CGeneralUser : CSQLResult
    {
        public CGeneralUser()
        {
            UserName = "";
            PassWord = "";
        }

        public string UserName { get; set; }  
        public string PassWord { get; set; }
        public int UserID { get; set; }

        public string DisplayName { get; set; } 
        public int LoginTypeCode { get; set; }

        public DateTime LastLogged { get; set; }

        public DateTime LoginBeforeLastLogged { get; set; }
    }
}