using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class LookupData
    {
        public List<Lookup> assetTypes { get; set; }
        public List<Lookup> assetStatus { get; set; }
        public List<Lookup> colours { get; set; }

        public List<Lookup> oem { get; set; }

        public List<Lookup> model { get; set; }
        public List<Lookup> attachmentTypes { get; set; }
        public List<Lookup> siteTypes { get; set; }
        public List<Lookup> siteStatus { get; set; }
        public List<Lookup> operatorUsers { get; set; }

        

        public LookupData ()
        {
            assetTypes = new List<Lookup>();
            assetStatus = new List<Lookup>();
            colours = new List<Lookup>();
            oem = new List<Lookup>();
            model = new List<Lookup>();
            attachmentTypes = new List<Lookup>();
            siteTypes = new List<Lookup>();
            siteStatus = new List<Lookup>();
            operatorUsers = new List<Lookup>();
        }

    }
}
