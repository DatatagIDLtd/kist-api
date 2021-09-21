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
    public class ConsumableController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;
        readonly IConfiguration _configuration;
        readonly IKistService _kistService;
        readonly IConsumableService _consumableService;

        public ConsumableController(ILogger<AccountController> logger, IConfiguration configuration , IKistService kistService, IConsumableService consumableService)
        {
            _logger = logger;
            _configuration = configuration;
            _kistService = kistService;
            _consumableService = consumableService;

        }


        
         [Authorize]

        [Route("GetAssetConsumables/{id}")]
        // [HttpGet("{search}")]
        public async Task<List<Consumable>> GetAssetConsumables(long id)
        {
            // should be via company id or user id 
            var userId = (string)HttpContext.Items["User"];

            UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            userDetailsRequest.id = userId;


            return await _consumableService.GetAssetConsumables(id);

            }

        //   public async Task<Consumable> SetAssetConsumable(SetAssetConsumableRequest req)

        [Route("SetAssetConsumable")]
        [HttpPost]
        public async Task<Consumable> SetAssetConsumable(SetAssetConsumableRequest req)
        {
            // should be via company id or user id 
            var userId = (string)HttpContext.Items["User"];

            UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            userDetailsRequest.id = userId;

            var userDetails = await _kistService.UsersDetails(userDetailsRequest);

            req.userid = userDetails.ID;

            userDetailsRequest.id = userDetails.ID.ToString();

            return await _consumableService.SetAssetConsumable(req);

        }

        [Route("CreateConsumableAudit")]
        [HttpPost]
        public async Task<Consumable> CreateConsumableAudit(CreateConsumableAuditRequest req)
        {
            // should be via company id or user id 
            var userId = (string)HttpContext.Items["User"];

            UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            userDetailsRequest.id = userId;

            var userDetails = await _kistService.UsersDetails(userDetailsRequest);

            req.userid = userDetails.ID;

            userDetailsRequest.id = userDetails.ID.ToString();

            return await _consumableService.CreateConsumableAudit(req);

        }


    }
}
