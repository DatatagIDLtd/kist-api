using kist_api.Model;
using kist_api.Model.dtcusid;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Numerics;
using Microsoft.AspNetCore.Components.Forms;
using System.Reflection.PortableExecutable;
using System.IO;
using kist_api.Model.dashboard;
using System.Data.SqlClient;
using System.Data;
using kist_api.Model.dtcore;
using Microsoft.Extensions.Logging;
using kist_api.Model.dtcode;
using kist_api.Model.dtmobile;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration.UserSecrets;
using ceup_api.Model.dtdead;
using ceup_api.Model;
using Microsoft.AspNetCore.Http;
using kist_api.Model.reports;

namespace kist_api.Services
{
    public class KistService : IKistService
    {
        private const string BaseUrl = "https://jsonplaceholder.typicode.com/todos/";
        private readonly HttpClient _client;
        readonly IConfiguration _configuration;
        public string _connStr = String.Empty;
        private readonly ILogger<KistService> _logger;

        public KistService(HttpClient client, IConfiguration configuration , ILogger<KistService> logger)
        {
            _client = client;
            _configuration = configuration;
            _connStr = _configuration.GetConnectionString("DevConnection");
            _logger = logger;
        }

        public async Task<LoginResponse> Login(LoginRequest loginReq)
        {
            _logger.LogInformation(@"KistService\Login");

          
            LoginResponse loginRes = new LoginResponse();
            _logger.LogInformation(@"Created loginRes");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("AuthUser") + ":" + _configuration.GetValue<string>("AuthPassword"));
            _logger.LogInformation(@"Created byteArray");

            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            _logger.LogInformation(@"Created Headers");

            StringContent content = new StringContent(JsonConvert.SerializeObject(loginReq), Encoding.UTF8, "application/json");
            _logger.LogInformation(@"Created String Content");

            var url = _configuration.GetValue<string>("AuthEndPoint");
            _logger.LogInformation(@"Url End Point " + url);

            using (var response = await _client.PostAsync(url, content))
            {
                _logger.LogInformation(@"getting Response");

                string apiResponse = await response.Content.ReadAsStringAsync();
                _logger.LogInformation(@"Response:" + apiResponse);

                loginRes = JsonConvert.DeserializeObject<LoginResponse>(apiResponse);
                _logger.LogInformation(@"Convert Response");


                if (loginRes.userDetails != null && loginRes.response == null)
                {
                    // fetch userdetails to find company
                    // UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
                    //userDetailsRequest.id = loginRes.userDetails._ProviderUserKey.ToString();

                    //  UserDetails userDetails = await UsersDetails(userDetailsRequest)
                    _logger.LogInformation(@"We have Login Details , so lets call Kist API and get user Details");

                    if (loginRes.userDetails._IsApproved)
                    {
                        _logger.LogInformation(@"User Approved - Generate token");

                        loginRes.userDetails.token = generateJwtToken(loginRes.userDetails , loginReq);
                        loginRes.userDetails.role = "NGMUSR";
                    } else
                    {
                        _logger.LogWarning(@"User Not Approved");

                        loginRes.response = "User not approved";
                    }
                    //  loginRes.userDetails.token = generateJwtToken(loginRes.userDetails);
                       

                }else
                {
                    _logger.LogWarning(@"Invalid Credentials " + content.ToString());
                   // SaveActivity_SQL(0, "login", loginReq.username, "Failed to logon");
                }
                    // authentication successful so generate jwt token
              

            }
            
            
            return loginRes;
        }

        public async Task<UserDetails> UsersDetails(UserDetailsRequest userDetailsRequest)
        {
            GetUserDetailsResponse userDetailsResponse = new GetUserDetailsResponse();

            StringContent content = new StringContent(JsonConvert.SerializeObject(userDetailsRequest), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));


            using (var response = await _client.PostAsync(_configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:GetUserDetails"), content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                //var options = new JsonSerializerOptions
                //{
                //    PropertyNameCaseInsensitive = true,
                //};

                // loginRes = System.Text.Json.JsonSerializer.Deserialize<GetUserDetailsResponse>(apiResponse, options);
                userDetailsResponse = JsonConvert.DeserializeObject<GetUserDetailsResponse>(apiResponse);


            }
            // proc should only return one row , but comes back as a list regardless from API
            return userDetailsResponse.Value.First();
        }

        public async Task<Dashboard> Dashboard(UserDetailsRequest userDetailsRequest)
        {

            GetDashboardResponse dashboardResponse = new GetDashboardResponse();

            StringContent content = new StringContent(JsonConvert.SerializeObject(userDetailsRequest), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));


            using (var response = await _client.PostAsync(_configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:GetDashboard"), content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                //var options = new JsonSerializerOptions
                //{
                //    PropertyNameCaseInsensitive = true,
                //};
                //File.WriteAllText(@"c:\temp\payload.json", apiResponse);
                //apiResponse = apiResponse.Replace("\\\"", "\"");  //savs frig
                apiResponse = apiResponse.Replace("\"dashboard\": \"", "\"dashboard\": ");  //savs frig
                apiResponse = apiResponse.Replace("}]}]\"", "}]}]");  //savs frig
                apiResponse = apiResponse.Replace("\\\"", "\"");  //savs frig

                File.WriteAllText(@"c:\temp\payload2.json", apiResponse);
                // loginRes = System.Text.Json.JsonSerializer.Deserialize<GetUserDetailsResponse>(apiResponse, options);
                dashboardResponse = JsonConvert.DeserializeObject<GetDashboardResponse>(apiResponse);


            }
            // proc should only return one row , but comes back as a list regardless from API
            return dashboardResponse.Value.First().dashboard.First();
        }

        public async Task<Dashboard> GetMobileDashboard(UserDetailsRequest userDetailsRequest)
        {

            GetDashboardResponse dashboardResponse = new GetDashboardResponse();

            StringContent content = new StringContent(JsonConvert.SerializeObject(userDetailsRequest), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));


            using (var response = await _client.PostAsync(_configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:GetMobileDashboard"), content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                //var options = new JsonSerializerOptions
                //{
                //    PropertyNameCaseInsensitive = true,
                //};
                //File.WriteAllText(@"c:\temp\payload.json", apiResponse);
                //apiResponse = apiResponse.Replace("\\\"", "\"");  //savs frig
                apiResponse = apiResponse.Replace("\"dashboard\": \"", "\"dashboard\": ");  //savs frig
                apiResponse = apiResponse.Replace("}]}]\"", "}]}]");  //savs frig
                apiResponse = apiResponse.Replace("\\\"", "\"");  //savs frig

              
                // loginRes = System.Text.Json.JsonSerializer.Deserialize<GetUserDetailsResponse>(apiResponse, options);
                dashboardResponse = JsonConvert.DeserializeObject<GetDashboardResponse>(apiResponse);


            }
            // proc should only return one row , but comes back as a list regardless from API
            return dashboardResponse.Value.First().dashboard.First();
        }

        public async Task<List<AssetImages>> GetAssetImages(long id, long userId )
        {
            var req = new { AssetId = id,  userId = userId , tags = ""};

             List<AssetImages> res ;

            StringContent content = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));


            using (var response = await _client.PostAsync(_configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:GetAssetImages"), content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                res = JsonConvert.DeserializeObject<List<AssetImages>>(JObject.Parse(apiResponse).GetValue("value").ToString());


         

            }
           
            return res;
        }

        public async Task<long> CreateAllocation(long Pid , long id , long siteid , String status, long userId)
        {
            var req = new { AssetId = id  , siteid=siteid , ParentId = Pid , status = status ,operatorId = 1 , userId = userId };

            GetMapPopupResponse res = new GetMapPopupResponse();


            StringContent content = new StringContent(Regex.Unescape(JsonConvert.SerializeObject(req)), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var url = _configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:CreateAllocation");
            using (var response = await _client.PostAsync(url, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
   
                res = JsonConvert.DeserializeObject<GetMapPopupResponse>(apiResponse);


            }
            // proc should only return one row , but comes back as a list regardless from API
            return 1;
        }

        public async Task<long> RemoveAllocation(long id , long userid)
        {
            var req = new { AllocationId = id ,  AssetId = 1,  operatorId = 1, userId = userid, status="History"};

            GetMapPopupResponse res = new GetMapPopupResponse();


            StringContent content = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var url = _configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:RemoveAllocation");
            using (var response = await _client.PostAsync(url, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                res = JsonConvert.DeserializeObject<GetMapPopupResponse>(apiResponse);


            }
            // proc should only return one row , but comes back as a list regardless from API
            return 1;
        }

        public async Task<GetMapPopupResponse> GetMapPopupInfo(String id)
        {
            var req = new { AssetId = id };

            GetMapPopupResponse res = new GetMapPopupResponse();


            StringContent content = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var url = _configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:GetMapPopupInfo");
            using (var response = await _client.PostAsync(url , content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                //var options = new JsonSerializerOptions
                //{
                //    PropertyNameCaseInsensitive = true,
                //};
                //File.WriteAllText(@"c:\temp\payload.json", apiResponse);
                //apiResponse = apiResponse.Replace("\\\"", "\"");  //savs frig

                // loginRes = System.Text.Json.JsonSerializer.Deserialize<GetUserDetailsResponse>(apiResponse, options);
                res = JsonConvert.DeserializeObject<GetMapPopupResponse>(apiResponse);


            }
            // proc should only return one row , but comes back as a list regardless from API
            return res;
        }

        public async Task<LookupData> GetLookUpData(UserDetailsRequest userDetailsRequest)
        {
            GetLookupDataResponse lookupResponse = new GetLookupDataResponse();
         
            StringContent content = new StringContent(JsonConvert.SerializeObject(userDetailsRequest), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));


            using (var response = await _client.PostAsync(_configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:GetLookupData"), content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                //var options = new JsonSerializerOptions
                //{
                //    PropertyNameCaseInsensitive = true,
                //};
                File.WriteAllText(@"c:\temp\lookupdata.json", apiResponse);
                //apiResponse = apiResponse.Replace("\\\"", "\"");  //savs frig
                apiResponse = apiResponse.Replace("\"lookupdata\": \"", "\"lookupdata\": ");  //savs frig
                apiResponse = apiResponse.Replace("}]}]\"", "}]}]");  //savs frig
                apiResponse = apiResponse.Replace("\\\"", "\"");  //savs frig

                //File.WriteAllText(@"c:\temp\payload2.json", apiResponse);
                // loginRes = System.Text.Json.JsonSerializer.Deserialize<GetUserDetailsResponse>(apiResponse, options);
                lookupResponse = JsonConvert.DeserializeObject<GetLookupDataResponse>(apiResponse);

                
            }
            // proc should only return one row , but comes back as a list regardless from API
            return lookupResponse.Value.First().lookupData.First();
        }

        public async Task<List<AssetView>> GetAssets()
        {
            GetAssetResponse userDetailsResponse = new GetAssetResponse();

         //   StringContent content = new StringContent(JsonConvert.SerializeObject(getAssetRequest), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            using (var response = await _client.GetAsync(_configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:GetAssets"))) //, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                userDetailsResponse = JsonConvert.DeserializeObject<GetAssetResponse>(apiResponse);

            }
            // proc should only return one row , but comes back as a list regardless from API
            return userDetailsResponse.Value;
        }

        public async Task<List<MyScan>> GetMyScans(String Id)
        {
            MyScansResponse res = new MyScansResponse();
            var req = new { UserGUID = Id, };

               StringContent content = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
            // UserGUID : "EE2F80F1-AA52-450C-ACEA-520D6F2EE1BC"
            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("DTMobile:apiUser") + ":" + _configuration.GetValue<string>("DTMobile:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            using (var response = await _client.PostAsync(_configuration.GetValue<string>("DTMobile:APIEndPoint") + _configuration.GetValue<string>("DTMobile:GetMyScans"), content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<MyScansResponse>(apiResponse);

            }
            // proc should only return one row , but comes back as a list regardless from API
            return res.Value;
        }

        public async Task<List<AssetView>> GetAssetsByUser(UserDetailsRequest userDetailsRequest)
        {
            GetAssetResponse userDetailsResponse = new GetAssetResponse();

            StringContent content = new StringContent(JsonConvert.SerializeObject(userDetailsRequest), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            using (var response = await _client.PostAsync(_configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:GetAssetsByUser"), content)) //, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                userDetailsResponse = JsonConvert.DeserializeObject<GetAssetResponse>(apiResponse);

            }
            // proc should only return one row , but comes back as a list regardless from API
            return userDetailsResponse.Value;
        }

        public async Task<List<AssetView>> GetAssetsByUser(GetAssetRequest asset)
        {
            GetAssetResponse userDetailsResponse = new GetAssetResponse();

            if (asset.location == null) { asset.location = ""; };
            if (asset.uniqueID == null) { asset.uniqueID = ""; };
            if (asset.fleetNo == null) { asset.fleetNo = ""; };
            if (asset.assetTypeID == null) { asset.assetTypeID = 0; };
            if (asset.make == null) { asset.make = ""; };
            if (asset.model == null) { asset.model = ""; };
            if (asset.name == null) { asset.name = ""; };
            if (asset.status == null) { asset.status = ""; };
            if ( asset.assetStatusId > 0 )
            {
                asset.status = asset.assetStatusId.ToString();
                asset.assetStatusId = null;
            }
            
               

        StringContent content = new StringContent(JsonConvert.SerializeObject(asset), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            using (var response = await _client.PostAsync(_configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:GetAssetsByUser"), content)) //, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                userDetailsResponse = JsonConvert.DeserializeObject<GetAssetResponse>(apiResponse);

            }
            // proc should only return one row , but comes back as a list regardless from API
            return userDetailsResponse.Value;
        }

        public async Task<List<AssetView>> GetInventoryByUser(GetAssetRequest asset)
        {
            GetAssetResponse userDetailsResponse = new GetAssetResponse();

            if (asset.location == null) { asset.location = ""; };
            if (asset.uniqueID == null) { asset.uniqueID = ""; };
            if (asset.fleetNo == null) { asset.fleetNo = ""; };
            if (asset.assetTypeID == null) { asset.assetTypeID = 0; };
            if (asset.make == null) { asset.make = ""; };
            if (asset.model == null) { asset.model = ""; };
            if (asset.name == null) { asset.name = ""; };
            if (asset.status == null) { asset.status = ""; };
            if (asset.assetStatusId > 0)
            {
                asset.status = asset.assetStatusId.ToString();
                asset.assetStatusId = null;
            }



            StringContent content = new StringContent(JsonConvert.SerializeObject(asset), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            using (var response = await _client.PostAsync(_configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:GetInventoryByUser"), content)) //, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                userDetailsResponse = JsonConvert.DeserializeObject<GetAssetResponse>(apiResponse);

            }
            // proc should only return one row , but comes back as a list regardless from API
            return userDetailsResponse.Value;
        }

        public async Task<List<SiteView>> GetSitesByUser(GetSiteRequest site)
        {
            GetSiteResponse userDetailsResponse = new GetSiteResponse();

            if (site.location == null) { site.location = ""; };
            if (site.siteCode == null) { site.siteCode = ""; };
       
            if (site.siteTypeID == null) { site.siteTypeID = 0; };

            if (site.name == null) { site.name = ""; };
            if (site.status == null) { site.status = ""; };
            if (site.siteStatusId > 0)
            {
                site.status = site.siteStatusId.ToString();
                site.siteStatusId = null;
            }



            StringContent content = new StringContent(JsonConvert.SerializeObject(site), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            using (var response = await _client.PostAsync(_configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:GetSitesByUser"), content)) //, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                userDetailsResponse = JsonConvert.DeserializeObject<GetSiteResponse>(apiResponse);

            }
            // proc should only return one row , but comes back as a list regardless from API
            return userDetailsResponse.Value;
        }

        public async Task<Site> PutSite(Site site)
        {
            Site userDetailsResponse = new Site();
            //  asset.modifiedBy = (string)HttpContext.Items["User"];

            StringContent content = new StringContent(Regex.Unescape(JsonConvert.SerializeObject(site)), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            using (var response = await _client.PutAsync(_configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:PutSite") + "(" + site.id.ToString() + ")", content)) //, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                userDetailsResponse = JsonConvert.DeserializeObject<Site>(apiResponse);

            }
            // proc should only return one row , but comes back as a list regardless from API
            return userDetailsResponse;
        }

        public async Task<Site> GetSite(long id)
        {
            Site userDetailsResponse = new Site();

            //   StringContent content = new StringContent(JsonConvert.SerializeObject(getAssetRequest), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            using (var response = await _client.GetAsync(_configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:GetSite") + "(" + id.ToString() + ")")) //, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                userDetailsResponse = JsonConvert.DeserializeObject<Site>(apiResponse);

            }
            // proc should only return one row , but comes back as a list regardless from API
            //perform post prod validation

            //if (userDetailsResponse.companyID != companyId)
            //{
            //    // requested asset for in correct company id , fail ! 

            //}
            return userDetailsResponse;
        }

        public async Task<CreateAssetResult> CreateAsset(CreateQuickAssetRequest req)
        {
            CreateAssetResult dashboardResponse = new CreateAssetResult();
            //   var jsonSettings = new JsonSerializerSettings();
            //   jsonSettings.DateFormatString = _configuration.GetValue<string>("api:dateFormat"); //"dd/MM/yyy hh:mm:ss";

            // string json = JsonConvert.SerializeObject(someObject, jsonSettings);

            StringContent content = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));


            using (var response = await _client.PostAsync(_configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:CreateAsset"), content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

            //    dashboardResponse = JsonConvert.DeserializeObject<CreateAssetResult>(apiResponse);

                dashboardResponse = JsonConvert.DeserializeObject<CreateAssetResult>(JObject.Parse(apiResponse).GetValue("value").First().ToString());

                

              //  dashboardResponse = JsonConvert.DeserializeObject<dashboardResponse>(JObject.Parse(dashboardResponse).GetValue("value").First()); // .ToString());


            }
            // proc should only return one row , but comes back as a list regardless from API
            return dashboardResponse;
        }

        public async Task<SetAllocationAuditResponse> SetAllocationAudit(SetAllocationAuditRequest req)
        {
            SetAllocationAuditResponse res = new SetAllocationAuditResponse();


            StringContent content = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));


            using (var response = await _client.PostAsync(_configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:SetAllocationAudit"), content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<SetAllocationAuditResponse>(JObject.Parse(apiResponse).GetValue("value").First().ToString());
            }
            // proc should only return one row , but comes back as a list regardless from API
            return res;
        }

        public async Task<CreateAuditResponse> CreateAudit(CreateAuditRequest req)
        {
            CreateAuditResponse res = new CreateAuditResponse();


            StringContent content = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));


            using (var response = await _client.PostAsync(_configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:CreateAllocationAudit"), content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<CreateAuditResponse>(JObject.Parse(apiResponse).GetValue("value").First().ToString());
            }
            // proc should only return one row , but comes back as a list regardless from API
            return res;
        }

        public async Task<Asset> GetAsset(long id )
        {
            Asset userDetailsResponse = new Asset();
            //   StringContent content = new StringContent(JsonConvert.SerializeObject(getAssetRequest), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword") );
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            using (var response = await _client.GetAsync(_configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:GetAsset") + "(" + id.ToString() + ")")) //, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                userDetailsResponse = JsonConvert.DeserializeObject<Asset>(apiResponse);

            }
            // proc should only return one row , but comes back as a list regardless from API
            //perform post prod validation

            //if (userDetailsResponse.companyID != companyId)
            //{
            //    // requested asset for in correct company id , fail ! 

            //}
            return userDetailsResponse;
        }

        public async Task<AssetIdentity> GetAssetIdentity(long id)
        {
            GetAssetIndentyResponse assetIndentity = new GetAssetIndentyResponse();

            //   StringContent content = new StringContent(JsonConvert.SerializeObject(getAssetRequest), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var url = _configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:GetAssetIdentity") + "?$filter=AssetId eq " + id.ToString() ;

            using (var response = await _client.GetAsync(url)) //, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                assetIndentity = JsonConvert.DeserializeObject<GetAssetIndentyResponse>(apiResponse);

            }
            // proc should only return one row , but comes back as a list regardless from API
            //perform post prod validation

            //if (userDetailsResponse.companyID != companyId)
            //{
            //    // requested asset for in correct company id , fail ! 

            //}
          //  AssetIdentity a = new AssetIdentity();
            var a = assetIndentity.Value.First();

            return a;
        }

        public async Task<AssetSystem> GetAssetSystem(long id)
        {
            GetAssetSystemResponse assetsystem = new GetAssetSystemResponse();

            //   StringContent content = new StringContent(JsonConvert.SerializeObject(getAssetRequest), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var url = _configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:GetAssetSystem") + "?$filter=AssetId eq " + id.ToString();

            using (var response = await _client.GetAsync(url)) //, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                assetsystem = JsonConvert.DeserializeObject<GetAssetSystemResponse>(apiResponse);

            }
            var a = assetsystem.Value.First();

            if (a.membershipNumber.Length > 1)
            {
                if (a.productCode == "KIST")
                {
                    a.systemTypeInfo.Name = "KIST";
                    a.qrCodeUrl = "https://www.datatag.mobi/qrcsrm.aspx?id="+ a.idNumber+ "&assetId=" + id.ToString();
                } else
                {
                    try { 
                        a.systemTypeInfo = await GetDTCore_SystemType(a.membershipNumber.Substring(0, 2));
                        var qr = await GetDTCoode_QRCodeURL(a.idNumber, a.membershipNumber.Substring(0, 2));
                        a.qrCodeUrl = qr.qrcodeurl;

                        if (a.qrCodeUrl.ToLower()=="unknown")
                        {
                            a.qrCodeUrl = "https://www.datatag.mobi/qrcsrm.aspx?id=" + a.idNumber + "&assetId=" + id.ToString();
                        }

                    }
                    catch
                    {
                       
                            a.qrCodeUrl = "https://www.datatag.mobi/qrcsrm.aspx?id=" + a.idNumber + "&assetId=" + id.ToString();


                       
                    }
                }
                //
                //a.qrCodeUrl = qr.qrcodeurl;

            }


            return a;
        }

        public async Task<Asset> PutAsset(Asset asset)
        {
            Asset userDetailsResponse = new Asset();
            //asset.modifiedBy = (string)HttpContext.Items["User"];

            StringContent content = new StringContent(Regex.Unescape(JsonConvert.SerializeObject(asset)), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            using (var response = await _client.PutAsync(_configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:PutAsset") + "(" + asset.id.ToString() + ")", content)) //, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                userDetailsResponse = JsonConvert.DeserializeObject<Asset>(apiResponse);

            }
            // proc should only return one row , but comes back as a list regardless from API
            return userDetailsResponse;
        }

        public async Task<List<Attachment>> GetAttachments(Attachment attachments)
        {
            GetAttachmentResponse attachmentResponse = new GetAttachmentResponse();

            StringContent content = new StringContent(JsonConvert.SerializeObject(attachments), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            using (var response = await _client.PostAsync(_configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:GetAttachments"), content)) //, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                attachmentResponse = JsonConvert.DeserializeObject<GetAttachmentResponse>(apiResponse);

            }
            // proc should only return one row , but comes back as a list regardless from API
            return attachmentResponse.Value;
        }

        public async Task<Attachment> PutAttachment(Attachment attachment)
        {
            Attachment attachmentResponse = new Attachment();

            //  asset.modifiedBy = (string)HttpContext.Items["User"];

            StringContent content = new StringContent(JsonConvert.SerializeObject(attachment), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            //using (var response = await _client.PutAsync(_configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:PutAttachment") + "(" + asset.ID.ToString() + ")", content)) //, content))
            using (var response = await _client.PostAsync(_configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:PutAttachment") , content)) //, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                attachmentResponse = JsonConvert.DeserializeObject<Attachment>(apiResponse);

            }
            // proc should only return one row , but comes back as a list regardless from API
            return attachmentResponse;
        }

        public async Task<Note> PutNote(Note attachment)
        {
            Note attachmentResponse = new Note();

            //  asset.modifiedBy = (string)HttpContext.Items["User"];

            StringContent content = new StringContent(JsonConvert.SerializeObject(attachment), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            //using (var response = await _client.PutAsync(_configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:PutAttachment") + "(" + asset.ID.ToString() + ")", content)) //, content))
            using (var response = await _client.PostAsync(_configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:PutNote"), content)) //, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                attachmentResponse = JsonConvert.DeserializeObject<Note>(apiResponse);

            }
            // proc should only return one row , but comes back as a list regardless from API
            return attachmentResponse;
        }

        public async Task<AssetIdentity> PutIdentity(AssetIdentity ai)
        {
            AssetIdentity attachmentResponse = new AssetIdentity();

            //  asset.modifiedBy = (string)HttpContext.Items["User"];

            StringContent content = new StringContent(JsonConvert.SerializeObject(ai), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var url = _configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:PutAssetIdentity") + "(" + ai.id + ")";
            //using (var response = await _client.PutAsync(_configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:PutAttachment") + "(" + asset.ID.ToString() + ")", content)) //, content))
            using (var response = await _client.PutAsync(url, content)) //, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                attachmentResponse = JsonConvert.DeserializeObject<AssetIdentity>(apiResponse);

            }
            // proc should only return one row , but comes back as a list regardless from API
            return attachmentResponse;
        }

        public async Task<AssetSystem> PutAssetSystem(AssetSystem ai)
        {
            AssetSystem attachmentResponse = new AssetSystem();

            //  asset.modifiedBy = (string)HttpContext.Items["User"];

            StringContent content = new StringContent(JsonConvert.SerializeObject(ai), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var url = _configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:PutAssetSystem") + "(" + ai.id + ")";
            //using (var response = await _client.PutAsync(_configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:PutAttachment") + "(" + asset.ID.ToString() + ")", content)) //, content))
            using (var response = await _client.PutAsync(url, content)) //, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                attachmentResponse = JsonConvert.DeserializeObject<AssetSystem>(apiResponse);

            }
            // proc should only return one row , but comes back as a list regardless from API
            return attachmentResponse;
        }

        public async Task<List<Note>> GetNotes(Note note)
        {
            GetNoteResponse aList = new GetNoteResponse();

            //   StringContent content = new StringContent(JsonConvert.SerializeObject(getAssetRequest), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var url = _configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:GetNotes") + "?$filter=key eq " + note.key.ToString() + " and system eq " + note.system + " and area eq " + note.area;

            using (var response = await _client.GetAsync(url)) //, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                aList = JsonConvert.DeserializeObject<GetNoteResponse>(apiResponse);

            }
            var f = new List<Note>();

            foreach (Note note1 in aList.Value)
            {
        
                var n2 = note1.notes?.Replace(@"\n", @"</br>");


                note1.notes = n2;
                f.Add(note1);
            }

            // frig

            return f;
        }

        public async Task<List<AssetStatusHistory>> GetAssetStatusHistory(long id)
        {
            GetStatusHistoryResponse aList = new GetStatusHistoryResponse();

            //   StringContent content = new StringContent(JsonConvert.SerializeObject(getAssetRequest), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var url = _configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:GetStatusHistory") + "?$filter=AssetId eq " + id.ToString();

            using (var response = await _client.GetAsync(url)) //, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                aList = JsonConvert.DeserializeObject<GetStatusHistoryResponse> (apiResponse);

            }

            return aList.Value;
        }

        public async Task<List<Activity>> GetActivity(GetActivityRequest getActivityRequest)
        {
            GetActivityResponse aList = new GetActivityResponse();

               StringContent content = new StringContent(JsonConvert.SerializeObject(getActivityRequest), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var url = _configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:GetActivity") + "?$orderby=ModifiedOn desc";//+ " ?$filter=AssetId eq " + id.ToString();

            using (var response = await _client.PostAsync(url, content ))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                aList = JsonConvert.DeserializeObject<GetActivityResponse>(apiResponse);

            }

            
            return aList.Value;
        }

        public async Task<List<RecentAllocation>> GetRecentAllocations(long userId )
        {
            var req = new { UserId = userId, };

            var res = new List<RecentAllocation>();

            StringContent content = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var url = _configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:GetRecentAllocations") ;//+ " ?$filter=AssetId eq " + id.ToString();

            using (var response = await _client.PostAsync(url, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
            //    aList = JsonConvert.DeserializeObject<GetActivityResponse>(apiResponse);
               res = JsonConvert.DeserializeObject<List<RecentAllocation>>(JObject.Parse(apiResponse).GetValue("value").ToString());


            }


            return res;
        }

        public async Task<List<Audit>> GetRecentAudits(long userId)
        {
            var req = new { UserId = userId, };

            var res = new List<Audit>();

            StringContent content = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var url = _configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:GetRecentAudits");//+ " ?$filter=AssetId eq " + id.ToString();

            using (var response = await _client.PostAsync(url, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                //    aList = JsonConvert.DeserializeObject<GetActivityResponse>(apiResponse);
                res = JsonConvert.DeserializeObject<List<Audit>>(JObject.Parse(apiResponse).GetValue("value").ToString());


            }


            return res;
        }

        public async Task<SystemType> GetDTCore_SystemType(string id)
        {
            GetSystemTypeResponse aList = new GetSystemTypeResponse();
            //$filter=TypeCode eq TC
            //   StringContent content = new StringContent(JsonConvert.SerializeObject(getAssetRequest), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("DTCore:apiUser") + ":" + _configuration.GetValue<string>("DTCore:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var url = _configuration.GetValue<string>("DTCore:APIEndPoint") + _configuration.GetValue<string>("DTCore:GetSystemType") + "?$filter=TypeCode eq " + id;

            using (var response = await _client.GetAsync(url)) //, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                aList = JsonConvert.DeserializeObject<GetSystemTypeResponse>(apiResponse);

                
            }

            return aList.Value.First();
        }

        public async Task<QrCode> GetDTCoode_QRCodeURL(string id , string systemType )
        {
            GetQRCodeResponse aList = new GetQRCodeResponse();
            GetQRCodeRequest aReq = new GetQRCodeRequest();

            aReq.IDNumber = id;
            aReq.SystemType = systemType;

            //$filter=TypeCode eq TC
              StringContent content = new StringContent(JsonConvert.SerializeObject(aReq), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("DTCodes:apiUser") + ":" + _configuration.GetValue<string>("DTCodes:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var url = _configuration.GetValue<string>("DTCodes:APIEndPoint") + _configuration.GetValue<string>("DTCodes:GetQrCodeUrl");

            using (var response = await _client.PostAsync(url, content)) //, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                aList = JsonConvert.DeserializeObject<GetQRCodeResponse>(apiResponse);

         
            }

            var a = aList.Value.First();

            return a;
        }

        public async Task<GeoLocationEvent> PostGeoLocationEvent(GeoLocationEvent req)
        {
            GeoLocationEvent attachmentResponse = new GeoLocationEvent();
            req.IPAddress = (req.IPAddress == null) ? "0.0.0.0" : req.IPAddress;
            req.QRCodeURL = (req.QRCodeURL == null) ? "na" : req.QRCodeURL;
            req.CreatedOn = DateTime.Now;
            req.WFStatus = (req.WFStatus == null) ? "N" : req.WFStatus;
            req.CreatedBy = (req.WFStatus == null) ? "UnKnown" : req.WFStatus;

            StringContent content = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
            //req.Latitude = (req.Latitude == null) ? 0 : req.Latitude;

            //'x-rssbus-authtoken' : '5x1S6d8z7X4w6b4S0v0y'

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("DTMobile:apiUser") + ":" + _configuration.GetValue<string>("DTMobile:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
           // _client.DefaultRequestHeaders.Add("x-rssbus-authtoken", "5x1S6d8z7X4w6b4S0v0y");

            //using (var response = await _client.PutAsync(_configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:PutAttachment") + "(" + asset.ID.ToString() + ")", content)) //, content))
            var url = _configuration.GetValue<string>("DTMobile:APIEndPoint") + _configuration.GetValue<string>("DTMobile:GetGeoLocationEvents") ;
            using (var response = await _client.PostAsync(url, content)) //, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                attachmentResponse = JsonConvert.DeserializeObject<GeoLocationEvent>(apiResponse);

            }
            // proc should only return one row , but comes back as a list regardless from API
            return attachmentResponse;
        }

        public async Task<List<GeoLocationEvent>> GetDTMobile_ScanEvents(GetScanRequest req)
        {
            GetScanResponse aList = new GetScanResponse();
            //GetQRCodeRequest aReq = new GetQRCodeRequest();

            //     aReq.IDNumber = id;
            //  aReq.SystemType = systemType;

            //$filter=TypeCode eq TC
            //  StringContent content = new StringContent(JsonConvert.SerializeObject(aReq), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("DTMobile:apiUser") + ":" + _configuration.GetValue<string>("DTMobile:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var url = _configuration.GetValue<string>("DTMobile:APIEndPoint") + _configuration.GetValue<string>("DTMobile:GetGeoLocationEvents") + "?$filter=";

            if (req.User != "")
            {
                url += "startswith(UserName,'" + req.User + "')&";

            }
            if (req.Application != "")
            {
                url += "startswith(Application,'" + req.Application + "')&";

            }

            if (req.AssetID != "")
            {
                url += "startswith(LookupCode,'" + req.AssetID + "')&";

            }

            if (req.SystemType != "")
            {
                url += "startswith(systemType,'" + req.SystemType + "')&";

            }


            //if (lookupCode != "ALL")
            //{
            //    url = url + "?$filter=LookupCode eq " + lookupCode + "&$orderby=createdon desc";
            //}
            //else
            //{
            url += "&$orderby=createdon desc";
            //}

            using (var response = await _client.GetAsync(url)) //, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                aList = JsonConvert.DeserializeObject<GetScanResponse>(apiResponse);


            }

            return aList.Value;
        }

        public async Task<List<GeoLocationEvent>> GetDTMobile_ScanEvents(string lookupCode)
        {
            GetScanResponse aList = new GetScanResponse();
            //GetQRCodeRequest aReq = new GetQRCodeRequest();

       //     aReq.IDNumber = id;
          //  aReq.SystemType = systemType;

            //$filter=TypeCode eq TC
          //  StringContent content = new StringContent(JsonConvert.SerializeObject(aReq), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("DTMobile:apiUser") + ":" + _configuration.GetValue<string>("DTMobile:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var url = _configuration.GetValue<string>("DTMobile:APIEndPoint") + _configuration.GetValue<string>("DTMobile:GetGeoLocationEvents");
            if (lookupCode != "ALL")
            {
                url = url + "?$filter=LookupCode eq " + lookupCode  + "&$orderby=createdon desc";
            }else
            {
                url = url + "?$orderby=createdon desc";
            }
          
            using (var response = await _client.GetAsync(url)) //, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                aList = JsonConvert.DeserializeObject<GetScanResponse>(apiResponse);


            }

            return aList.Value;
        }

        public async Task<List<AssetStatusHistory>> GetAssetStatusHistory_SQL(long id)
       // public List<AssetStatusHistory> GetAssetStatusHistory(long id)
        {
            List<AssetStatusHistory> aList = new List<AssetStatusHistory>();
            string commandText = @"SELECT * FROM [dbo].[vwKISTAssetStatusHistory] where assetid = @assetId ";

            using (var connection = new SqlConnection(_connStr))
            {
                await connection.OpenAsync();   //vs  connection.Open();
                using (var tran = connection.BeginTransaction())
                {
                    using (var command = new SqlCommand(commandText, connection, tran))
                    {
                        try
                        {
                            command.Parameters.Add("@assetId", SqlDbType.BigInt);
                            command.Parameters["@assetId"].Value = id;

                            SqlDataReader rdr = await command.ExecuteReaderAsync();  // vs also alternatives, command.ExecuteReader();  or await command.ExecuteNonQueryAsync();

                            while (rdr.Read())
                            {
                                var itemContent = new AssetStatusHistory();
                                // assetid , assetstatusid , assetstatus , modifiedon , modifiedby
                                //itemContent.id = (long)rdr["id"];
                                itemContent.assetId = (long)rdr["assetid"];
                                itemContent.assetstatusId = (long)rdr["assetstatusId"];
                                itemContent.assetstatus = rdr["assetstatus"].ToString();
                                itemContent.reasonForChange = rdr["reasonForChange"].ToString();

                                itemContent.modifiedBy = rdr["modifiedby"].ToString();
                                itemContent.modifiedOn = (DateTime)rdr["modifiedon"];


                                aList.Add(itemContent);
                            }
                            await rdr.CloseAsync();
                        }
                        catch (Exception Ex)
                        {
                            await connection.CloseAsync();
                            string msg = Ex.Message.ToString();
                            tran.Rollback();
                            throw;
                        }
                    }
                }
            }

            

            return aList;

        }

        public void SaveActivity_SQL(long operatorId , string appArea , string username , string desc)
        // public List<AssetStatusHistory> GetAssetStatusHistory(long id)
        {
            List<AssetStatusHistory> aList = new List<AssetStatusHistory>();
            string commandText = @"INSERT INTO [dbo].[Activity]
                       (OperatorId,[ActivityKey]
                       ,[ApplicationArea]
                       ,[ModifiedOn]
                       ,[ModifiedBy]
                       ,[description]
                       ,[ItemKey])
                 VALUES
                       (@operatorId,''
                       ,@area
                       ,getdate()
                       ,@username
                       ,@desc
                       ,0)";

            using (var connection = new SqlConnection(_connStr))
            {
                connection.Open();   //vs  connection.Open();
                using (var tran = connection.BeginTransaction())
                {
                    using (var command = new SqlCommand(commandText, connection, tran))
                    {
                        try
                        {
                            command.Parameters.Add("@operatorId", SqlDbType.BigInt);
                            command.Parameters["@operatorId"].Value = operatorId;

                            command.Parameters.Add("@area", SqlDbType.NVarChar);
                            command.Parameters["@area"].Value = appArea;

                            command.Parameters.Add("@username", SqlDbType.NVarChar);
                            command.Parameters["@username"].Value = username;

                            command.Parameters.Add("@desc", SqlDbType.NVarChar);
                            command.Parameters["@desc"].Value = desc;

                            command.ExecuteNonQuery();  // vs also alternatives, command.ExecuteReader();  or await command.ExecuteNonQueryAsync();

                            //while (rdr.Read())
                            //{
                            //    var itemContent = new AssetStatusHistory();
                            //    // assetid , assetstatusid , assetstatus , modifiedon , modifiedby
                            //    //itemContent.id = (long)rdr["id"];
                            //    itemContent.assetId = (long)rdr["assetid"];
                            //    itemContent.assetstatusId = (long)rdr["assetstatusId"];
                            //    itemContent.assetstatus = rdr["assetstatus"].ToString();
                            //    itemContent.reasonForChange = rdr["reasonForChange"].ToString();

                            //    itemContent.modifiedBy = rdr["modifiedby"].ToString();
                            //    itemContent.modifiedOn = (DateTime)rdr["modifiedon"];


                            //    aList.Add(itemContent);
                            //}
                           // await rdr.CloseAsync();
                        }
                        catch (Exception Ex)
                        {
                            connection.Close();
                            string msg = Ex.Message.ToString();
                            tran.Rollback();
                            throw;
                        }
                    }
                }
            }



           

        }

        public async Task<String> Test2()
        {
            try
            {
                var _ClientConfig = new ClientConfig
                {
                 //   Endpoint = _Endpoint,
                    ContentType = "application/json",
                 //   APIUserName = _APIUserName,
                  //  APIPassword = _APIPassword,
                  //  APIToken = _APIToken,
                  //  EmailRecipient = _EmailRecipient,
                  //  EmailRegex = _EmailRegex
                };

                //var _APIEndpoint = _configuration.GetValue<string>("email:APIEndPoint"); // ConfigurationManager.AppSettings["APIEndPoint"];

            //    _ClientConfig.EmailRecipient = emailRecipient;
                _ClientConfig.HttpMethod = "POST";
                _ClientConfig.Destination = "DTDEAD";
                _ClientConfig.DatabaseName = "DTKIST";
                _ClientConfig.EventSourceID = 25;
                _ClientConfig.EventTypeID = 401;
                _ClientConfig.EventAdviceID = 45;
                //_ClientConfig.FieldData = "DTXXXX";
                _ClientConfig.KeyName = "keyName";
                _ClientConfig.KeyValue = "subject";
                _ClientConfig.MembershipType = "";
                _ClientConfig.Mute = true;
                _ClientConfig.OperatorID = "";
                _ClientConfig.RecordID = "";
                _ClientConfig.SnoozeUntil = "";
                _ClientConfig.TableField = "";
                _ClientConfig.TableName = "";
                _ClientConfig.UserName = "";
                _ClientConfig.CreatedBy = "DTKistUser";
                _ClientConfig.ObjectName = "EventData";
                _ClientConfig.Report = "body";

                var response = await PostDTDead(_ClientConfig);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                string innerException = ex.InnerException == null ? string.Empty : ex.InnerException.ToString();
                if (!string.IsNullOrWhiteSpace(innerException)) error += ": " + ex.InnerException;
                // _ExceptionClient.SendEmail(_ClientEmailConfig, "An API Error has occurred: " + error);
            }

            return "";
        }

        public async Task<String> PostDTDead(ClientConfig req)
        {

            req.ContentType = "application/json";

            req.EmailRecipient = _configuration.GetValue<string>("email:EmailRecipient"); ;

            req.HttpMethod = "POST";
            req.Destination = _configuration.GetValue<string>("dtdead:Destination");
            req.DatabaseName = _configuration.GetValue<string>("dtdead:DatabaseName");
            req.EventSourceID = _configuration.GetValue<int>("dtdead:EventSourceID"); //25
            req.EventTypeID = _configuration.GetValue<int>("dtdead:EventTypeID"); //401;
            req.EventAdviceID = _configuration.GetValue<int>("dtdead:EventAdviceID"); //45;
            //_ClientConfig.FieldData = "DTXXXX";
            req.KeyName = _configuration.GetValue<string>("dtdead:KeyName");  //"keyName";
            //req.KeyValue = _configuration.GetValue<string>("dtdead:KeyValue");  //"subject";
            req.MembershipType = "";
            req.Mute = false;
            req.OperatorID = "";
            req.RecordID = "IDNumber";
            req.SnoozeUntil = "";
            req.TableField = "";
            req.TableName = "";
            //req.UserName = "";
            req.CreatedBy = _configuration.GetValue<string>("dtdead:CreatedBy");  // "DTKistUser";
            req.ObjectName = _configuration.GetValue<string>("dtdead:ObjectName");  //"EventData";
            req.Report = "QR Scan from KIST Mobile using ID:"+req.KeyValue + " was not located";  //"body";


            var res = new List<APIReturn>();
            StringContent content = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("dtdead:APIUserName") + ":" + _configuration.GetValue<string>("dtdead:APIPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            using (var response = await _client.PostAsync(_configuration.GetValue<string>("dtdead:Endpoint").Replace("[TokenContext]", _configuration.GetValue<string>("dtdead:APIToken")), content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<List<APIReturn>>(JObject.Parse(apiResponse).GetValue("value").ToString());

                // log into DTDEAD / sends emails

            }

            //return res.First();
            return "success";

        }

        public async Task<List<CustomReport>> GetCustomReports(string userName)
        {
            CustomReportResponse reportsResponse = new CustomReportResponse();

            StringContent content = new StringContent(JsonConvert.SerializeObject(new { Username = userName }), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            using (var response = await _client.PostAsync(_configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:GetCustomReports"), content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                reportsResponse = JsonConvert.DeserializeObject<CustomReportResponse>(apiResponse);
            }

            return reportsResponse.value;
        }

        private string generateJwtToken(MembershipUser user, LoginRequest loginReq)
        {
            //MembershipUser
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("TokenAuthentication:Secret"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user._ProviderUserKey), new Claim("userName", loginReq.username) }),
                Expires = DateTime.UtcNow.AddDays(99),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}