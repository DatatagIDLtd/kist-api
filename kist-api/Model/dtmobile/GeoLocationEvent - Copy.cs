using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model.dtmobile
{
    public class GeoLocationEventMapFlag : GeoLocationEvent
    {
       
        public string name { get; set; }

        public string scandate { get; set; }
        public string contactInfo { get; set; }
        public DateTime? ModifiedOn { get; set; }


    }
}
