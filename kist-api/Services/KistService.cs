using kist_api.Model;
using kist_api.Model.dtcusid;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

namespace kist_api.Services
{
    public class KistService : IKistService
    {
        private const string BaseUrl = "https://jsonplaceholder.typicode.com/todos/";
        private readonly HttpClient _client;
        readonly IConfiguration _configuration;
        public string _connStr = String.Empty;

        public KistService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
            _connStr = _configuration.GetConnectionString("DevConnection");
        }

        public async Task<LoginResponse> Login(LoginRequest loginReq)
        {
            LoginResponse loginRes = new LoginResponse();
          
            StringContent content = new StringContent(JsonConvert.SerializeObject(loginReq), Encoding.UTF8, "application/json");

            using (var response = await _client.PostAsync(_configuration.GetValue<string>("AuthEndPoint"), content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                loginRes = JsonConvert.DeserializeObject<LoginResponse>(apiResponse);

                
                if (loginRes.userDetails != null )
                {
                    // fetch userdetails to find company
                   // UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
                    //userDetailsRequest.id = loginRes.userDetails._ProviderUserKey.ToString();

                  //  UserDetails userDetails = await UsersDetails(userDetailsRequest);

                    if (loginRes.userDetails._IsApproved)
                    {
                        loginRes.userDetails.token = generateJwtToken(loginRes.userDetails);
                    } else
                    {
                        loginRes.response = "User not approved";
                    }
                    //  loginRes.userDetails.token = generateJwtToken(loginRes.userDetails);
                       

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
            AssetIdentity assetIndentity = new AssetIdentity();

            //   StringContent content = new StringContent(JsonConvert.SerializeObject(getAssetRequest), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var url = _configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:GetAssetIdentity") + "(" + id.ToString() + ")";

            using (var response = await _client.GetAsync(url)) //, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                assetIndentity = JsonConvert.DeserializeObject<AssetIdentity>(apiResponse);

            }
            // proc should only return one row , but comes back as a list regardless from API
            //perform post prod validation

            //if (userDetailsResponse.companyID != companyId)
            //{
            //    // requested asset for in correct company id , fail ! 

            //}
            return assetIndentity;
        }
        public async Task<AssetSystem> GetAssetSystem(long id)
        {
            AssetSystem assetsystem = new AssetSystem();

            //   StringContent content = new StringContent(JsonConvert.SerializeObject(getAssetRequest), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var url = _configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:GetAssetSystem") + "(" + id.ToString() + ")";

            using (var response = await _client.GetAsync(url)) //, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                assetsystem = JsonConvert.DeserializeObject<AssetSystem>(apiResponse);

            }

            return assetsystem;
        }


        public async Task<Asset> PutAsset(Asset asset)
        {
            Asset userDetailsResponse = new Asset();

          //  asset.modifiedBy = (string)HttpContext.Items["User"];

            StringContent content = new StringContent(JsonConvert.SerializeObject(asset), Encoding.UTF8, "application/json");

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

        public async Task<List<AssetStatusHistory>> GetAssetStatusHistory(long id)
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


        private string generateJwtToken(MembershipUser user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("TokenAuthentication:Secret"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user._ProviderUserKey) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
