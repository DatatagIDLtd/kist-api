﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
	public class Owner
	{
		public long ID { get; set; }
		public long OperatorId { get; set; }
		public string Reference { get; set; }
		public string Name { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? ModifiedOn { get; set; }
		public string? ModifiedBy { get; set; }
	}
}
