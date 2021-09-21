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
                 
    public class NoteController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;
        readonly IConfiguration _configuration;
        readonly IKistService _kistService;

        public NoteController(ILogger<AccountController> logger, IConfiguration configuration , IKistService kistService)
        {
            _logger = logger;
            _configuration = configuration;
            _kistService = kistService;
     
        }

        private readonly string[] ACCEPTED_FILE_TYPES = new[] { ".jpg", ".jpeg", ".png" };

  
        [HttpPost]
        [Route("AddNote")]
        public async Task<Note> AddNote(Note note)
        //     public async Task<IActionResult> Index([FromForm]IFormFile file)
        {
            var userId = (string)HttpContext.Items["User"];

            var userName = (string)HttpContext.Items["UserName"];
            UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            userDetailsRequest.id = userId;

            var userDetails = await _kistService.UsersDetails(userDetailsRequest);

            note.createdBy = userName;
                    note.createdOn = DateTime.Now;
                    return await _kistService.PutNote(note);
    
        }

        [HttpPost]
        [Route("GetNotes")]
        public async Task<List<Note>> GetAttachments(Note note)
        {
            var userId = (string)HttpContext.Items["User"];

            //List<Attachment> attachmentResults = new List<Attachment>();
            //attachmentResults.Add(new Attachment { appName = "KIST", area = "Asset", ID = 1, key = 13573, attachmentType = "file", uploadedFileName = "capture.png" });
            //attachmentResults.Add(new Attachment { appName = "KIST", area = "Asset", ID = 2, key = 13573, attachmentType = "file", uploadedFileName = "manual.pdf" });

            //UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            //userDetailsRequest.id = userId;

            //var userDetails = await _kistService.UsersDetails(userDetailsRequest);
            return await _kistService.GetNotes(note);

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
            Attachment attachment = new Attachment();
            attachment.ID = id;

            var returnAttachment = await _kistService.GetAttachments(attachment);

            string mime = MimeTypes.GetMimeType(returnAttachment.First().storageLocation);

         //   new FileExtensionContentTypeProvider().TryGetContentType( out contentType);
            //return contentType ?? "application/octet-stream";

            var stream = new FileStream(returnAttachment.First().storageLocation, FileMode.Open);
            return new FileStreamResult(stream, mime);
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
