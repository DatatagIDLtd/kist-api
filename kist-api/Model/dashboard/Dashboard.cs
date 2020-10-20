using kist_api.Model;
using kist_api.Model.dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model.dashboard
{
    public class Dashboard
    {
        public List<Widget> widgets { get; set; }
        public List<Misc> misc { get; set; }
    }
}
