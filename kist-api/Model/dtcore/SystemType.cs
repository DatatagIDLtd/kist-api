using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model.dtcore
{
	//[{"Description": null, "ID": 101, "FilePrefix": null, "InUse": true, "IsActive": true, "ModifiedBy": null, "InformationURL": "https:\/\/www.cesarscheme.org\/cesar-micro.html", "CreatedOn": "2019-07-11T16:14:22.0000+01:00", "CreatedBy": "dbo", "PreviewImageLocation": "", "ModifiedOn": null, "TypeCode": "TC", "ProductPartNumber": "DATCESARMICRO08", "Name": "Micro CESAR"}]}
    public class SystemType
    {
		public long? id { get; set; }
		public long? assetId { get; set; }
		public long? operatorId { get; set; }
		public string? TypeCode { get; set; }
		public string? PreviewImageLocation { get; set; }
		public string? InformationURL { get; set; }
		public string? ProductPartNumber { get; set; }
		public string? Name { get; set; }
		public bool? InUse { get; set; }
		
			


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
