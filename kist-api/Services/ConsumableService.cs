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

namespace kist_api.Services
{
    public class ConsumableService : IConsumableService
    {
        private const string BaseUrl = "https://jsonplaceholder.typicode.com/todos/";
        private readonly HttpClient _client;
        readonly IConfiguration _configuration;
        public string _connStr = String.Empty;
        private readonly ILogger<ConsumableService> _logger;

        public ConsumableService(HttpClient client, IConfiguration configuration, ILogger<ConsumableService> logger)
        {
            _client = client;
            _configuration = configuration;
            _connStr = _configuration.GetConnectionString("DevConnection");
            _logger = logger;
        }

        //
        public async Task<Consumable> CreateConsumableAudit(CreateConsumableAuditRequest req)
        {
        
              var res = new Consumable();
            StringContent content = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var url = _configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:CreateConsumableAudit" );

            using (var response = await _client.PostAsync(url, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<List<Consumable>>(JObject.Parse(apiResponse).GetValue("value").ToString()).First();


            }


            return res;

        }

        public async Task<List<Consumable>> GetAssetConsumables(long id)
        {

            var req = new { assetId = id, userId = 0};

            var res = new List<Consumable>();
            StringContent content = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var url = _configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:GetAssetConsumables");

            using (var response = await _client.PostAsync(url, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<List<Consumable>>(JObject.Parse(apiResponse).GetValue("value").ToString());


            }


            return res;
        }

        public async Task<Consumable> SetAssetConsumable(SetAssetConsumableRequest req)
        {
            if (req.consumableId == null) { req.consumableId = 0; };
            if (req.assetConsumableId == null) { req.assetConsumableId = 0; };
            if (req.assetId == null) { req.assetId = 0; };
        


            var res = new Consumable();
            StringContent content = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var url = _configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:SetAssetConsumable");

            using (var response = await _client.PostAsync(url, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<List<Consumable>>(JObject.Parse(apiResponse).GetValue("value").ToString()).First();


            }


            return res;

        }

    }


}
