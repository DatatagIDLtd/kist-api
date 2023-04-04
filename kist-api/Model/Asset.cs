using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class Asset
    {
		public long? id { get; set; }
		public long? parentId { get; set; }
		public long? OperatorId { get; set; }

		//public string? uniqueID { get; set; }
		public string? name { get; set; }
		public long? assetTypeID { get; set; }
		//public string? assetType { get; set; }
		//public string? searchQuery { get; set }
		public long? contractId { get; set; }
		public long? ownerId { get; set; }
		public long? lifeTypeId { get; set; }

		public string description { get; set; }
		public long? oemID { get; set; }
		public long modelId { get; set; }
		public long? assetStatusID { get; set; }
		//public string location { get; set; }
		public long? assignUserId { get; set; }
		public bool isActive { get; set; }
		public DateTime? createdOn { get; set; }
		public string createdBy { get; set; }

		public DateTime? modifiedOn { get; set; }
		public string modifiedBy { get; set; }

		public bool containsItems { get; set; }
        public bool geoLocation { get; set; }
		public bool virtualContainer { get; set; }
    }
}
