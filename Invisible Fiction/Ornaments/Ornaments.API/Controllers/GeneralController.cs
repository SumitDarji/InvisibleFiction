using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ornaments.API.Model;
using Ornaments.API.Service.Interface;
using Ornaments.BusinessObject;

namespace Ornaments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralController : ControllerBase
    {

        private readonly IGeneralService _generalService;
        public GeneralController(IGeneralService generalService)
        {
            _generalService = generalService;
        }

        // POST: api/General
        [HttpPost]
        [Route("Login")]
        public CGeneralUser Post([FromBody] LoginModel loginModel)
        {
            CGeneralUser cGeneralUser = new CGeneralUser();
            cGeneralUser = _generalService.Login(loginModel.Username, loginModel.Password);
            return cGeneralUser;
        }
    }
}
