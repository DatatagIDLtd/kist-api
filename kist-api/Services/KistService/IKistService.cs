using ceup_api.Model.dtdead;
using kist_api.Model;
using kist_api.Model.dashboard;
using kist_api.Model.dtcusid;
using kist_api.Model.dtmobile;
using kist_api.Model.reports;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace kist_api.Services
{
    public interface IKistService
    {
        Task<String> PostDTDead(ClientConfig req);
        Task<LoginResponse> Login(LoginRequest loginReq);
        Task<UserDetails> UsersDetails(UserDetailsRequest userDetailsRequest);
        Task<List<AssetView>> GetAssets();
        Task<Asset> GetAsset(long id);
        Task<CreateAssetResult> CreateAsset(CreateQuickAssetRequest req);
        Task<List<AssetImages>> GetAssetImages(long id, long userId);
        Task<AssetIdentity> GetAssetIdentity(long id);
        Task<AssetIdentity> PutIdentity(AssetIdentity ai);
        Task<AssetSystem> GetAssetSystem(long id);
        Task<AssetSystem> PutAssetSystem(AssetSystem ai);
        Task<List<AssetView>> GetAssetsByUser(UserDetailsRequest userDetailsRequest);
        Task<List<AssetView>> GetAssetsByUser(GetAssetRequest asset);
        Task<List<AssetView>> GetInventoryByUser(GetAssetRequest asset);
        Task<List<RecentAllocation>> GetRecentAllocations(long userId);
        Task<List<Audit>> GetRecentAudits(long userId);
        Task<long> CreateAllocation(long Pid, long id,  long siteid ,String status , long userId);
        Task<long> RemoveAllocation( long id , long userId);
        Task<List<SiteView>> GetSitesByUser(GetSiteRequest asset);
        Task<Site> GetSite(long id);
        Task<Site> PutSite(Site site);
        Task<Asset> PutAsset(Asset asset);
        Task<Attachment> PutAttachment(Attachment attachment);
        Task<Note> PutNote(Note note);
        Task<List<Attachment>> GetAttachments(Attachment attachment);
        Task<List<Note>> GetNotes(Note note);
        Task<AssetGeoData> GetAssetGeoData(long id);
        Task<AssetGeoData> PutAssetGeoData(AssetGeoData ai);
        Task<List<Activity>> GetActivity(GetActivityRequest getActivityRequest);
        Task<List<GeoLocationEvent>> GetDTMobile_ScanEvents(string lookupCode);
        Task<List<GeoLocationEvent>> GetDTMobile_ScanEvents(GetScanRequest req);
        Task<GeoLocationEvent> PostGeoLocationEvent(GeoLocationEvent req);
        Task<SetAllocationAuditResponse> SetAllocationAudit(SetAllocationAuditRequest req);
        Task<CreateAuditResponse> CreateAudit(CreateAuditRequest req);
        Task<List<MyScan>> GetMyScans(String Id);
        Task<Dashboard> Dashboard(UserDetailsRequest userDetailsRequest);
        Task<Dashboard> GetMobileDashboard(UserDetailsRequest userDetailsRequest);
        Task<LookupData> GetLookUpData(UserDetailsRequest userDetailsRequest);
        Task<List<AssetStatusHistory>> GetAssetStatusHistory(long id);
        Task<GetMapPopupResponse> GetMapPopupInfo(String id);
        void SaveActivity_SQL(long operatorId, string appArea, string username, string desc);
        Task<List<CustomReport>> GetCustomReports(string userName);
    }
}