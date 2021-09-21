using kist_api.Model;
using kist_api.Model.dashboard;
using kist_api.Model.dtcusid;
using kist_api.Model.dtmobile;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace kist_api.Services
{
    public interface IConsumableService
    {
    
      //  Task<List<GeoLocationEvent>> GetDTMobile_ScanEvents(string lookupCode);
        //Task<List<GeoLocationEvent>> GetDTMobile_ScanEvents(GetScanRequest req);
       // Task<GeoLocationEvent> PostGeoLocationEvent(GeoLocationEvent req);


        Task<List<Consumable>> GetAssetConsumables(long id);

        Task<Consumable> SetAssetConsumable(SetAssetConsumableRequest req);
        Task<Consumable> CreateConsumableAudit(CreateConsumableAuditRequest req);


    }
}