using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class GetActivityRequest

    {
        public long? operatorId { get; set; }
        public long? maxrows { get; set; }
      
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }

    }
}
