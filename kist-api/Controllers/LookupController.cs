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
    public class LookupController : ControllerBase
    {

        private readonly ILogger<LookupController> _logger;
        readonly IConfiguration _configuration;
        readonly IKistService _kistService;

        public LookupController(ILogger<LookupController> logger, IConfiguration configuration , IKistService kistService)
        {
            _logger = logger;
            _configuration = configuration;
            _kistService = kistService;
        }


        //[Route("UsersDetails")]
        //[HttpPost]
        //   [Authorize]
        [HttpGet]
        //       public async Task<LookupData> Get()
        public LookupData Get()
        {
            var ll = new LookupData();
            ll.assetType.Add(new Lookup { ID = 1, value = "Tool" });
            ll.assetType.Add(new Lookup { ID = 2, value = "Vehicle" });


            // should be via company id or user id 
            //var userId = (string)HttpContext.Items["User"];

            //UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            //userDetailsRequest.id = userId;

            //var userDetails = await _kistService.UsersDetails(userDetailsRequest);





            //UserDetailsRequest userDetailsRequest2 = new UserDetailsRequest();
            //userDetailsRequest2.id = userDetails.ID.ToString();
            return ll;
            //return await _kistService.GetAssetsByUser(userDetailsRequest2);

        }

     
    }
}
