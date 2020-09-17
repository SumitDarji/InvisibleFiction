using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ornaments.Models
{
    public class LoginModel
    {
        public LoginModel()
        {
            //Username = CGlobal.LoginUsername;
            //Password = CGlobal.LoginPassword;
            Username = "";
            Password = "";
        }

        [Required(ErrorMessage = "Username is required.", AllowEmptyStrings = false)]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Username must be minimum to 4 Characters Long.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.", AllowEmptyStrings = false)]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Password must be minimum to 6 Characters Long.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class ForgotModel
    {
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Username must be minimum to 4 Characters Long.")]
        public string Username { get; set; }

        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }
    }
}