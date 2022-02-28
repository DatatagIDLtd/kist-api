using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class CreateVehicleCheckAuditRequest
	{
		//		{ "assetVehicleCheckId": "assetVehicleCheckId_1", "assetId": "assetId_2", "VehicleCheckId": "VehicleCheckId_3", "note": "note_4", "statusId": "statusId_5", "userid": "userid_6"}


	
	

		public long? assetId { get; set; }
		public long? statusId { get; set; }
		public string? note { get; set; }



		public long? userid { get; set; }



	


	}
}
