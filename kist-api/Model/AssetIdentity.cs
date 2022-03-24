using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class AssetIdentity
    {
		public long? id { get; set; }
		public long? assetId { get; set; }
		public long? OperatorId { get; set; }
		public string? serialNumber { get; set; }
		public string? VRN { get; set; }
		public string? engineNumber { get; set; }
		public string? UniqueReference { get; set; }
        public string? hireCompanyID { get; set; }
		public string? hireReferenceNumber { get; set; }

        public string? vin_Chassis { get; set; }
		//public string? qrCodeUrl { get; set; }

		public bool isActive { get; set; }
		public DateTime? createdOn { get; set; }
		public string createdBy { get; set; }

		public DateTime? modifiedOn { get; set; }
		public string modifiedBy { get; set; }





		//	[CompanyID]
		//	[bigint]
		//	NOT NULL,
		//[UniqueID] [nvarchar] (100) NULL,
		//[Name] [nvarchar] (100) NULL,
		//[AssetTypeID]
		//	[bigint]
		//	NOT NULL,
		//[Description] [nvarchar] (200) NOT NULL,

		// [SerialNumber] [nvarchar] (100) NOT NULL,

		//  [IsActive] [bit]
		//	NOT NULL,

		//  [CreatedOn] [datetime]
		//	NOT NULL,

		//  [CreatedBy] [nvarchar] (50) NOT NULL,

		//   [ModifiedOn] [datetime] NULL,
		//[ModifiedBy] [nvarchar] (50) NULL,

	}
}
