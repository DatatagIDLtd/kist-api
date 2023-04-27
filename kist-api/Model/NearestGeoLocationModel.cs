using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class NearestGeoLocationModel
    {
        public string assetid { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public string idnumber { get; set; }
        public string wtw { get; set; }
        public string postcode { get; set; }
        public string distance { get; set; }
        public string error { get; set; }
        public string result { get; set; } 
    }
}
