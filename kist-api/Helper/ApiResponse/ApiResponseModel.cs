using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Helper.ApiResponse
{
    public class ApiResponseModel
    {
        public bool IsSucceded { get; set; }

        public string ErrorMessage { get; set; }

        public object Data { get; set; }
    }
}
