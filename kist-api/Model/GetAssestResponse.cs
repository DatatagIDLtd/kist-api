﻿using kist_api.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class GetAssetResponse
    {
        public List<AssetView> Value { get; set; }
    }
}
