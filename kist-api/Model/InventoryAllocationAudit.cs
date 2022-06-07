using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class InventoryAllocationAudit
    {
        public long id { get; set; }
        public int statusId { get; set; }
        public long auditId { get; set; }
        public long assetId { get; set; }
        public string assignTo { get; set; }
        public DateTime checkedOn { get; set; }
        public string checkedBy { get; set; }
    }
}
