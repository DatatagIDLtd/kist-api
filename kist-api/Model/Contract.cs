using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
	public class Contract
	{
		public long ID { get; set; }
		public long OperatorId { get; set; }
		public string ContractReference { get; set; }
		public string ContractName { get; set; }
		public long? ContractCompanyId { get; set; }
		public DateTime ContractStartDate { get; set; }
		public DateTime ContractEndDate { get; set; }
		public int? ContractDuration { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? ModifiedOn { get; set; }
		public string? ModifiedBy { get; set; }
	}

}
