﻿using System;
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
    public class ScanController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;
        readonly IConfiguration _configuration;
        readonly IKistService _kistService;

        public ScanController(ILogger<AccountController> logger, IConfiguration configuration , IKistService kistService)
        {
            _logger = logger;
            _configuration = configuration;
            _kistService = kistService;
        }


        //[Route("UsersDetails")]
        //[HttpPost]
       

        [Route("Scan/")]
        // [HttpGet("{search}")]
        public async Task<GeoLocationEvent> Scan(GeoLocationEvent req)
        {
            // should be via company id or user id 
            var userId = (string)HttpContext.Items["User"];

            UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            userDetailsRequest.id = userId;

            var userDetails = await _kistService.UsersDetails(userDetailsRequest);



            // if payload contains geo event data log that first
          
                req.CreatedBy = "matttest";
            

                

          
          //  req.userId = userDetails.ID; // fudge for now to pass in user id 


            return await _kistService.PostGeoLocationEvent(req);

            }


      


        [Authorize]

        [HttpGet("{id}")]
        public  Task<Asset> Get(long id)
        {
            var userId = (string)HttpContext.Items["User"];

            //UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            //userDetailsRequest.id = userId;

            //var userDetails = await _kistService.UsersDetails(userDetailsRequest);


            return _kistService.GetAsset(id);
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


        [Authorize]
        [HttpPost("MyScans")]
        public async Task<List<MyScan>> MyScans()
        {
            var userId = (string)HttpContext.Items["User"];

            //UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            //userDetailsRequest.id = userId;

            //var userDetails = await _kistService.UsersDetails(userDetailsRequest);


            return await _kistService.GetMyScans(userId);
        }


        [HttpPost("MapPopupInfo")]
        public async Task<GetMapPopupResponse> MapPopupInfo(GetScanRequest req)
        {
          //  var userId = (string)HttpContext.Items["User"];

            //UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            //userDetailsRequest.id = userId;

            //var userDetails = await _kistService.UsersDetails(userDetailsRequest);


            return await _kistService.GetMapPopupInfo(req.AssetID);
        }

        [Authorize]

        [HttpPost("Create")]
        public async Task<CreateAssetResult> Create(CreateQuickAssetRequest req)
        {
            var userId = (string)HttpContext.Items["User"];

            //UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            //userDetailsRequest.id = userId;

            //var userDetails = await _kistService.UsersDetails(userDetailsRequest);


            return await _kistService.CreateAsset(req);
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
