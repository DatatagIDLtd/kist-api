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
    public class InventoryController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;
        readonly IConfiguration _configuration;
        readonly IKistService _kistService;

        public InventoryController(ILogger<AccountController> logger, IConfiguration configuration , IKistService kistService)
        {
            _logger = logger;
            _configuration = configuration;
            _kistService = kistService;
        }

        //[Route("UsersDetails")]
        //[HttpPost]
        [Authorize]
        [Route("Search/")]
        public async Task<List<AssetView>> Search(GetAssetRequest asset)
        {
            // should be via company id or user id 
            var userId = (string)HttpContext.Items["User"];
            UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            userDetailsRequest.id = userId;
            var userDetails = await _kistService.UsersDetails(userDetailsRequest);

            var resultList = new List<AssetView>();

            //UserDetailsRequest userDetailsRequest2 = new UserDetailsRequest();
            //userDetailsRequest2.id = userDetails.ID.ToString();
            //userDetailsRequest2.searchQuery = search;
            asset.userId = userDetails.ID; // fudge for now to pass in user id 
            var inventoryList =  await _kistService.GetInventoryByUser(asset);

            if(inventoryList?.Count > 0)
            {
                if (asset.assetTypeID == 2 || asset.assetTypeID == 3)
                {
                    foreach (var inventoryAsset in inventoryList)
                    {
                        resultList.Add(inventoryAsset);

                        if (inventoryAsset.assetTypeID == 1 && inventoryAsset.containsItems)
                        {
                            var boxInventoryList = await _kistService.GetInventoryByUser(new GetAssetRequest
                            {
                                userId = userDetails.ID,
                                parentId = inventoryAsset.id,
                                siteId = 0,
                                uniqueID = "",
                                location = "",
                                status = "",
                                name = ""
                            });

                            resultList.AddRange(boxInventoryList);
                        }
                    }
                }
                else
                {
                    resultList.AddRange(inventoryList);
                }
               
            }

            return resultList;

        }

        [Authorize]
        [HttpGet("{id}")]
        public  Task<Site> Get(long id)
        {
            var userId = (string)HttpContext.Items["User"];
            //UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            //userDetailsRequest.id = userId;
            //var userDetails = await _kistService.UsersDetails(userDetailsRequest);
            return _kistService.GetSite(id);
        }

        [Authorize]
        // PUT: api/Default/5
        [HttpPut("{id}")]
        public async Task<Site> Put(int id, [FromBody] Site site)
        {
            var userId = (string)HttpContext.Items["User"];
            UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            userDetailsRequest.id = userId;
            var userDetails = await _kistService.UsersDetails(userDetailsRequest);

            site.modifiedBy = userDetails.Forename + " " + userDetails.Surname;
            site.modifiedOn = DateTime.Now;
            return await _kistService.PutSite(site);
        }

        [HttpGet("ScanEvents/{codeLookup}")]
        public async Task<List<GeoLocationEvent>> Get(string codeLookup)
        {
            var userId = (string)HttpContext.Items["User"];
            //UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            //userDetailsRequest.id = userId;
            //var userDetails = await _kistService.UsersDetails(userDetailsRequest);
            return await _kistService.GetDTMobile_ScanEvents(codeLookup);
        }
        
        [HttpPost("Allocate")]
        public async Task<CreateAllocationResponse> Allocate(CreateAllocationRequest req)
        {
            var userId = (string)HttpContext.Items["User"];
            UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            userDetailsRequest.id = userId;
            var userDetails = await _kistService.UsersDetails(userDetailsRequest);
            // loop through assets to be allocated
            var res = new CreateAllocationResponse();

            foreach (long id in req.AssetID)
            {
                await _kistService.CreateAllocation(req.ParentId , id , req.siteId , req.status , userDetails.ID);
            }

            return res;
        }

        [HttpPost("ScanEvents")]
        public async Task<List<GeoLocationEvent>> Post(GetScanRequest req)
        {
            var userId = (string)HttpContext.Items["User"];
            //UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            //userDetailsRequest.id = userId;
            //var userDetails = await _kistService.UsersDetails(userDetailsRequest);
            return await _kistService.GetDTMobile_ScanEvents(req);
        }

        [HttpGet("Remove/{id}")]
        public async  Task<long> Remove(long id)
        {
            var userId = (string)HttpContext.Items["User"];
            UserDetailsRequest userDetailsRequest2 = new UserDetailsRequest();
            userDetailsRequest2.id = userId;
            var userDetails = await _kistService.UsersDetails(userDetailsRequest2);
            return await _kistService.RemoveAllocation(id, userDetails.ID);
        }

        [HttpGet("Identity/{id}")]
        public Task<AssetIdentity> Identity(long id)
        {
            var userId = (string)HttpContext.Items["User"];
            return _kistService.GetAssetIdentity(id);
        }

        [HttpPost]
        [Route("Identity/Put")]
        public async Task<AssetIdentity> PutIdentity(AssetIdentity note)
        //public async Task<IActionResult> Index([FromForm]IFormFile file)
        {
            var userId = (string)HttpContext.Items["User"];
            UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            userDetailsRequest.id = userId;
            var userDetails = await _kistService.UsersDetails(userDetailsRequest);

            note.modifiedBy = userDetails.Forename + " " + userDetails.Surname;
            note.modifiedOn = DateTime.Now;
            return await _kistService.PutIdentity(note);
        }

        [HttpGet("System/{id}")]
        public Task<AssetSystem> System(long id)
        {
            var userId = (string)HttpContext.Items["User"];
            return _kistService.GetAssetSystem(id);
        }

        [HttpPost]
        [Route("System/Put")]
        public async Task<AssetSystem> PutSystem(AssetSystem note)
        //public async Task<IActionResult> Index([FromForm]IFormFile file)
        {
            var userId = (string)HttpContext.Items["User"];
            UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            userDetailsRequest.id = userId;
            var userDetails = await _kistService.UsersDetails(userDetailsRequest);

            note.modifiedBy = userDetails.Forename + " " + userDetails.Surname;
            note.modifiedOn = DateTime.Now;
            return await _kistService.PutAssetSystem(note);
        }

        [HttpGet("StatusHistory/{id}")]
        public async Task<List<AssetStatusHistory>> StatusHistory(long id)
        {
            var userId = (string)HttpContext.Items["User"];
            var aList = await _kistService.GetAssetStatusHistory(id);
            return aList;
        }
    }
}
