using Ornaments.BusinessObject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ornaments.API.Model
{
    public class CompanyModel
    {
        public CompanyModel() {
            MasterUsername = "";
            MasterPassword = "";
        }

        public CompanyModel(CCompany oCompany)
        {
            CompanyID = oCompany.CompanyID;
            Name = oCompany.Name;
            Address = oCompany.Address;
            Email = oCompany.Email;
            Mobile = oCompany.Mobile;
            MasterUsername = oCompany.MasterUsername;
            MasterPassword = oCompany.MasterPassword;
            LogoImgPath = oCompany.LogoImgPath;
        }

        public long CompanyID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string MasterUsername { get; set; }
        public string MasterPassword { get; set; }
        public string LogoImgPath { get; set; }
    }
}