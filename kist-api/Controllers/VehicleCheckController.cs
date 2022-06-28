using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using kist_api.Model;
using kist_api.Model.dtcusid;
using kist_api.Model.dtmobile;
using kist_api.Models.Account;
using kist_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace kist_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleCheckController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;
        readonly IConfiguration _configuration;
        readonly IKistService _kistService;
        readonly IVehicleCheckService _VehicleCheckService;

        public VehicleCheckController(ILogger<AccountController> logger, IConfiguration configuration, IKistService kistService, IVehicleCheckService VehicleCheckService)
        {
            _logger = logger;
            _configuration = configuration;
            _kistService = kistService;
            _VehicleCheckService = VehicleCheckService;

        }



        [Authorize]

        [Route("GetAssetVehicleChecks/{id}")]
        // [HttpGet("{search}")]
        public async Task<List<VehicleCheck>> GetAssetVehicleChecks(long id)
        {
            // should be via company id or user id 
            var userId = (string)HttpContext.Items["User"];

            UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            userDetailsRequest.id = userId;


            return await _VehicleCheckService.GetAssetVehicleChecks(id);

        }

        //   public async Task<VehicleCheck> SetAssetVehicleCheck(SetAssetVehicleCheckRequest req)

        [Route("SetAssetVehicleCheck")]
        [HttpPost]
        public async Task<VehicleCheck> SetAssetVehicleCheck([FromBody] SetAssetVehicleCheckRequest req)
        {
            // should be via company id or user id 
            var userId = (string)HttpContext.Items["User"];

            UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            userDetailsRequest.id = userId;

            var userDetails = await _kistService.UsersDetails(userDetailsRequest);
            
            userDetailsRequest.id = userDetails.ID.ToString();

            return await _VehicleCheckService.SetAssetVehicleChecks(req);

        }

        [Route("CreateVehicleCheckAudit")]
        [HttpPost]
        public async Task<VehicleCheck> CreateVehicleCheckAudit(CreateVehicleCheckAuditRequest req)
        {
            // should be via company id or user id 
            var userId = (string)HttpContext.Items["User"];

            UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            userDetailsRequest.id = userId;

            var userDetails = await _kistService.UsersDetails(userDetailsRequest);

            req.userid = userDetails.ID;

            userDetailsRequest.id = userDetails.ID.ToString();

            return await _VehicleCheckService.CreateVehicleCheckAudit(req);

        }

    }
}
