using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model

{
    public class CreateAssetRequest
    {

		public string? operatorRef { get; set; }
		public string? name { get; set; }
		public string? oemRef { get; set; }
		public string? modelRef { get; set; }
		public string? colour { get; set; }
		public string? type { get; set; }
		public string? status { get; set; }
		public string? description { get; set; }
		public DateTime purchaseDate { get; set; }
		public string? serialNumber { get; set; }
		public string? vrn { get; set; }
		public string? engineNumber { get; set; }
		public string? vin_Chassis { get; set; }
		public string? uniqueReference { get; set; }
		public long? parentAssetId { get; set; }
		public string? dtIdnumber { get; set; }
		public string? dtMembershipNumber { get; set; }
	}
}
