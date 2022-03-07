using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model.dashboard
{
    public class Misc
    {
        public string loggedInUser { get; set; }
        public string roleName { get; set; }
        public long vehicleAssetId { get; set; }
        public string operatorRef { get; set; }
        public long operatorId { get; set; }
    }
}
