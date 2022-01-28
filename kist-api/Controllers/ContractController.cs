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
    public class ContractController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;
        readonly IConfiguration _configuration;
        readonly IKistService _kistService;
        readonly IContractService _contractService;

        public ContractController(ILogger<AccountController> logger, IConfiguration configuration , IKistService kistService, IContractService contractService)
        {
            _logger = logger;
            _configuration = configuration;
            _kistService = kistService;
            _contractService = contractService;

        }


        //[Route("UsersDetails")]
        //[HttpPost]

        [Authorize]

        [HttpGet("{id}")]
        public async Task<Contract> Get(long id)
        {
            // should be via company id or user id 
            var req = new ContractRequest();


            req.UserName = (string)HttpContext.Items["UserName"];

            req.ID = id;


            //  req.userId = userDetails.ID; // fudge for now to pass in user id 

            var response = await _contractService.GetContract(req);

            return response.First();
        }

        [Authorize]
        // PUT: api/Default/5
        [HttpPost("Update")]
        public async Task<Contract> Update(Contract contract)
        {
            var userId = (string)HttpContext.Items["User"];
            var userName = (string)HttpContext.Items["UserName"];
           // UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
           // userDetailsRequest.id = userId;

           // var userDetails = await _kistService.UsersDetails(userDetailsRequest);


            contract.ModifiedBy = userName;
            contract.ModifiedOn = DateTime.Now;
            return await _contractService.UpdateContract(contract, userName);
        }

        [Route("Search/")]
        // [HttpGet("{search}")]
        public async Task<List<Contract>> Search(ContractRequest req)
        {
            // should be via company id or user id 
            req.UserName = (string)HttpContext.Items["UserName"];

           
          
          //  req.userId = userDetails.ID; // fudge for now to pass in user id 


            return await _contractService.GetContract(req);

            }


      


        
    }
}
