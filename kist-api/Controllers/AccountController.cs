using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using kist_api.Model;
using kist_api.Model.dashboard;
using kist_api.Model.dtcusid;
using kist_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace kist_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;
        readonly IConfiguration _configuration;
        readonly IKistService _kistService;

        public AccountController(ILogger<AccountController> logger, IConfiguration configuration , IKistService kistService)
        {
            _logger = logger;
            _configuration = configuration;
            _kistService = kistService;
        }


        [Route("Login")]
        [HttpPost]
        public Task<LoginResponse> Login(LoginRequest loginReq)

        {

            return _kistService.Login(loginReq);

            //LoginResponse loginRes = new LoginResponse();

            //using (var httpClient = new HttpClient())
            //{
            //    StringContent content = new StringContent(JsonConvert.SerializeObject(loginReq), Encoding.UTF8, "application/json");

            //    using (var response = await httpClient.PostAsync(_configuration.GetValue<string>("AuthEndPoint"), content))
            //    {
            //        string apiResponse = await response.Content.ReadAsStringAsync();
            //        loginRes = JsonConvert.DeserializeObject<LoginResponse>(apiResponse);
            //    }
            //}
            //return loginRes;
        }

        [Route("UsersDetails")]
        [HttpPost]
        public Task<UserDetails> UsersDetails( UserDetailsRequest userDetailsRequest)
        {
            return _kistService.UsersDetails(userDetailsRequest);

        }

        [Authorize]

        [Route("Dashboard")]
        [HttpPost]
        public async Task<Dashboard> Dashboard(UserDetailsRequest userDetailsRequest)
        {
            var userId = (string)HttpContext.Items["User"];
            UserDetailsRequest userDetailsRequest2 = new UserDetailsRequest();
            userDetailsRequest2.id = userId;

            var userDetails = await _kistService.UsersDetails(userDetailsRequest2);





            ////UserDetailsRequest userDetailsRequest3 = new UserDetailsRequest();
            //    userDetailsRequest.id = userDetails.ID.ToString();

            return await _kistService.Dashboard(userDetailsRequest);

        }


    }
}
