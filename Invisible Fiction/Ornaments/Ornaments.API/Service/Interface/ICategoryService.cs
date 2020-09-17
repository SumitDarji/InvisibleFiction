using Ornaments.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ornaments.API.Service.Interface
{
    public interface ICategoryService
    {
        List<CCategory> CategoryDetailList();
        CCategory CategoryDetailGetById(int CategoryID);
        CSQLResult CategoryDetailRemove(int id);
    }
}
