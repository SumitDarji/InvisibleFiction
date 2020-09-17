using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ornaments.BusinessObject
{
    public class CBranch : CSQLResult
    {
        public int BranchID { get; set; }
        public int CompanyID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }

        public string Location { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public DateTime LastLogged { get; set; }
        public DateTime LoginBeforeLastLogged { get; set; }
    }
}