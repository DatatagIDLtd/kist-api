using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace kist_api.Helper.ApiResponse
{
    public static class ApiResponseService 
    {
        public static ApiResponseModel Create(object apiResult,HttpStatusCode statusCode)
        {
            try
            {
                var apiResponse = new ApiResponseModel();

                if (statusCode == System.Net.HttpStatusCode.OK)
                {

                    apiResponse.Value = apiResult;
                    apiResponse.ErrorMessage = null;
                    apiResponse.IsSucceded = true;

                }
                else
                {
                    apiResponse.Value = null;
                    apiResponse.ErrorMessage =statusCode.ToString();
                    apiResponse.IsSucceded = false;
                }

                return apiResponse;
            }

            catch (Exception ex)
            {
                throw new Exception("Something went wrong. Please try again. Error message: ",ex);

            }
               
        }
    }
}
