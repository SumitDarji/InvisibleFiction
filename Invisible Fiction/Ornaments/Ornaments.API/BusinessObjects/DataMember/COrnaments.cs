using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ornaments.BusinessObject
{
    public class COrnaments : CSQLResult
    {
        public COrnaments()
        {
            OrnamentsImgPath = new List<string>();
        }
        public int OrnamentID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int OrnamentPositionID { get; set; }
        public string OrnamentPositionName { get; set; }
        public string Weight { get; set; }
        public decimal Cost { get; set; }
        public List<string> OrnamentsImgPath { get; set; }
    }
}