using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class SyncAllAuditsRequest
    {
        public List<SyncAuditRequest> RequestList { get; set; }

        public List<AssetView> VehicleChecksAssetList { get; set; }
    }

    public class SyncAuditRequest
    {
        public Audit Audit { get; set; }
        public List<AssetView> Assets { get; set; }
    }
}
