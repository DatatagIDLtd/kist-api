using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class Attachment
    {


	
		public long ID { get; set; }
	
		public string appName { get; set; }
		public string area { get; set; }
		public long key { get; set; }
		public long subKey { get; set; }
		public long attachmentType { get; set; }
		public string fileName { get; set; }
		public string fileExtension { get; set; }
		public string notes { get; set; }
		public string storageLocation { get; set; }
		public string uploadedFileName { get; set; }
		public string tags { get; set; }

		public bool isActive { get; set; }
		public DateTime createdOn { get; set; }
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
