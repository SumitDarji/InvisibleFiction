using Ornaments.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ornaments.API.Service.Interface
{
    public interface IOrnamentsPositionService
    {
        List<COrnamentsPosition> OrnamentsPositionDetailList();
        COrnamentsPosition OrnamentsPositionDetailGetById(int OrnamentsPositionID);
        CSQLResult OrnamentsPositionDetailRemove(int id);
    }
}
