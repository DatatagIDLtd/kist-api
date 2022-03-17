using kist_api.Helper.ApiResponse;
using kist_api.Model;
using kist_api.Model.dashboard;
using kist_api.Model.dtcusid;
using kist_api.Model.dtmobile;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace kist_api.Services
{
    public interface IScanService
    {
    
      //  Task<List<GeoLocationEvent>> GetDTMobile_ScanEvents(string lookupCode);
        //Task<List<GeoLocationEvent>> GetDTMobile_ScanEvents(GetScanRequest req);
       // Task<GeoLocationEvent> PostGeoLocationEvent(GeoLocationEvent req);


        Task<List<GeoLocationEventMapFlag>> GetScansByLocation(GetScanByLocationRequest req);
        Task<ApiResponseModel> GetScannedAssetDetails(ScanAssetDetailsRequestModel request);
        Task<ApiResponseModel> CreateGeoLocationEvents(ScanEventRequestModel request);
        Task<ApiResponseModel> GetAssetDocumentList(ScanAssetDetailsRequestModel request);




    }
}