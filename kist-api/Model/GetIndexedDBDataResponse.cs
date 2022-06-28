using kist_api.Model.dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class GetIndexedDBDataResponse
    {
        public List<AssetView> Assets { get; set; }

        public List<Audit> Audits { get; set; }

        public List<VehicleCheckScreenParameter> VehicleCheckScreenParameters { get; set; }

        public IndexedDBLookupData IndexedDbLookupData { get; set; }
    }

    public class IndexedDBLookupData
    {
        public List<Lookup> Contracts { get; set; }

        public List<Lookup> AssetTypes { get; set; }

        public List<Lookup> Oem { get; set; }

        public List<Lookup> AssetStatus { get; set; }
    }
}
