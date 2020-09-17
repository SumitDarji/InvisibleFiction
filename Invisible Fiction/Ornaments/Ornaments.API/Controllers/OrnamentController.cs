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
    public class OrnamentController : ControllerBase
    {
        private readonly IOrnamentsService _ornamentsService;
        private readonly IGeneralService _generalService;
        public OrnamentController(IOrnamentsService ornamentsService, IGeneralService generalService)
        {
            _ornamentsService = ornamentsService;
            _generalService = generalService;
        }
        // GET: api/Ornament
        [HttpGet]
        public List<COrnaments> Get()
        {
            List<COrnaments> cOrnaments = new List<COrnaments>();
            cOrnaments = _ornamentsService.OrnamentsDetailList();
            return cOrnaments;
        }

        // GET: api/Ornament/5
        [HttpGet("{id}", Name = "GetOrnaments")]
        public COrnaments GetOrnaments(int id)
        {
            COrnaments cOrnaments = new COrnaments();
            cOrnaments = _ornamentsService.OrnamentsDetailGetById(id);
            return cOrnaments;
        }

        // GET: api/Ornament/5
        [HttpGet("Images/{id}")]
        public CImages GetCategoryImages(int id)
        {
            // 1: Category, 2: Ornaments 3: Position 
            CImages oImages = new CImages();
            oImages = _generalService.GetImages(2, id);
            return oImages;
        }
    }
}
