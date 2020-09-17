using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ornaments.API.Model
{
    public class LoginModel
    {
        public LoginModel()
        {
            Username = "";
            Password = "";
        }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
