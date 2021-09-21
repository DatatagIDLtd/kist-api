using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model.dtmobile
{
    public class SetAllocationAuditResponse
    {
        public long? result { get; set; }
        public long? assetId { get; set; }
        public long? allocationAuditId { get; set; }
        public long? auditId { get; set; }

        public string? error { get; set; }
    }
}
