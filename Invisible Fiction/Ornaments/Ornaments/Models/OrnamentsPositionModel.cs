using Ornaments.BusinessObject;
using Ornaments.Code;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ornaments.Models
{
    public class OrnamentsPositionModel
    {
        public OrnamentsPositionModel() { }

        public OrnamentsPositionModel(COrnamentsPosition oOrnamentsPosition)
        {
            OrnamentPositionID = oOrnamentsPosition.OrnamentPositionID;
            CategoryID = oOrnamentsPosition.CategoryID;
            Name = oOrnamentsPosition.Name;
            Description = oOrnamentsPosition.Description;
        }

        [Key]
        public int OrnamentPositionID { get; set; }
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Name is required.", AllowEmptyStrings = false)]
        [StringLength(255, MinimumLength = 4, ErrorMessage = "Name must be minimum to 4 Characters Long.")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgPath { get; set; }
        public HttpPostedFileBase ImgFile { get; set; }
    }
}