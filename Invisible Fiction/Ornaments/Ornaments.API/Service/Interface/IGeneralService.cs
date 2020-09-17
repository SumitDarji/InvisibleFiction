using Ornaments.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ornaments.API.Service.Interface
{
    public interface IGeneralService
    {
        CGeneralUser Login(string UserName, string PassWord);
        CImages GetImages(int type, int id = 0);
    }
}
