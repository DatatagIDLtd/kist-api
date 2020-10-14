using kist_api.Model;
using kist_api.Model.dtcusid;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace kist_api.Services
{
    public interface IKistService
    {
        Task<LoginResponse> Login(LoginRequest loginReq);
        Task<UserDetails> UsersDetails(UserDetailsRequest userDetailsRequest);
        Task<List<Asset>> GetAssets();
        Task<Asset> GetAsset(long id);
        Task<List<Asset>> GetAssetsByUser(UserDetailsRequest userDetailsRequest);
        Task<Asset> PutAsset(Asset asset);
    }       
}