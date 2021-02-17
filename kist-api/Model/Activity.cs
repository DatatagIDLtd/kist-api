using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class Activity
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
		public long? itemkey { get; set; }
		

	}
}
