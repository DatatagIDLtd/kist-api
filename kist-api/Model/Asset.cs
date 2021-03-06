﻿using System;
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
		//public string? searchQuery { get; set; }
		public string description { get; set; }
		public long? oemID { get; set; }
		public long modelId { get; set; }
		public long? assetStatusID { get; set; }
		//public string location { get; set; }

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
