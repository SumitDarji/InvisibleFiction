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
    public class PositionController : ControllerBase
    {
        private readonly IOrnamentsPositionService _ornamentsPositionService;
        private readonly IGeneralService _generalService;
        public PositionController(IOrnamentsPositionService ornamentsPositionService, IGeneralService generalService)
        {
            _ornamentsPositionService = ornamentsPositionService;
            _generalService = generalService;
        }
        // GET: api/Position
        [HttpGet]
        public List<COrnamentsPosition> Get()
        {
            List<COrnamentsPosition> cOrnamentsPositions = new List<COrnamentsPosition>();
            cOrnamentsPositions = _ornamentsPositionService.OrnamentsPositionDetailList();
            return cOrnamentsPositions;
        }

        // GET: api/Position/5
        [HttpGet("{id}", Name = "GetPosition")]
        public COrnamentsPosition GetPosition(int id)
        {
            COrnamentsPosition cOrnamentsPosition = new COrnamentsPosition();
            cOrnamentsPosition = _ornamentsPositionService.OrnamentsPositionDetailGetById(id);
            return cOrnamentsPosition;
        }
        // GET: api/Category/5
        [HttpGet("Images")]
        public CImages GetCategoryImages()
        {
            // 1: Category, 2: Ornaments 3: Position 
            CImages oImages = new CImages();
            oImages = _generalService.GetImages(3);
            return oImages;
        }
    }
}
