using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model.dtmobile
{
    public class CreateAllocationRequest
    {
        public long ParentId { get; set; }
        public long[]  AssetID { get; set; }
        public long? operatorID { get; set; }
        public long siteId { get; set; }
        public string? User { get; set; }
        public string status { get; set; }
     



    }
}
