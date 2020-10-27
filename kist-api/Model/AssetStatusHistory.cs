using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class AssetStatusHistory
    {
		public long? id { get; set; }
		public long? assetId { get; set; }
		public long? assetstatusId { get; set; }
		public string assetstatus { get; set; }

		
		public DateTime? modifiedOn { get; set; }
		public string modifiedBy { get; set; }




	}
}
