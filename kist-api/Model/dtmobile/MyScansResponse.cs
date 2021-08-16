using kist_api.Model.dtcode;
using kist_api.Model.dtcusid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model.dtmobile
{
    public class MyScansResponse
    {
        public List<MyScan> Value { get; set; }
    }

    public class MyScan
    {
        public String assetId { get; set; }
        public String description { get; set; }
        public String image { get; set; }
        public DateTime dateTime { get; set; }
    }

}
