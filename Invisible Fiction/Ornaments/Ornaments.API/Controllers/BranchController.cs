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
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;
        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }
        // GET: api/Branch/{companyID}
        [HttpGet("CompanyId/{CompanyId}", Name = "GetBranchList")]
        public List<CBranch> GetBranchList(int CompanyId)
        {
            List<CBranch> cBranches = new List<CBranch>();
            cBranches = _branchService.BranchDetailList(CompanyId);
            return cBranches;
        }

        // GET: api/Branch/5
        [HttpGet("{id}", Name = "GetBranch")]
        public CBranch Get(int id)
        {
            CBranch cBranch = new CBranch();
            cBranch = _branchService.BranchDetailGetById(id);
            return cBranch;
        }
    }
}
