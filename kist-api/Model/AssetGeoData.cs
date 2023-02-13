using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class AssetGeoData
    {
		public long? id { get; set; }
		public long? assetId { get; set; }
		public long? OperatorId { get; set; }
		public string? wtw { get; set; }
		public string? postCode { get; set; }
		public float latitude { get; set; }
		public float longitude { get; set; }
        public int radius { get; set; }
		public bool isActive { get; set; }
		public DateTime? createdOn { get; set; }
		public string createdBy { get; set; }
		public DateTime? modifiedOn { get; set; }
		public string modifiedBy { get; set; }
	}
}