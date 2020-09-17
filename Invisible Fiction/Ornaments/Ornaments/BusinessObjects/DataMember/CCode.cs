using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ornaments.BusinessObject
{
    public class CCode
    {
        public CCode()
        {
            CodeID = 0;
            CodeTypeId = 0;
            Description = "";
            Ref = "";
        }

        public int CodeID { get; set; }
        public int CodeTypeId { get; set; }
        public string Description { get; set; }
        public string Ref { get; set; }
    }
}