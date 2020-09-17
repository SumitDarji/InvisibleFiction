using Ornaments.BusinessObject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ornaments.API.Model
{
    public class CategoryModel
    {
        public CategoryModel() { }

        public CategoryModel(CCategory oCategory)
        {
            CategoryID = oCategory.CategoryID;
            Name = oCategory.Name;
            Description = oCategory.Description;
            ImgPath = oCategory.ImgPath;
        }

        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgPath { get; set; }
    }
}