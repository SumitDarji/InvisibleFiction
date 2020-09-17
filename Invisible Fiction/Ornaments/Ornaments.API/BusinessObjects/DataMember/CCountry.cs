using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReplicantWeb.BusinessObject
{
    public class CCountry
    {
        public int CountryID { get; set; }
        public string ISO { get; set; }
        public string Name { get; set; }
        public string NiceName { get; set; }
        public string ISO3 { get; set; }
        public string NumCode { get; set; }
        public string PhoneCode { get; set; }
    }
}