using System;

namespace Ornaments.BusinessObject
{
    public class CDashboard : CSQLResult
    {
        public CDashboard() {
            CompanyCount = 0;
            CategoryCount = 0;
            PositionCount = 0;
            OrnamentCount = 0;
        }
        public int  CompanyCount { get; set; }  
        public int CategoryCount { get; set; }
        public int PositionCount { get; set; }
        public int OrnamentCount { get; set; }
    }
}