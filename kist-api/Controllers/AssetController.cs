using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ceup_api.Model.dtdead;
using kist_api.Model;
using kist_api.Model.dtcusid;
using kist_api.Model.dtmobile;
using kist_api.Models.Account;
using kist_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using QRCoder;

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

        [Authorize]
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
        public async Task<List<AssetView>> Search2(GetAssetRequest req)
        {
            // should be via company id or user id 
            var userId = (string)HttpContext.Items["User"];

            UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            userDetailsRequest.id = userId;

            var userDetails = await _kistService.UsersDetails(userDetailsRequest);



            // if payload contains geo event data log that first
            if (req.geo != null )
            {
                //req.geo.CreatedBy = userDetails.;
                req.geo.CreatedOn = DateTime.Now;
                req.geo.WFStatus = "N";
                req.geo.UserGUID = userId;
                await _kistService.PostGeoLocationEvent(req.geo);

            }

            //UserDetailsRequest userDetailsRequest2 = new UserDetailsRequest();
            //userDetailsRequest2.id = userDetails.ID.ToString();
            //userDetailsRequest2.searchQuery = search;

            req.userId = userDetails.ID; // fudge for now to pass in user id 


            return await _kistService.GetAssetsByUser(req);

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

        [HttpPost("PostEventData")]
        public async Task<String> PostEventData(ClientConfig req)
        {
            var userName = (string)HttpContext.Items["UserName"];

            req.UserName = userName;
            req.CreatedBy = userName;
            //UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            //userDetailsRequest.id = userId;

            //var userDetails = await _kistService.UsersDetails(userDetailsRequest);


            return await _kistService.PostDTDead(req);

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

        [HttpGet("GetImages/{id}")]
        public Task<List<AssetImages>> GetImages(long id)
        {
            var userId = (string)HttpContext.Items["User"];



            return _kistService.GetAssetImages(id, 20060);
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
        [Authorize]
        [HttpGet("GetQrSheet/{id}")]
        public async Task<IActionResult> GetQrSheet(long id)
        {
            var iAsset = await _kistService.GetAsset(id); 
            var iSystem = await _kistService.GetAssetSystem(id);
            var uploads = _configuration.GetValue<string>("Attachments:Path");


            var label = iAsset.name;

            PdfDocument pdfDocument = PdfReader.Open(uploads + @"KISTData\genericQRCodeSheet.pdf", PdfDocumentOpenMode.Modify);

            //var stream = new FileStream(, FileMode.Open);
            var codePages = CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(codePages);


            // Create an empty page
            PdfPage page = pdfDocument.Pages[0];

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(iSystem.qrCodeUrl, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(_configuration.GetValue<int>("qrCode:pixelsPerModule"));

            

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create a font
            XFont font = new XFont(_configuration.GetValue<string>("qrCode:fontStyle"), _configuration.GetValue<int>("qrCode:fontSize"), XFontStyle.Bold);


            // add qr CODE

            var ximg = XImage.FromGdiPlusImage(qrCodeImage);

            gfx.DrawImage(ximg, _configuration.GetValue<int>("qrCode:x"), _configuration.GetValue<int>("qrCode:y"));

            // Draw the text
            gfx.DrawString(label, font, XBrushes.Black,
                           new XRect(0, _configuration.GetValue<int>("qrCode:textX"), page.Width, page.Height),
                           XStringFormats.TopCenter);

 

         

         //   pdfDocument.Save(@"C:\temp\uploads\KISTData\temp.pdf");


            //byte[] fileContents = null;
            //using (MemoryStream stream = new MemoryStream())
            //{
            //    pdfDocument.Save(stream, true);
            //    fileContents = stream.ToArray();
            //    return new FileStreamResult(stream, "application/pdf");
            //}


            MemoryStream stream = new MemoryStream();

            pdfDocument.Save(stream, false);
            return new FileStreamResult(stream, "application/pdf");
            //  var stream = new FileStream(@"C:\temp\uploads\KISTData\temp.pdf", FileMode.Open);



        }



    }
}
