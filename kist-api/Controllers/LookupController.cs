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
        [Authorize]
        [Route("GetData")]
        [HttpGet]
        public async Task<LookupData> GetData()
        {
            //var json = "{\"assetStatus\":[{\"id\":1,\"value\":\"Active\"},{\"id\":2,\"value\":\"Broken\"},{\"id\":3,\"value\":\"Lost\"},{\"id\":4,\"value\":\"Repiar\"},{\"id\":5,\"value\":\"Service\"}],\"assetTypes\":[{\"id\":1,\"value\":\"Tool\"},{\"id\":2,\"value\":\"Vehicle\"}],\"oem\":[{\"id\":27,\"value\":\"Ashtead Plant Hire Company Ltd\"},{\"id\":28,\"value\":\"Honda\"},{\"id\":29,\"value\":\"BLUELIGHT GmbH\"},{\"id\":30,\"value\":\"SOURCE 1 ENVIROMENTAL\"},{\"id\":31,\"value\":\"MOTOROLA\"},{\"id\":32,\"value\":\"SOURCE ONE ENVIRONMENTAL\"},{\"id\":33,\"value\":\"FUSION PROVIDA\"},{\"id\":34,\"value\":\"HARRINGTON GENERATORS INT.LTD\"},{\"id\":35,\"value\":\"PLANT & SITE SERVICES LIMITED\"},{\"id\":36,\"value\":\"ADVANCE TECHL SYSTEMS LTD t\\/a ADVANCE WELDING\"},{\"id\":37,\"value\":\"RADIO DETECTION\"},{\"id\":38,\"value\":\"IMS Robotics\"},{\"id\":39,\"value\":\"Unknown\"}],\"model\":[{\"id\":50,\"parentid\":34,\"value\":\"2.4Kw Generator\"},{\"id\":51,\"parentid\":27,\"value\":\"250MM BUTT F\\/MACHINE\"},{\"id\":52,\"parentid\":34,\"value\":\"3.75KVA GENERATOR\"},{\"id\":53,\"parentid\":31,\"value\":\"6 WAY MULTI CHARGER\"},{\"id\":54,\"parentid\":36,\"value\":\"ATS180 Electrofusion Box\"},{\"id\":55,\"parentid\":29,\"value\":\"Bluelight LED Pipe Lining System\"},{\"id\":56,\"parentid\":27,\"value\":\"BUTT FUSION MACHINE\"},{\"id\":57,\"parentid\":35,\"value\":\"Butt Fusion Machine 180\"},{\"id\":58,\"parentid\":35,\"value\":\"Butt Fusion Machine 250\"},{\"id\":59,\"parentid\":37,\"value\":\"Cable Avoidance Took MK10\"},{\"id\":60,\"parentid\":37,\"value\":\"Cable Avoidance Took MK11\"},{\"id\":61,\"parentid\":37,\"value\":\"Cable Avoidance Took MK12\"},{\"id\":62,\"parentid\":37,\"value\":\"Cable Avoidance Took MK13\"},{\"id\":63,\"parentid\":37,\"value\":\"Cable Avoidance Took MK14\"},{\"id\":64,\"parentid\":37,\"value\":\"Cable Avoidance Took MK15\"},{\"id\":65,\"parentid\":37,\"value\":\"Cable Avoidance Took MK16\"},{\"id\":66,\"parentid\":37,\"value\":\"Cable Avoidance Took MK17\"},{\"id\":67,\"parentid\":37,\"value\":\"Cable Avoidance Took MK4\"},{\"id\":68,\"parentid\":37,\"value\":\"Cable Avoidance Took MK5\"},{\"id\":69,\"parentid\":37,\"value\":\"Cable Avoidance Took MK6\"},{\"id\":70,\"parentid\":37,\"value\":\"Cable Avoidance Took MK7\"},{\"id\":71,\"parentid\":37,\"value\":\"Cable Avoidance Took MK8\"},{\"id\":72,\"parentid\":37,\"value\":\"Cable Avoidance Took MK9\"},{\"id\":73,\"parentid\":37,\"value\":\"Cable Avoidance Tool gCAT4+\"},{\"id\":74,\"parentid\":37,\"value\":\"Cable Avoidance Tool Mk 4\"},{\"id\":75,\"parentid\":37,\"value\":\"Cable Avoidance Tool Mk 5\"},{\"id\":76,\"parentid\":37,\"value\":\"Cable Avoidance Tool Mk 6\"},{\"id\":77,\"parentid\":37,\"value\":\"Cable Avoidance Tool Mk4\"},{\"id\":78,\"parentid\":37,\"value\":\"CAT4\"},{\"id\":79,\"parentid\":31,\"value\":\"DP4000\"},{\"id\":80,\"parentid\":36,\"value\":\"Electrofusion box\"},{\"id\":81,\"parentid\":36,\"value\":\"Electrofusion Box Polyfuse+, Barcode\"},{\"id\":82,\"parentid\":27,\"value\":\"FUSION MACHINE\"},{\"id\":83,\"parentid\":33,\"value\":\"Gator 250mm Automatic Butt Fusion Machine\"},{\"id\":84,\"parentid\":34,\"value\":\"Generator 3.75Kva Petrol\"},{\"id\":85,\"parentid\":34,\"value\":\"Generator 3Kva Dual Voltage\"},{\"id\":86,\"parentid\":34,\"value\":\"Generator 3kva Petrol\"},{\"id\":87,\"parentid\":34,\"value\":\"Generator 40Kva Silenced 240v Single Phase\"},{\"id\":88,\"parentid\":28,\"value\":\"Honda GX270 3.5kW 4.4kva Generator\"},{\"id\":89,\"parentid\":32,\"value\":\"Kraso 500 Pipe Inversion Drum, c\\/w Storz Connector & Side Entrance Y Piece\"},{\"id\":90,\"parentid\":38,\"value\":\"MICROautomatic plus\"},{\"id\":91,\"parentid\":30,\"value\":\"Picote Maxi Miller 110v 30m\"},{\"id\":92,\"parentid\":37,\"value\":\"RD8100 RX\"},{\"id\":93,\"parentid\":37,\"value\":\"RD8100 TX\"},{\"id\":94,\"parentid\":37,\"value\":\"Signal Generator\"},{\"id\":95,\"parentid\":37,\"value\":\"Signal Generator Mk 4\"},{\"id\":96,\"parentid\":37,\"value\":\"Signal Generator Mk4\"},{\"id\":98,\"parentid\":39,\"value\":\"Unknown\"}]}";
            //var lookupResponse = JsonConvert.DeserializeObject<LookupData>(json);
            //return lookupResponse;

            var userId = (string)HttpContext.Items["User"];
            UserDetailsRequest userDetailsRequest2 = new UserDetailsRequest();
            userDetailsRequest2.id = userId;
            var userDetails = await _kistService.UsersDetails(userDetailsRequest2);

            UserDetailsRequest ud = new UserDetailsRequest();
            ud.id = userDetails.ID.ToString();
            ////UserDetailsRequest userDetailsRequest3 = new UserDetailsRequest();
            //    userDetailsRequest.id = userDetails.ID.ToString();
            return await _kistService.GetLookUpData(ud);
        }

        //[HttpGet]
        ////public async Task<LookupData> Get()
        //public LookupData Get2()
        //{
        //    var ll = new LookupData();
        //    ll.assetTypes.Add(new Lookup { ID = 1, value = "Tool" });
        //    ll.assetTypes.Add(new Lookup { ID = 2, value = "Vehicle" });
        //    // asset Status
        //    ll.assetStatus.Add(new Lookup { ID = 1, value = "Active" });
        //    ll.assetStatus.Add(new Lookup { ID = 2, value = "Broken" });
        //    ll.assetStatus.Add(new Lookup { ID = 3, value = "Lost" });
        //    ll.assetStatus.Add(new Lookup { ID = 4, value = "Repair" });
        //    ll.assetStatus.Add(new Lookup { ID = 5, value = "Service" });
        //    // should be via company id or user id 
        //    //var userId = (string)HttpContext.Items["User"];
        //    //UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
        //    //userDetailsRequest.id = userId;
        //    //var userDetails = await _kistService.UsersDetails(userDetailsRequest);
        //    //UserDetailsRequest userDetailsRequest2 = new UserDetailsRequest();
        //    //userDetailsRequest2.id = userDetails.ID.ToString();
        //    return ll;
        //    //return await _kistService.GetAssetsByUser(userDetailsRequest2);
        //}
    }
}
