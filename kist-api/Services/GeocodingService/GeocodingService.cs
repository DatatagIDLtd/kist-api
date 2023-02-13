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
using System.Net;
using System.IO;

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
            var endpoint = _configuration.GetValue<string>("geoCoding:WTWEndPoint").Replace("[Lat]", request.Latitude.ToString()).Replace("[Long]", request.Longitude.ToString()).Replace("[Token]", _configuration.GetValue<string>("geoCoding:WTWToken"));
            var wtwRequest = WebRequest.Create(endpoint);
            wtwRequest.Method = "GET";
            WebResponse wtwResponse = wtwRequest.GetResponse();
            Stream wtwStream = wtwResponse.GetResponseStream();
            var wtwReader = new StreamReader(wtwStream, Encoding.UTF8);
            var wtwResponseString = wtwReader.ReadToEnd();
            return JsonConvert.DeserializeObject<GetWTWResponse>(JObject.Parse(wtwResponseString).ToString());
        }

        public GetAddressResponse GetAddress(GeocodingRequestModel request)
        {
            var endpoint = _configuration.GetValue<string>("geoCoding:GoogleEndPoint").Replace("[Lat]", request.Latitude.ToString()).Replace("[Long]", request.Longitude.ToString()).Replace("[Token]", _configuration.GetValue<string>("geoCoding:GoogleToken"));
            var geoRequest = WebRequest.Create(endpoint);
            geoRequest.Method = "GET";
            WebResponse geoResponse = geoRequest.GetResponse();
            Stream geoStream = geoResponse.GetResponseStream();
            var reader = new StreamReader(geoStream, Encoding.UTF8);
            var geoResponseString = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<GetAddressResponse>(JObject.Parse(geoResponseString).ToString());
        }
    }
}