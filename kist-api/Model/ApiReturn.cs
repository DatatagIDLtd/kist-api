using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ceup_api.Model
{
	public class APIReturn
	{
		public string ExceptionString { get; set; }
		public string CustomerNameList { get; set; }
		public string CustomerEmailList { get; set; }
		public string OpsNameList { get; set; }
		public string OpsEmailList { get; set; }
		public string WarehouseNameList { get; set; }
		public string WarehouseEmailList { get; set; }
		public string Description { get; set; }
		public string FileName { get; set; }
		public string WarningReason { get; set; }
		public string CustomerEmail { get; set; }
		public string WarehouseEmail { get; set; }
		public string OpsEmail { get; set; }
	}
}
