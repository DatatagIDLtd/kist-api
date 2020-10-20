using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class KistFile
    {
        public string AppName { get; set; }
        public string Area { get; set; }
        public string Key { get; set; }
        public string FileName { get; set; }
        public IFormFile File { get; set; }

        public string MimeType { get; set; }
        public string FileExtenstion { get; set; }

    }

}
