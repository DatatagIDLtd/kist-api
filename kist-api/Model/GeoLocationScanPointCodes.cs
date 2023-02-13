using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class GeoLocationScanPointCodes
    {
        public string IDNumber { get; set; }
    }

    public class GeoLocationScanPointCodesResponse
    {
        public List<GeoLocationScanPointCodes> Value { get; set; }
    }
}
