using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class RecentAllocation
    {
		public long? id { get; set; }
		public long? operatorId { get; set; }
		public long? siteId { get; set; }
		public long? parentId { get; set; }
		public long? assetId { get; set; }
		public DateTime? effectiveFrom { get; set; }
		public DateTime? effectiveTo { get; set; }

		public long? allocationTypeId { get; set; }

		public long? allocatedBy { get; set; }
		public string? status { get; set; }
		public string? notes { get; set; }

		public DateTime? createdOn { get; set; }

		//public string? uniqueID { get; set; }
		public string? name { get; set; }
		public string? description { get; set; }
		public string? imgUrl { get; set; }

		public string? parentName { get; set; }
		public string? parentDescription { get; set; }
		public string? parentImgUrl { get; set; }

		//		al.[ID]
		//      ,al.[OperatorId]
		//      ,al.[SiteID]
		//      ,[AssetId]
		//      ,[EffectiveFrom]
		//      ,[EffectiveTo]
		//      ,[AllocationTypeId]
		//	  ,al.[AllocatedBy]
		//      ,[Status]
		//      ,[Notes]
		//      ,al.[CreatedOn]
		//	  ,IIF(ISNULL(a.Name,'')='', m.Name,a.Name) [Name]
		//	  ,a.Description
		//	  ,[dbo].[f_getAssetImage] (a.id) as imgUrl
		//	  ,a.ParentID
		//	  ,IIF(ISNULL(ap.Name,'')='', mp.Name,ap.Name) [Name]
		//--	  ,ap.Name[ParentName]
		//	  ,ap.Description[ParentDescription]
		//	   ,[dbo].[f_getAssetImage] (a.ParentID) as parentImgUrl

	}
}
