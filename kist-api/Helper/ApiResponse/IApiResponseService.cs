using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace kist_api.Helper.ApiResponse
{
    public interface IApiResponseService
    {
        Task<ApiResponseModel> ApiResponse<T>(HttpResponseMessage apiResult) where T : class;
    }
}
