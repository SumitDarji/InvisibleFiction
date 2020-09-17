using Ornaments.BusinessObject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ornaments.API.Model
{
    public class OrnamentsModel
    {
        public OrnamentsModel() {
            OrnamentsImgPath = new List<string>();
        }

        public OrnamentsModel(COrnaments oOrnaments)
        {
            OrnamentID = oOrnaments.OrnamentID;
            Name = oOrnaments.Name;
            Description = oOrnaments.Description;
            CategoryID = oOrnaments.CategoryID;
            CategoryName = oOrnaments.CategoryName;
            OrnamentPositionID = oOrnaments.OrnamentPositionID;
            OrnamentPositionName = oOrnaments.OrnamentPositionName;
            Weight = oOrnaments.Weight;
            Cost = oOrnaments.Cost;
            OrnamentsImgPath = oOrnaments.OrnamentsImgPath;
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