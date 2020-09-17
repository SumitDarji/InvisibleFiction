using Newtonsoft.Json;
using Ornaments.BusinessObject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ornaments.API.Model
{
    public class BranchModel
    {
        public BranchModel() {
            Username = "";
            Password = "";
        }

        public BranchModel(CBranch oBranch)
        {
            BranchID = oBranch.BranchID;
            CompanyID = oBranch.CompanyID;
            Name = oBranch.Name;
            Address = oBranch.Address;
            Email = oBranch.Email;
            Location = oBranch.Location;
            Mobile = oBranch.Mobile;
            Username = oBranch.Username;
            Password = oBranch.Password;
        }

        public int BranchID { get; set; }
        public int CompanyID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}