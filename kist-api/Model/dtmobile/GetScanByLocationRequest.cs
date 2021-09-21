using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model.dtmobile
{
    public class GetScanByLocationRequest
    {
        public decimal lat { get; set; }
        // need @ so compiler dont moan , should be changed to longitude !!!! 
        public decimal @long { get; set; }
        public decimal milage { get; set; }
        public string search { get; set; }
        

 

    }
}
