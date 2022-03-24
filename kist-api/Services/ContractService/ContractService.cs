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
using Microsoft.AspNetCore.Http;

namespace kist_api.Services
{
    public class ContractService : IContractService
    {
        private const string BaseUrl = "https://jsonplaceholder.typicode.com/todos/";
        private readonly HttpClient _client;
        readonly IConfiguration _configuration;
        public string _connStr = String.Empty;
        private readonly ILogger<ContractService> _logger;


        public ContractService(HttpClient client, IConfiguration configuration, ILogger<ContractService> logger)
        {
            _client = client;
            _configuration = configuration;
            _connStr = _configuration.GetConnectionString("DevConnection");
            _logger = logger;
        }


        public async Task<List<Contract>> GetContract(ContractRequest req)
        {

            List<Contract> res;

            StringContent content = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));


            using (var response = await _client.PostAsync(_configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:GetContract"), content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                res = JsonConvert.DeserializeObject<List<Contract>>(JObject.Parse(apiResponse).GetValue("value").ToString());




            }

            return res;
        }

        public async Task<Contract> UpdateContract(Contract contract , String userName)
        {
            Contract contractResponse = new Contract();

            //@id bigint = null,
            //@OperatorId  bigint = 0,
            //@Reference nvarchar(20) = null,
            //@Name nvarchar(100) = null,
            //@CompanyId bigint = 0,
            //@StartDate date = null,
            //@EndDate date = null,
            //@Duration int = null,
            //@UserName nvarchar(50)

            if (contract.Duration == null) contract.Duration = 0;
            var req = new { id = contract.ID, OperatorId = contract.OperatorId , Reference = contract.Reference , Name = contract.Name , CompanyId = contract.CompanyId , StartDate = contract.StartDate , EndDate = contract.EndDate , Duration = contract.Duration , UserName  = userName };


            //  asset.modifiedBy = (string)HttpContext.Items["User"];

            StringContent content = new StringContent(Regex.Unescape(JsonConvert.SerializeObject(req)), Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("api:apiUser") + ":" + _configuration.GetValue<string>("api:apiPassword"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            using (var response = await _client.PostAsync(_configuration.GetValue<string>("api:APIEndPoint") + _configuration.GetValue<string>("api:UpdateContract") , content)) //, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                contractResponse = JsonConvert.DeserializeObject<Contract>(JObject.Parse(apiResponse).GetValue("value").First().ToString());

            }
            // proc should only return one row , but comes back as a list regardless from API
            return contractResponse;
        }

    }


}
