using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ornaments.BusinessObject
{
    public class COrnamentsPosition : CSQLResult
    {
        public int OrnamentPositionID { get; set; }
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgPath { get; set; }
    }
}