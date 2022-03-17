using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class ScanEventRequestModel
    {
        public string IPAddress { get; set; }
        public string DeviceID { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string QRCodeURL { get; set; }
        public string IMEI { get; set; }
        public bool DevicePermissionGiven { get; set; }
        public string DeviceUserID { get; set; }
        public string DeviceDateTime { get; set; }
        public string UserGUID { get; set; }
        public string SystemType { get; set; }
        public string SecurityCode { get; set; }
        public string LookupCode { get; set; }
        public string Application { get; set; }
        public string UserName { get; set; }
}
}
