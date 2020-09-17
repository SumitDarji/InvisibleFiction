using Ornaments.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ornaments.API.Service.Interface
{
    public interface IOrnamentsService
    {
        List<COrnaments> OrnamentsDetailList();
        COrnaments OrnamentsDetailGetById(int OrnamentsID);
        CSQLResult OrnamentsDetailRemove(int id);
    }
}
