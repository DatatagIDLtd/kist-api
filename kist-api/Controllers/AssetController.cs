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
      
        public async Task<List<Asset>> Get()
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
        public async Task<List<Asset>> Search(string search)
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
        public Task<Asset> Put(int id, [FromBody] Asset asset)
        {
            var userId = (string)HttpContext.Items["User"];
            asset.modifiedBy = userId;

            return _kistService.PutAsset(asset);
        }
    }
}
