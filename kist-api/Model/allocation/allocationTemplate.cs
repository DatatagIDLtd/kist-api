using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model.allocation
{
    public class AllocationTemplate
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public List<AllocationStep> steps { get; set; }
        public AllocationTemplate()
        {
            steps = new List<AllocationStep>();

        }


    }

 

    public class AllocationStep
    {
        public int id { get; set; }
        public string name { get; set; }
        public string label { get; set; }
        public string assetTypes { get; set; }
        public int maxItems { get; set; }

        public Boolean multiple { get; set; }

        public string placeholder { get; set; }

    }


}
