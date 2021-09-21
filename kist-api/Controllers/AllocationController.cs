using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using kist_api.Model;
using kist_api.Model.allocation;
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


    public class AllocationController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;
        readonly IConfiguration _configuration;
        readonly IKistService _kistService;

        public AllocationController(ILogger<AccountController> logger, IConfiguration configuration , IKistService kistService)
        {
            _logger = logger;
            _configuration = configuration;
            _kistService = kistService;
        }

        [Authorize]
        [HttpGet("RecentAllocations")]
        public async Task<List<RecentAllocation>> RecentAllocations()
        {
            var userId = (string)HttpContext.Items["User"];

            UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            userDetailsRequest.id = userId;

            var userDetails = await _kistService.UsersDetails(userDetailsRequest);


            return await _kistService.GetRecentAllocations(userDetails.ID);
        }

        [Authorize]
        [HttpGet("RecentAudits")]
        public async Task<List<Audit>> RecentAudits()
        {
            var userId = (string)HttpContext.Items["User"];

            UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            userDetailsRequest.id = userId;

            var userDetails = await _kistService.UsersDetails(userDetailsRequest);


            return await _kistService.GetRecentAudits(userDetails.ID);
        }


        [Authorize]
        [HttpPost("SetAllocationAudit")]
        public async Task<SetAllocationAuditResponse> SetAllocationAudit(SetAllocationAuditRequest req)
        {
            var userId = (string)HttpContext.Items["User"];

            UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            userDetailsRequest.id = userId;

            var userDetails = await _kistService.UsersDetails(userDetailsRequest);
            req.userid = 20060;
            req.assetId = 1;

            return await _kistService.SetAllocationAudit(req);
        }

        [Authorize]
        [HttpPost("CreateAudit")]               
        public async Task<CreateAuditResponse> CreateAudit(CreateAuditRequest req)
        {
            _logger.LogInformation(@"Creat Audit");
            var userId = (string)HttpContext.Items["User"];

            UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            userDetailsRequest.id = userId;

            var userDetails = await _kistService.UsersDetails(userDetailsRequest);
            req.userId = userDetails.ID;
            _logger.LogInformation(@"user Id:" + req.userId.ToString());
            return await _kistService.CreateAudit(req);

        }
        //[Route("UsersDetails")]
        //[HttpPost]
        [Authorize]

        [HttpGet("GetTemplates")]
        public  String GetTemplates()
        {

           // String filePath = HttpContext.Server.MapPath("~/App_Data/allocationTemplates.json");

            var JSON = System.IO.File.ReadAllText("allocationTemplates.json");

            return JSON;

            //return JsonConvert.DeserializeObject<List<AllocationTemplate>>(JSON);

          //  // should be via company id or user id 
          //  var userId = (string)HttpContext.Items["User"];

          //  UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
          //  userDetailsRequest.id = userId;
           
          ////  var userDetails = await _kistService.UsersDetails(userDetailsRequest);


          //  var tList = new List<AllocationTemplate>() ;


          //  // tempalte 1 

          //  var step1 = new AllocationStep(){ label = "Scan Asset" , assetTypes = "Equipment" , multiple = false};
          //  var step2 = new AllocationStep() { label = "Scan Gang Code"  ,assetTypes = "Van" };
          //  var t = new AllocationTemplate();
          //  t.name = "Collecting Asset from Yard";
          //  t.description = "Gang Leader goes to yard to collect a new asset, to be allocated to his gang";
          //  t.steps.Add(step1);
          //  t.steps.Add(step2);
          //  tList.Add(t);


          //  // template 2 
          //  step1 = new AllocationStep() { label = "Van QR Code Scanned", assetTypes = "Vehicle",  multiple = true };
          //  step2 = new AllocationStep() { label = "Scan Gang Code" , assetTypes = "Equipment" };
          //  t = new AllocationTemplate();
          //  t.name = "Assign Van to Gang";
          //  t.description = "Assign a Van that already has assets to new gang , could be already assigned to a previous gang";
          //  t.steps.Add(step1);
          //  t.steps.Add(step2);
          //  tList.Add(t);


          //  UserDetailsRequest userDetailsRequest2 = new UserDetailsRequest();
          //  //userDetailsRequest2.id = userDetails.ID.ToString();

          //  return tList;

        }

      
    }
}
