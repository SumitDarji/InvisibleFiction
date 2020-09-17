using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ornaments.BusinessObject
{
    public class CCompany : CSQLResult
    {
        public long CompanyID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string LogoImgPath { get; set; }
        public string ProfileImgPath { get; set; }
        public string MasterUsername { get; set; }
        public string MasterPassword { get; set; }
        public string Status { get; set; }
        public DateTime LastLogged { get; set; }
        public DateTime LoginBeforeLastLogged { get; set; }
    }
}