using kist_api.Helper.ApiResponse;
using kist_api.Model;
using kist_api.Model.Geocoding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Services
{
    public interface IGeocodingService
    {
        GetWTWResponse GetWTW(GeocodingRequestModel request);
        GetAddressResponse GetAddress(GeocodingRequestModel request);
    }
}
