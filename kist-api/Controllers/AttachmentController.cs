using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using kist_api.Model;
using kist_api.Model.dtcusid;
using kist_api.Models.Account;
using kist_api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace kist_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
                 
    public class AttachmentController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;
        readonly IConfiguration _configuration;
        readonly IKistService _kistService;

        public AttachmentController(ILogger<AccountController> logger, IConfiguration configuration , IKistService kistService)
        {
            _logger = logger;
            _configuration = configuration;
            _kistService = kistService;
     
        }

        private readonly string[] ACCEPTED_FILE_TYPES = new[] { ".jpg", ".jpeg", ".png" };

        //public filesController(IHostingEnvironment host)
        //{
        //    this.context = context;
        //    this.host = host;
        //}

        [HttpPost]
        public async Task<IActionResult> Index([FromForm]KistFile kFile)
        //     public async Task<IActionResult> Index([FromForm]IFormFile file)
        {
            var uploads = _configuration.GetValue<string>("Attachments:Path") ;

            _logger.LogInformation(@"uploading file to path " + uploads);

            var newFileName = kFile.system + "_" + kFile.Area + "_" + kFile.Key + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + kFile.File.FileName;

            _logger.LogInformation(@"New File Name will be " + newFileName);

            var userId = (string)HttpContext.Items["User"];
            UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            userDetailsRequest.id = userId;

            var userDetails = await _kistService.UsersDetails(userDetailsRequest);


            //foreach (var file in files)
            //{
            if (kFile.File.Length > 0)
            {
                using (var fileStream = new FileStream(Path.Combine(uploads, newFileName), FileMode.Create))
                {
                    await kFile.File.CopyToAsync(fileStream);

                    Attachment attachment = new Attachment();
                    attachment.system = kFile.system;
                    attachment.area = kFile.Area;
                    attachment.key = Convert.ToInt64(kFile.Key);
                    attachment.subKey = Convert.ToInt64(kFile.SubKey);

                    attachment.uploadedFileName = kFile.File.FileName;
                    attachment.storageLocation = newFileName;
                    attachment.attachmentType = kFile.attachmentType;
                    attachment.notes = kFile.Notes;
                    attachment.tags = kFile.Tags;


                    attachment.createdBy = userDetails.Forename + " " + userDetails.Surname;
                    attachment.createdOn = DateTime.Now;
                    _logger.LogInformation(@"Creating matching DB record");
                    await _kistService.PutAttachment(attachment);

                    return StatusCode(StatusCodes.Status201Created);
                }
            }
            //}
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost]
        [Route("AddNote")]
        public async Task<Attachment> AddNote(Attachment attachment)
        //     public async Task<IActionResult> Index([FromForm]IFormFile file)
        {
            var userId = (string)HttpContext.Items["User"];
            UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            userDetailsRequest.id = userId;

            var userDetails = await _kistService.UsersDetails(userDetailsRequest);


            attachment.createdBy = userDetails.Forename + " " + userDetails.Surname;
                    attachment.createdOn = DateTime.Now;
                    return await _kistService.PutAttachment(attachment);

    
        }

        [HttpPost]
        [Route("GetAttachments")]
        public async Task<List<Attachment>> GetAttachments(Attachment attachment)
        {
            var userId = (string)HttpContext.Items["User"];

            //List<Attachment> attachmentResults = new List<Attachment>();
            //attachmentResults.Add(new Attachment { appName = "KIST", area = "Asset", ID = 1, key = 13573, attachmentType = "file", uploadedFileName = "capture.png" });
            //attachmentResults.Add(new Attachment { appName = "KIST", area = "Asset", ID = 2, key = 13573, attachmentType = "file", uploadedFileName = "manual.pdf" });

            //UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            //userDetailsRequest.id = userId;

            //var userDetails = await _kistService.UsersDetails(userDetailsRequest);
            return await _kistService.GetAttachments(attachment);

           // return  attachmentResults;
        }
        //[HttpPost]


        //[Route("api/[controller]")]

        //GET api/download/12345abc
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            string contentType;

            var dmsRoutePath = _configuration.GetValue<string>("Attachments:Path");

            Attachment attachment = new Attachment();
            attachment.ID = id;

            var returnAttachment = await _kistService.GetAttachments(attachment);

            string mime = MimeTypes.GetMimeType(returnAttachment.First().storageLocation);

            //   new FileExtensionContentTypeProvider().TryGetContentType( out contentType);
            //return contentType ?? "application/octet-stream";
            var fileloc = dmsRoutePath + returnAttachment.First().storageLocation;
            //var fileloc = Path.Combine(dmsRoutePath, returnAttachment.First().storageLocation);

            var stream = new FileStream(fileloc, FileMode.Open);
            return new FileStreamResult(stream, mime )
            {
                FileDownloadName = "template" + id.ToString() + ".xlsx"
            }; ;
        }


        //public ActionResult Post([FromForm]IFormFile file)
        //{
        //    if (file != null)
        //    {
        //        string filePath = "c:\temp\test.png";


        //        using (var stream = System.IO.File.Create(filePath))
        //        {
        //            file.CopyTo(stream);
        //        }
        //    }
        //    return Redirect("/");
        //}


        //public IActionResult Post(IFormFile filesData)
        //{
        //    if (filesData == null) return BadRequest("Null File");
        //    if (filesData.Length == 0)
        //    {
        //        return BadRequest("Empty File");
        //    }
        //    if (filesData.Length > 10 * 1024 * 1024) return BadRequest("Max file size exceeded.");
        //    if (!ACCEPTED_FILE_TYPES.Any(s => s == Path.GetExtension(filesData.FileName).ToLower())) return BadRequest("Invalid file type.");
        //    var uploadFilesPath = Path.Combine("c:\temp", "uploads");
        //    if (!Directory.Exists(uploadFilesPath))
        //        Directory.CreateDirectory(uploadFilesPath);
        //    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(filesData.FileName);
        //    var filePath = Path.Combine(uploadFilesPath, fileName);
        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //         filesData.CopyToAsync(stream); //await
        //    }
        //    var photo = new KistFile { FileName = fileName };
        //   // context.files.Add(photo);
        //  //  await context.SaveChangesAsync();
        //    return Ok();
        //}

        //[HttpPost]
        //public IActionResult Post(List<IFormFile> file)
        //{
        //    try
        //    {
        //        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.FileName);
        //        using (Stream stream = new FileStream(path, FileMode.Create))
        //        {
        //            file.FormFile.CopyTo(stream);
        //        }

        //        return StatusCode(StatusCodes.Status201Created);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }

        //}
    }
}
