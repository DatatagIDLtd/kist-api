using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using kist_api.Model;
using kist_api.Model.dtcusid;
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
    public class AssetController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;
        readonly IConfiguration _configuration;
        readonly IKistService _kistService;

        public AssetController(ILogger<AccountController> logger, IConfiguration configuration , IKistService kistService)
        {
            _logger = logger;
            _configuration = configuration;
            _kistService = kistService;
        }


        //[Route("UsersDetails")]
        //[HttpPost]
        [Authorize]
      
        public async Task<List<AssetView>> Get()
        {
            // should be via company id or user id 
            var userId = (string)HttpContext.Items["User"];

            UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            userDetailsRequest.id = userId;
           
            var userDetails = await _kistService.UsersDetails(userDetailsRequest);





            UserDetailsRequest userDetailsRequest2 = new UserDetailsRequest();
            userDetailsRequest2.id = userDetails.ID.ToString();
      
            return await _kistService.GetAssetsByUser(userDetailsRequest2);

        }

       // [Authorize]
        [Route("Search/{search}")]
       // [HttpGet("{search}")]
        public async Task<List<AssetView>> Search(string search)
        {
            // should be via company id or user id 
            var userId = (string)HttpContext.Items["User"];

            UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            userDetailsRequest.id = userId;

            var userDetails = await _kistService.UsersDetails(userDetailsRequest);





            UserDetailsRequest userDetailsRequest2 = new UserDetailsRequest();
            userDetailsRequest2.id = userDetails.ID.ToString();
            userDetailsRequest2.searchQuery = search;
            return await _kistService.GetAssetsByUser(userDetailsRequest2);

        }

        [Route("Search2/")]
        // [HttpGet("{search}")]
        public async Task<List<AssetView>> Search2(GetAssetRequest asset)
        {
            // should be via company id or user id 
            var userId = (string)HttpContext.Items["User"];

            UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            userDetailsRequest.id = userId;

            var userDetails = await _kistService.UsersDetails(userDetailsRequest);





            //UserDetailsRequest userDetailsRequest2 = new UserDetailsRequest();
            //userDetailsRequest2.id = userDetails.ID.ToString();
            //userDetailsRequest2.searchQuery = search;

            asset.userId = userDetails.ID; // fudge for now to pass in user id 


            return await _kistService.GetAssetsByUser(asset);

        }

        [HttpGet("{id}")]
        public  Task<Asset> Get(long id)
        {
            var userId = (string)HttpContext.Items["User"];

            //UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            //userDetailsRequest.id = userId;

            //var userDetails = await _kistService.UsersDetails(userDetailsRequest);


            return _kistService.GetAsset(id);
        }

        // PUT: api/Default/5
        [HttpPut("{id}")]
        public async Task<Asset> Put(int id, [FromBody] Asset asset)
        {
            var userId = (string)HttpContext.Items["User"];
            UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            userDetailsRequest.id = userId;

            var userDetails = await _kistService.UsersDetails(userDetailsRequest);


            asset.modifiedBy = userDetails.Forename + " " + userDetails.Surname;
            asset.modifiedOn = DateTime.Now;
            return await _kistService.PutAsset(asset);
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
        //     public async Task<IActionResult> Index([FromForm]IFormFile file)
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
        //     public async Task<IActionResult> Index([FromForm]IFormFile file)
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
