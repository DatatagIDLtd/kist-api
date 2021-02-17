using kist_api.Model.dtcode;
using kist_api.Model.dtcusid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model.dtmobile
{
    public class GetScanResponse
    {
        public List<GeoLocationEvent> Value { get; set; }
    }
}
