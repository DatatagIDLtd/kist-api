using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class AssetView : Asset
	{
	
		public string assetType { get; set; }
		public string assetStatus { get; set; }

		public string make { get; set; }
		public string model { get; set; }
		public string fleetNo { get; set; }
		public string label { get; set; }
		public Boolean disabled { get; set; }

		public string? uniqueID { get; set; }
		public string? allocationStatus { get; set; }
		public string?allocationId { get; set; }
		public DateTime? effectiveFrom { get; set; }
		public DateTime? effectiveTo { get; set; }
		public string? imgUrl { get; set; }
		public string allocation { get; set; }

	}
}
