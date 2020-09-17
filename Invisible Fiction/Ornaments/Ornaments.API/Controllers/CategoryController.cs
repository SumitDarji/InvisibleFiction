using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ornaments.API.Service.Interface;
using Ornaments.BusinessObject;

namespace Ornaments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IGeneralService _generalService;
        public CategoryController(ICategoryService categoryService, IGeneralService generalService)
        {
            _categoryService = categoryService;
            _generalService = generalService;
        }
        // GET: api/Category
        [HttpGet(Name = "GetCategoryList")]
        public List<CCategory> GetCategoryList()
        {
            List<CCategory> cCategories = new List<CCategory>();
            cCategories = _categoryService.CategoryDetailList();
            return cCategories;
        }

        // GET: api/Category/5
        [HttpGet("{id}", Name = "GetCategory")]
        public CCategory GetCategory(int id)
        {
            CCategory cCategory = new CCategory();
            cCategory = _categoryService.CategoryDetailGetById(id);
            return cCategory;
        }

        // GET: api/Category/5
        [HttpGet("Images")]
        public CImages GetCategoryImages()
        {
            // 1: Category, 2: Ornaments 3: Position 
            CImages oImages = new CImages();
            oImages = _generalService.GetImages(1);
            return oImages;
        }
    }
}
