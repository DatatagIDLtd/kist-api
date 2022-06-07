using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class Audit
    {
		public long ID { get; set; }
		public long OperatorId { get; set; }
		public string AuditType { get; set; }
		public long AssetId { get; set; }
		public string AssignTo { get; set; }
		public DateTime CheckedOn { get; set; }
		public string CheckedBy { get; set; }
		public string AuditStatus { get; set; }
		public string Notes { get; set; }
		public bool IsActive { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public string assignedToInfo { get; set; }
		public string timeToCompleteAudit { get; set; }

		[JsonIgnore]
        public bool isLocal { get; set; }

    }
}
