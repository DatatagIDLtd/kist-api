using kist_api.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class GetSiteResponse
    {
        public List<SiteView> Value { get; set; }
    }
}
