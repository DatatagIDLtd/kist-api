using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class SetAssetVehicleCheckRequest
    {
        //		{ "assetVehicleCheckId": "assetVehicleCheckId_1", "assetId": "assetId_2", "VehicleCheckId": "VehicleCheckId_3", "note": "note_4", "statusId": "statusId_5", "userid": "userid_6"}

        public long? assetId { get; set; }

        public long? userId { get; set; }

        public string xml { get; set; }

        public long? mileage { get; set; }

    }


    //public class VehicleCheckList
    //{
    //    public int vehicleCheckId { get; set; }

    //    public bool vehicleCheckStatus { get; set; }

    //    public string textCapture { get; set; }

    //}
}
