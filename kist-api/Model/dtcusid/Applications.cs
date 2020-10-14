using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model.dtcusid
{
    public class Applications
    {
        public long ID { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationCode { get; set; }
        public bool IsActive { get; set; }
    }
}
