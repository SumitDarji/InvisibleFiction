using Ornaments.BusinessObject;
using Ornaments.Code;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ornaments.Models
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
            
        }

        [Key]
        public int OrnamentID { get; set; }

        [Required(ErrorMessage = "Name is required.", AllowEmptyStrings = false)]
        [StringLength(255, MinimumLength = 4, ErrorMessage = "Name must be minimum to 4 Characters Long.")]
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int OrnamentPositionID { get; set; }
        public string OrnamentPositionName { get; set; }
        public string Weight { get; set; }
        public decimal Cost { get; set; }
        public List<string> OrnamentsImgPath { get; set; }
        public HttpPostedFileBase[] OrnamentsImgFile { get; set; }
    }
}