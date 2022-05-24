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

        public List<Dashboard> Dashboard { get; set; }
    }
}
