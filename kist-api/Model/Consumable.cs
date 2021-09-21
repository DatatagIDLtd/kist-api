using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class Consumable
    {
//		SELECT '' as ActivityId,
//OperatorId ,
//'Asset' as ApplicationArea
//      ,[ModifiedOn]
//      ,[ModifiedBy]
//	  ,'Some Change' [Description]
//	  ,assetid Itemkey
//  FROM[DTKIST].[dbo].[AssetHistory]

		public string? activityUrl { get; set; }
		public long? operatorId { get; set; }

		public string? applicationArea { get; set; }
		public DateTime? modifiedOn { get; set; }
		public string modifiedBy { get; set; }

		public string? description { get; set; }
		public long? assetConsumableId { get; set; }
		public long? id { get; set; }
		public long? consumableId { get; set; }
		public long? consumableStatusId { get; set; }
		public long? assetConsumableStatusId { get; set; }
		public long? consumableSortOrder { get; set; }
		public long? groupSortOrder { get; set; }
		public long? groupStart { get; set; }
		public string? groupDescription { get; set; }

		//c.SortOrder ,cg.sortOrder ,cg.Description

		public long? statusId { get; set; }

		public DateTime? createdOn { get; set; }
		public string createdBy { get; set; }

		public string vrn { get; set; }
		public DateTime auditDate { get; set; }


	}
}
