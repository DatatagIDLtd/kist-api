using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model.dtmobile
{
    public class CreateAssetResult
    {
        public long? result { get; set; }
        public long? assetId { get; set; }
        public long? systemId { get; set; }
        public long? identityId { get; set; }
        public string? error { get; set; }
    }
}
