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
using kist_api.Helper.ApiResponse;

namespace kist_api.Services
{
    public class ScanService : IScanService
    {
        private const string BaseUrl = "https://jsonplaceholder.typicode.com/todos/";
        private readonly HttpClient _client;
        readonly IConfiguration _configuration;
        public string _connStr = String.Empty;
        private readonly ILogger<ScanService> _logger;

        public ScanService(HttpClient client, IConfiguration configuration, ILogger<ScanService> logger)
        {
            _client = client;
            _configuration = configuration;
            _connStr = _configuration.GetConnectionString("DevConnection");
            _logger = logger;
        }

        public async Task<List<GeoLocationEventMapFlag>> GetScansByLocation(GetScanByLocationRequest req)
        {
   

            var res = new List<GeoLocationEventMapFlag>();
            StringContent content = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("DTMobile:apiUser") + ":" + _configuration.GetValue<string>("DTMobile:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var url = _configuration.GetValue<string>("DTMobile:APIEndPoint") + _configuration.GetValue<string>("DTMobile:GetScansByLocation");

            using (var response = await _client.PostAsync(url, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<List<GeoLocationEventMapFlag>>(JObject.Parse(apiResponse).GetValue("value").ToString());


            }

            return res;
        }

        public async Task<ApiResponseModel> GetScannedAssetDetails(ScanAssetDetailsRequestModel request)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("DTMobile:apiUser") + ":" + _configuration.GetValue<string>("DTMobile:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var url = _configuration.GetValue<string>("DTMobile:APIEndPoint") + _configuration.GetValue<string>("DTMobile:GetScannedAssetDetail");

            var apiResponse = new ApiResponseModel();
            using (var response = await _client.PostAsync(url, content))
            {
                var resultAsString = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject(JObject.Parse(resultAsString).GetValue("value").ToString());
                apiResponse = ApiResponseService.Create(result, response.StatusCode);

            }

            return apiResponse;
        }

        public async Task<ApiResponseModel> CreateGeoLocationEvents(ScanEventRequestModel request)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("DTMobile:apiUser") + ":" + _configuration.GetValue<string>("DTMobile:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var url = _configuration.GetValue<string>("DTMobile:APIEndPoint") + _configuration.GetValue<string>("DTMobile:GeoLocationEvents");

            var apiResponse = new ApiResponseModel();
            using (var response = await _client.PostAsync(url, content))
            {
                var resultAsString = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject(JObject.Parse(resultAsString).GetValue("value").ToString());
                apiResponse = ApiResponseService.Create(result, response.StatusCode);

            }

            return apiResponse;
        }

        public async Task<ApiResponseModel> GetAssetDocumentList(ScanAssetDetailsRequestModel request)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("DTMobile:apiUser") + ":" + _configuration.GetValue<string>("DTMobile:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var url = _configuration.GetValue<string>("DTMobile:APIEndPoint") + _configuration.GetValue<string>("DTMobile:GetAssetDocumentList");

            var apiResponse = new ApiResponseModel();
            using (var response = await _client.PostAsync(url, content))
            {
                var resultAsString = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject(JObject.Parse(resultAsString).GetValue("value").ToString());
                apiResponse = ApiResponseService.Create(result, response.StatusCode);

            }

            return apiResponse;
        }
    }


}
