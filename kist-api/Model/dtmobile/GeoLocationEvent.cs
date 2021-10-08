using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model.dtmobile
{
    public class GeoLocationEvent
    {
        public string UserGUID { get; set; }
        public string IMEI { get; set; }
        public string SystemType { get; set; }
        public string LookupCode { get; set; }
        public string Application { get; set; }
        public string DeviceUserID { get; set; }
        public string SecurityCode { get; set; }
        public DateTime? DeviceDateTime { get; set; }
        public string Address { get; set; }
        public string MembershipPrefix { get; set; }
        public string QRCodeURL { get; set; }
        public string CreatedBy { get; set; }
        public bool DevicePermissionGiven { get; set; }
        public int ID { get; set; }
        public string Modifiedby { get; set; }
        public string DeviceID { get; set; }
        public DateTime? CreatedOn { get; set; }
        public decimal Longitude { get; set; }
        public string PostCode { get; set; }
        public decimal Latitude { get; set; }
        public string IPAddress { get; set;  }
        public string WTW { get; set; }
        public string UserName { get; set; }
        public string WFStatus { get; set; }



    }
}
