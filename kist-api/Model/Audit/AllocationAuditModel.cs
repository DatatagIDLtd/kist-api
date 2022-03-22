using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class AllocationAuditModel
    {
        public long OperatorId { get; set; }
        public long AllocationId { get; set; }
        public long AuditId { get; set; }
        public long SiteId { get; set; }
        public string AssignTo { get; set; }
        public DateTime CheckedOn { get; set; }
        public string CheckedBy { get; set; }
        public long StatusId { get; set; }
        public long AssetStatusID { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }


    }
}

