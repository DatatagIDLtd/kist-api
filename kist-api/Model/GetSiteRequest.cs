using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class GetSiteRequest

    {
        public long? userId { get; set; }

        public string? siteCode { get; set; }
        public long? siteTypeID { get; set; }
        public string? location { get; set; }

        public string? name { get; set; }
        public string? status { get; set; }
        public long? siteStatusId { get; set; }
        //var postdata = new
        //{
        //    assetTypeID = "",
        //    id = 20060,
        //    location = "",
        //    make = "",
        //    model = "",
        //    name = "HAM",
        //    status = "",
        //    uniqueID = ""
        //};
    }
}
