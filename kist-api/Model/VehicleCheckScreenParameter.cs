using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class VehicleCheckScreenParameter
	{
		public string? description { get; set; }
		public long? VehicleCheckId { get; set; }
		public long? VehicleCheckStatusId { get; set; }
		public long? assetVehicleCheckStatusId { get; set; }
		public long? VehicleCheckSortOrder { get; set; }
		public long? groupSortOrder { get; set; }
		public long? groupStart { get; set; }
		public string? groupDescription { get; set; }
        public string vehicleCheckType { get; set; }
		public DateTime auditDate { get; set; }

	}
}
