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

namespace kist_api.Services
{
    public class KistService : IKistService
    {
        private const string BaseUrl = "https://jsonplaceholder.typicode.com/todos/";
        private readonly HttpClient _client;
        readonly IConfiguration _configuration;


        public KistService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<LoginResponse> Login(LoginRequest loginReq)
        {
            LoginResponse loginRes = new LoginResponse();
          
            StringContent content = new StringContent(JsonConvert.SerializeObject(loginReq), Encoding.UTF8, "application/json");

            using (var response = await _client.PostAsync(_configuration.GetValue<string>("AuthEndPoint"), content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                loginRes = JsonConvert.DeserializeObject<LoginResponse>(apiResponse);

                
                if (loginRes.userDetails._IsApproved )
                {
                    // fetch userdetails to find company
                   // UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
                    //userDetailsRequest.id = loginRes.userDetails._ProviderUserKey.ToString();

                  //  UserDetails userDetails = await UsersDetails(userDetailsRequest);

                  
                    //  loginRes.userDetails.token = generateJwtToken(loginRes.userDetails);
                    loginRes.userDetails.token = generateJwtToken(loginRes.userDetails);
                        
                    

                       

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

        public async Task<List<Asset>> GetAssets()
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

        public async Task<List<Asset>> GetAssetsByUser(UserDetailsRequest userDetailsRequest)
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

        public async Task<Asset> PutAsset(Asset asset)
        {
            Asset userDetailsResponse = new Asset();

          //  asset.modifiedBy = (string)HttpContext.Items["User"];

            StringContent content = new StringContent(JsonConvert.SerializeObject(asset), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            using (var response = await _client.PutAsync(_configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:PutAsset") + "(" + asset.ID.ToString() + ")", content)) //, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                userDetailsResponse = JsonConvert.DeserializeObject<Asset>(apiResponse);

            }
            // proc should only return one row , but comes back as a list regardless from API
            return userDetailsResponse;
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
