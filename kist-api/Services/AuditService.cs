﻿using kist_api.Model;
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
    public class AuditService : IAuditService
    {
        private const string BaseUrl = "https://jsonplaceholder.typicode.com/todos/";
        private readonly HttpClient _client;
        readonly IConfiguration _configuration;
        public string _connStr = String.Empty;
        private readonly ILogger<AuditService> _logger;

        public AuditService(HttpClient client, IConfiguration configuration, ILogger<AuditService> logger)
        {
            _client = client;
            _configuration = configuration;
            _connStr = _configuration.GetConnectionString("DevConnection");
            _logger = logger;
        }

        public async Task<Audit> GetAudit(GetAuditRequest req)
        {
   

            var res = new Audit();
            StringContent content = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var url = _configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:GetAudit");

            using (var response = await _client.PostAsync(url, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<List<Audit>>(JObject.Parse(apiResponse).GetValue("value").ToString()).First();


            }


            return res;
        }
        
    }


}