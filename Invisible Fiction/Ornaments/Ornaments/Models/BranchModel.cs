using Ornaments.BusinessObject;
using Ornaments.Code;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ornaments.Models
{
    public class BranchModel
    {
        public BranchModel() { }

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

        [Key]
        public int BranchID { get; set; }
        public int CompanyID { get; set; }

        [Required(ErrorMessage = "Name is required.", AllowEmptyStrings = false)]
        [StringLength(255, MinimumLength = 4, ErrorMessage = "Name must be minimum to 4 Characters Long.")]
        public string Name { get; set; }
        
        public string Address { get; set; }

        [Required(ErrorMessage = "Location is required.", AllowEmptyStrings = false)]
        public string Location { get; set; }

        //[RemoteClientServer("ValiddateEmail", "General", ErrorMessage = "Email Already used. Please use different email.", OtherPropertyName = "Email", AdditionalFields = "oldEmail")]
        [Required(ErrorMessage = "Email is required.", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }
        public string oldEmail { get; set; }

        //[RemoteClientServer("ValiddateCountryMobileCode", "General", ErrorMessage = "Please insert valid country code.", OtherPropertyName = "CountryCodeMobile", AdditionalFields = "Username")]
        //[Required(ErrorMessage = "Country Code is required.", AllowEmptyStrings = false)]
        //[RegularExpression(@"^[0-9]*$", ErrorMessage = "Country Code should contain only numbers.")]
        //public string CountryCodeMobile { get; set; }

        [Required(ErrorMessage = "Mobile number is required.", AllowEmptyStrings = false)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-.]?([0-9]{3})[-.]?([0-9]{4})$", ErrorMessage = "Please enter valid Mobile number.")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "Phone number must be minimum to 10 Characters Long.")]
        [NotEqualTo("Phone", "Phone")]
        public string Mobile { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-.]?([0-9]{3})[-.]?([0-9]{4})$", ErrorMessage = "Please enter valid Phone number.")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "Phone number must be minimum to 10 Characters Long.")]
        [NotEqualTo("Mobile", "Mobile")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Username is required.", AllowEmptyStrings = false)]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Username must be minimum to 4 Characters Long.")]
        //[RemoteClientServer("ValiddateUsername", "General", ErrorMessage = "Master Username is Already used. Please try another.", OtherPropertyName = "Username", AdditionalFields = "oldUsername")]
        public string Username { get; set; }
        public string oldUsername { get; set; }

        [Required(ErrorMessage = "Password is required.", AllowEmptyStrings = false)]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Password must be minimum to 6 Characters Long.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}