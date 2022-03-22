using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class AllocationAuditRequestModel
    {
        public long AssetId { get; set; }
        public long ParentId { get; set; }
        public long AuditId { get; set; }
        public long SiteId { get; set; }
        public long UserId { get; set; }
        public string UniqueId { get; set; }
    }
}


