using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model.reports
{
    public class CustomReportResponse
    {
        public List<CustomReport> value { get; set; }
    }

    public class CustomReport
    {
        public string id { get; set; }
        public string optionalparamaters { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public string mandatoryparamaters { get; set; }
        public string url { get; set; }
        public string category { get; set; }
        public string reporticonurl { get; set; }
    }
}
