using Ornaments.BusinessObject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ornaments.API.Model
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
            ImgPath = oOrnamentsPosition.ImgPath;
        }
        public int OrnamentPositionID { get; set; }
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgPath { get; set; }
    }
}