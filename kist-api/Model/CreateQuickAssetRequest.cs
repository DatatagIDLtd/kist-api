using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
	public class CreateQuickAssetRequest
	{
		public string modifiedBy { get; set; }
		public string operatorRef { get; set; }
		public string name { get; set; }
		public string oemRef { get; set; }
		public string modelRef { get; set; }
		public string colour { get; set; }
		public string type { get; set; }
		public string status { get; set; }
		public string description { get; set; }
		//public DateTime purchaseDate { get; set; }
		public string? owner { get; set; }
		public string serialNumber { get; set; }
		public string vrn { get; set; }
		public string engineNumber { get; set; }
		public string vin_Chassis { get; set; }
		public string uniqueReference { get; set; }
		public string dthireCompanyID { get; set; }
		public string dthireReferenceNumber { get; set; }
		public long parentAssetId { get; set; }
		public string dtIdnumber { get; set; }
		public string dtMembershipNumber { get; set; }
		public string lifeType { get; set; }
		public string identificationType { get; set; }
		public string ContractRef { get; set; }
		public string OwnerRef { get; set; }

		public bool containsItems { get; set; }


		public CreateQuickAssetRequest()
		{
			modifiedBy = "system";
			operatorRef = "";
			name = "";
			oemRef = "";
			modelRef = "";
			colour = "";
			type = "";
			status = "";
			description = "";
			//purchaseDate = System.DateTime.Now;
			owner = "";
			serialNumber = "";
			vrn = "";
			engineNumber = "";
			vin_Chassis = "";
			uniqueReference = "";
			dthireCompanyID = "";
			parentAssetId = 0;
			dtIdnumber = "";
			dtMembershipNumber = "";
			identificationType = "";
			lifeType = "";
			ContractRef = "";
			OwnerRef = "";
			containsItems = false;
		}

		//@OperatorRef = N'CL1',
		//@Name = N'New Gang',
		//@OemRef = NULL,
		//@ModelRef = NULL,
		//@Colour = NULL,
		//@Type = N'Gang',
		//@Status = N'Active',
		//@Description = N'New Gang',
		//@PurchaseDate = NULL,
		//@SerialNumber = NULL,
		//@VRN = NULL,
		//@EngineNumber = NULL,
		//@VIN_Chassis = NULL,
		//@UniqueReference = NULL,
		//@ParentAssetId = NULL,
		//@DTIdNumber = NULL,
		//@DTMembershipNumber = NULL
	}
}
