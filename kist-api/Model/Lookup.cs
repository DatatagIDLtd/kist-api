using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class Lookup
    {
        public long ID { get; set; }
        public long? parentid { get; set; }

        public string value { get; set; }
    }
    
}
