using kist_api.Model.dashboard;
using kist_api.Model.dtcusid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class GetMapPopupResponse
    {
        public List<Value2> Value { get; set; }
    }
    public class Value2
    {
        public String popupinfo { get; set; }
        public String stockimageid { get; set; }
        public String beacon_red { get; set; }
        public String beacon_blue { get; set; }

        public int kistAssetId { get; set; }


    }

}
