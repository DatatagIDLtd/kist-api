using kist_api.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using kist_api.Helper.ApiResponse;
using kist_api.Model.Geocoding;

namespace kist_api.Services
{
    public class GeocodingService : IGeocodingService
    {
        private const string BaseUrl = "https://jsonplaceholder.typicode.com/todos/";
        private readonly HttpClient _client;
        readonly IConfiguration _configuration;
        public string _connStr = String.Empty;
        private readonly ILogger<AuditService> _logger;

        public GeocodingService(HttpClient client, IConfiguration configuration, ILogger<AuditService> logger)
        {
            _client = client;
            _configuration = configuration;
            _connStr = _configuration.GetConnectionString("DevConnection");
            _logger = logger;
        }

        public GetWTWResponse GetWTW(GeocodingRequestModel request)
        {
            var getWTWResponse = new GetWTWResponse();
            var endpoint = _configuration.GetValue<string>("geoCoding:WTWEndPoint").Replace("[Lat]", request.Latitude.ToString()).Replace("[Long]", request.Longitude.ToString()).Replace("[Token]", _configuration.GetValue<string>("geoCoding:WTWToken"));
            using (var response = _client.GetAsync(endpoint))
            {
                var resultAsString = response.Result.Content.ToString();
                getWTWResponse = JsonConvert.DeserializeObject<GetWTWResponse>(JObject.Parse(resultAsString).GetValue("value").ToString());
            }

            return getWTWResponse;
        }

        public GetAddressResponse GetAddress(GeocodingRequestModel request)
        {
            var getAddressResponse = new GetAddressResponse();
            var endpoint = _configuration.GetValue<string>("geoCoding:GoogleEndPoint").Replace("[Lat]", request.Latitude.ToString()).Replace("[Long]", request.Longitude.ToString()).Replace("[Token]", _configuration.GetValue<string>("geoCoding:GoogleToken"));
            using (var response = _client.GetAsync(endpoint))
            {
                var resultAsString = response.Result.Content.ToString();
                getAddressResponse = JsonConvert.DeserializeObject<GetAddressResponse>(JObject.Parse(resultAsString).GetValue("value").ToString());
            }

            return getAddressResponse;
        }
    }
}