using Ornaments.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ornaments.API.Service.Interface
{
    public interface IBranchService
    {
        CBranch BranchDetailGetById(int BranchID);
        List<CBranch> BranchDetailList(int CompanyID);
        CSQLResult BranchDetailRemove(int id);
    }
}
