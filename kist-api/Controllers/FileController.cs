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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace kist_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;
        readonly IConfiguration _configuration;
        readonly IKistService _kistService;

        public FileController(ILogger<AccountController> logger, IConfiguration configuration , IKistService kistService)
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

       // [HttpPost]
       // public async Task<IActionResult> Index([FromForm]KistFile kFile)
       ////     public async Task<IActionResult> Index([FromForm]IFormFile file)
       // {
       //     var uploads = Path.Combine("c:\\temp\\", "uploads");
       //     var newFileName = kFile.AppName + "_" + kFile.Area + "_" + kFile.Key + "_" +  DateTime.Now.ToString("yyyyMMddHHmmssfff" ) + "_" + kFile.File.FileName ;
       //     //foreach (var file in files)
       //     //{
       //         if (kFile.File.Length > 0)
       //         {
       //             using (var fileStream = new FileStream(Path.Combine(uploads, newFileName), FileMode.Create))
       //             {
       //                 await kFile.File.CopyToAsync(fileStream);
                    
       //             Attachment attachment = new Attachment();
       //             attachment.appName = kFile.AppName;
       //             attachment.area = kFile.Area;
       //             attachment.key = Convert.ToInt64(kFile.Key);
       //             attachment.uploadFileName = kFile.File.FileName;
       //             attachment.storageLocation = Path.Combine(uploads, newFileName);
       //             attachment.attachmentType = "file";

                   
       //             attachment.createdBy = "system";
       //             attachment.createdOn = DateTime.Now;
       //             await _kistService.PutAttachment(attachment);

       //             return StatusCode(StatusCodes.Status201Created);
       //             }
       //         }
       //     //}
       //     return StatusCode(StatusCodes.Status500InternalServerError);
       // }

        //[HttpPost]


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
