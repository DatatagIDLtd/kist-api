using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class LookupData
    {
        public List<Lookup> assetType { get; set; }

        public LookupData ()
        {
            assetType = new List<Lookup>();
            
        }

    }
}
