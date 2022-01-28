using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
	public class ContractRequest
	{
		public long ID { get; set; }
		public long OperatorId { get; set; }
		public string Reference { get; set; }
		public string Name { get; set; }
		public string UserName { get; set; }

		public long? CompanyId { get; set; }

		public ContractRequest()
		{
			Reference = "";
			Name = "";
			UserName = "";
			CompanyId = 0;

		}


	}



}
