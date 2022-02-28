using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;
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
        [Route("mobileUpload2")]
        public async Task<IActionResult> mobileUpload2(IList<IFormFile> files)
        {
            var uploads = _configuration.GetValue<string>("Attachments:Path");

            _logger.LogInformation(@"mobileUpload2 file to path " + uploads);

            foreach (IFormFile source in files)
            {
                _logger.LogInformation(@"file");

                string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.Trim('"');

                filename = this.EnsureCorrectFilename(filename);

                using (FileStream output = System.IO.File.Create(uploads + "\\test.png"))
                    await source.CopyToAsync(output);
            }

            return this.RedirectToAction("Index");
        }

        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }

    


        [HttpPost]
        [Route("mobileUpload3")]
        public async Task<IActionResult> mobileUpload([FromForm]IFormFile File)
        //     public async Task<IActionResult> Index([FromForm]IFormFile file)
        {
            var uploads = _configuration.GetValue<string>("Attachments:Path");

            _logger.LogInformation(@"mobileUpload file to path " + uploads);

      
     

            var system = "KIST";
            var area = "Asset";
            var Key = "40772";
            var SubKey = "";
            //var area = "KIST";
            //var area = "KIST";

            _logger.LogInformation(@"Area:" + area);

            _logger.LogInformation(@"kFile.FileName:" + File.FileName);
            

            var newFileName = system + "_" + area + "_" + Key + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + File.FileName;

            _logger.LogInformation(@"New File Name will be " + newFileName);

            var userId = (string)HttpContext.Items["User"];
            UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            userDetailsRequest.id = userId;

            var userDetails = await _kistService.UsersDetails(userDetailsRequest);


            //foreach (var file in files)
            //{
            if (File.Length > 0)
            {
                using (var fileStream = new FileStream(Path.Combine(uploads, newFileName), FileMode.Create))
                {
                    await File.CopyToAsync(fileStream);

                    Attachment attachment = new Attachment();
                    attachment.system = system;
                    attachment.area = area;
                    attachment.key = Convert.ToInt64(Key);
                    attachment.subKey = Convert.ToInt64(SubKey);

                    attachment.uploadedFileName = File.FileName;
                    attachment.storageLocation = newFileName;
                    attachment.attachmentType = 1;
                    attachment.notes = "";
                    attachment.tags = "stockimage";


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
       // [Route("mobileUpload")]
        public async Task<Attachment> Index([FromForm]KistFile kFile)
        //     public async Task<IActionResult> Index([FromForm]IFormFile file)
        {
            var res = new Attachment();
            var uploads = _configuration.GetValue<string>("Attachments:Path") ;

            _logger.LogInformation(@"uploading file to path " + uploads);

            _logger.LogInformation(@"Area:" + kFile.Area);


            var newFileName = kFile.system + "_" + kFile.Area + "_" + kFile.Key + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + kFile.File.FileName;

            _logger.LogInformation(@"New File Name will be " + newFileName);

            var userId = (string)HttpContext.Items["User"];
            var userName = (string)HttpContext.Items["UserName"];

            UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            userDetailsRequest.id = userId;

           // var userDetails = await _kistService.UsersDetails(userDetailsRequest);


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
                    attachment.notes = kFile.Note;
                    attachment.tags = kFile.Tags;


                    attachment.createdBy = userName;//userDetails.Forename + " " + userDetails.Surname;
                    attachment.createdOn = DateTime.Now;
                    _logger.LogInformation(@"Creating matching DB record");
                     res = await _kistService.PutAttachment(attachment);

                    return res;
                }
            }
            //}
            res.ID = -1;
            return res;
        }


        [HttpPost]
        [Route("AddNote")]
        public async Task<Attachment> AddNote([FromBody]AttachmentUpdateNoteModel request)
        //     public async Task<IActionResult> Index([FromForm]IFormFile file)
        {
            var userId = (string)HttpContext.Items["User"];
            UserDetailsRequest userDetailsRequest = new UserDetailsRequest();
            userDetailsRequest.id = userId;
            Attachment attachment = new Attachment { 
            
            };
            var userDetails = await _kistService.UsersDetails(userDetailsRequest);


            attachment.createdBy = userDetails.Forename + " " + userDetails.Surname;
            attachment.createdOn = DateTime.Now;
            attachment.notes = request.Note;
            attachment.ID = request.Id;
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
            var filename = returnAttachment.First().fileName;
            //var fileloc = Path.Combine(dmsRoutePath, returnAttachment.First().storageLocation);

            var stream = new FileStream(fileloc, FileMode.Open);
            return new FileStreamResult(stream, mime )
            {
                FileDownloadName = filename
            }; ;
        }
        [HttpGet]
        [Route("Thumb/{id}")]
        public async Task<IActionResult> Thumb(int id)
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
            var filename = returnAttachment.First().fileName;


            using (MagickImage image = new MagickImage(fileloc))
            {
                image.Format = image.Format; // Get or Set the format of the image.
                image.Resize(75, 75); // fit the image into the requested width and height. 
                image.Quality = 10; // This is the Compression level.
                image.Write(fileloc + ".thumb");
            }



            var stream = new FileStream(fileloc + ".thumb", FileMode.Open);
            return new FileStreamResult(stream, mime)
            {
                FileDownloadName = filename
            };
        }

        [HttpGet]
        [Route("Hand/{id}")]
        public async Task<IActionResult> Hand(int id)
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
            var filename = returnAttachment.First().fileName;


            using (MagickImage image = new MagickImage(fileloc))
            {
                image.Format = image.Format; // Get or Set the format of the image.
                image.Resize(500, 500); // fit the image into the requested width and height. 
                image.Quality = 10; // This is the Compression level.
                image.Write(fileloc + ".hand");
            }



            var stream = new FileStream(fileloc + ".hand", FileMode.Open);
            return new FileStreamResult(stream, mime)
            {
                FileDownloadName = filename
            };
        }


        public Image GetReducedImage(int width, int height, Stream resourceImage)
        {
            try
            {
                var image = Image.FromStream(resourceImage);
                var thumb = image.GetThumbnailImage(width, height, () => false, IntPtr.Zero);

                return thumb;
            }
            catch (Exception e)
            {
                return null;
            }
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
