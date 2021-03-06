﻿using kist_api.Model.dashboard;
using kist_api.Model.dtcusid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class GetDashboardResponse
    {
        public List<Value> Value { get; set; }
    }
    public class Value
    {
        public List<Dashboard> dashboard { get; set; }
        public List<LookupData> lookupData { get; set; }
    }

}
