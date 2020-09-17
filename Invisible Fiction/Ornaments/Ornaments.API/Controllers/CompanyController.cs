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
    public class CompanyController : ControllerBase
    {
        // SERVER MAP PATH
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        // GET: api/Company
        [HttpGet]
        public List<CCompany> Get()
        {
            List<CCompany> cCompanies = new List<CCompany>();
            cCompanies = _companyService.CompanyDetailList();
            return cCompanies;
        }

        // GET: api/Company/5
        [HttpGet("{id}", Name = "GetCompany")]
        public CCompany GetCompany(int id)
        {
            CCompany cCompany = new CCompany();
            cCompany = _companyService.CompanyDetailGetById(id);
            return cCompany;
        }
    }
}
