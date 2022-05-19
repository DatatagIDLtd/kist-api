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
using kist_api.Services.RealtimeService;
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

        private readonly IRealtimeService _realtimeService;

        public AccountController(ILogger<AccountController> logger, IConfiguration configuration , IKistService kistService, IRealtimeService realtimeService)
        {
            _logger = logger;
            _configuration = configuration;
            _kistService = kistService;
            _realtimeService = realtimeService;
        }

        [Route("Login")]
        [HttpPost]
        public Task<LoginResponse> Login(LoginRequest loginReq)
        {

            _realtimeService.NotifyAssetsUpdated();

            _logger.LogInformation(@"Controller\Account\Login(Post)");
            return _kistService.Login(loginReq);
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


            userDetailsRequest.id = userDetails.ID.ToString() ;


            ////UserDetailsRequest userDetailsRequest3 = new UserDetailsRequest();
            //    userDetailsRequest.id = userDetails.ID.ToString();

            return await _kistService.Dashboard(userDetailsRequest);

        }

        [Authorize]
        [Route("GetMobileDashboard")]
        [HttpGet]
        public async Task<Dashboard> GetMobileDashboard()
        {
            var userId = (string)HttpContext.Items["User"];
            UserDetailsRequest userDetailsRequest2 = new UserDetailsRequest();
            userDetailsRequest2.id = userId;

            var userDetails = await _kistService.UsersDetails(userDetailsRequest2);

            var userDetailsRequest = new UserDetailsRequest();
            userDetailsRequest.id = userDetails.ID.ToString();


            ////UserDetailsRequest userDetailsRequest3 = new UserDetailsRequest();
            //    userDetailsRequest.id = userDetails.ID.ToString();

            return await _kistService.GetMobileDashboard(userDetailsRequest);

        }


        [Authorize]

        [Route("Activity")]
        [HttpPost]
        public async Task<List<Activity>> Activity(GetActivityRequest req)
        {
            //var userId = (string)HttpContext.Items["User"];
            //UserDetailsRequest userDetailsRequest2 = new UserDetailsRequest();
            //userDetailsRequest2.id = userId;

            //var userDetails = await _kistService.UsersDetails(userDetailsRequest2);


            //userDetailsRequest.id = userDetails.ID.ToString();


            ////UserDetailsRequest userDetailsRequest3 = new UserDetailsRequest();
            //    userDetailsRequest.id = userDetails.ID.ToString();
            /*var req = new GetActivityRequest();
*/
            req.operatorId = 1;
          //  req.maxrows = 2;
            req.fromDate = DateTime.Now;
            req.toDate = DateTime.Now;
            return await _kistService.GetActivity(req);

        }


        [Authorize]
        [HttpGet("GetClancyAssets")]
        public List<Asset> GetClancyAssets()
        {
            // String filePath = HttpContext.Server.MapPath("~/App_Data/allocationTemplates.json");
            var JSON = System.IO.File.ReadAllText("clancyAssets.json");
            return JsonConvert.DeserializeObject<List<Asset>>(JSON);
        }

    }
}
