using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class GetAssetRequest

    {
        public long? userId { get; set; }
        public string? fleetNo { get; set; }
        public string? uniqueID { get; set; }
        public long? assetTypeID { get; set; }
        public string? location { get; set; }
        public string? make { get; set; }
        public string? model { get; set; }
        public string? name { get; set; }
        public string? status { get; set; }
        public long? assetStatusId { get; set; }
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
