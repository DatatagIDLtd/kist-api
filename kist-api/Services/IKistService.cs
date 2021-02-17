using kist_api.Model;
using kist_api.Model.dashboard;
using kist_api.Model.dtcusid;
using kist_api.Model.dtmobile;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace kist_api.Services
{
    public interface IKistService
    {
        Task<LoginResponse> Login(LoginRequest loginReq);
        Task<UserDetails> UsersDetails(UserDetailsRequest userDetailsRequest);
        Task<List<AssetView>> GetAssets();
        Task<Asset> GetAsset(long id);
        Task<AssetIdentity> GetAssetIdentity(long id);
        Task<AssetIdentity> PutIdentity(AssetIdentity ai);
        Task<AssetSystem> GetAssetSystem(long id);
        Task<AssetSystem> PutAssetSystem(AssetSystem ai);
        Task<List<AssetView>> GetAssetsByUser(UserDetailsRequest userDetailsRequest);
        Task<List<AssetView>> GetAssetsByUser(GetAssetRequest asset);
        Task<Asset> PutAsset(Asset asset);
        Task<Attachment> PutAttachment(Attachment attachment);
        Task<Note> PutNote(Note note);
        Task<List<Attachment>> GetAttachments(Attachment attachment);
        Task<List<Note>> GetNotes(Note note);
        Task<List<Activity>> GetActivity(GetActivityRequest getActivityRequest);

        Task<List<GeoLocationEvent>> GetDTMobile_ScanEvents(string lookupCode);
        Task<List<GeoLocationEvent>> GetDTMobile_ScanEvents(GetScanRequest req);



        Task<Dashboard> Dashboard(UserDetailsRequest userDetailsRequest);
        Task<LookupData> GetLookUpData(UserDetailsRequest userDetailsRequest);
        Task<List<AssetStatusHistory>> GetAssetStatusHistory(long id);
        Task<GetMapPopupResponse> GetMapPopupInfo(String id);
        void SaveActivity_SQL(long operatorId, string appArea, string username, string desc);



    }
}