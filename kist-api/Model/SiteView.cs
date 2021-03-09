using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class SiteView : Site
	{
	

		public string siteStatus { get; set; }

		public string fleetNo { get; set; }

		public string? uniqueID { get; set; }
		public string? siteType { get; set; }
		public string? allocationSummary { get; set; }



	}
}
