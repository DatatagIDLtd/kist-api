using kist_api.Model.dtcore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class AssetSystem
    {
		public AssetSystem ()
		{

			systemTypeInfo = new SystemType();
		}
		public long? id { get; set; }
		public long? assetId { get; set; }
		public long? operatorId { get; set; }
		public string? idNumber { get; set; }
		public string? membershipNumber { get; set; }
		public string? productCode { get; set; }
		public string? productShortCode { get; set; }
	
		public string? qrCodeUrl { get; set; }
		public bool isActive { get; set; }
		public DateTime? createdOn { get; set; }
		public string createdBy { get; set; }

		public DateTime? modifiedOn { get; set; }
		public string modifiedBy { get; set; }


		public SystemType systemTypeInfo { get; set; }


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
