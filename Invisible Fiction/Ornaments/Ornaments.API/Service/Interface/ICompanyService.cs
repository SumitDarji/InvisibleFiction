using Ornaments.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ornaments.API.Service.Interface
{
    public interface ICompanyService
    {
        List<CCompany> CompanyDetailList();
        CCompany CompanyDetailGetById(int CompanyId);
        CSQLResult CompanyDetailRemove(int id);
    }
}
