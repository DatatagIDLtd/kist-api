using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class AssetImages
	{

		//a.[ID]
      
  //    ,a.[FileName]
  //    ,lower(reverse(left(reverse(a.[UploadedFileName]), charindex('.', reverse(a.[UploadedFileName]))-1))) [FileExtension]
	 // ,a.[AttachmentType][FileType]
  //    ,a.[Notes]
  //    ,a.[StorageLocation]
  //    ,a.[UploadedFileName]



		public long id { get; set; }
		public string fileName { get; set; }
		public string fileExtension { get; set; }
		public string fileType { get; set; }
		public string notes { get; set; }
		public string storageLocation { get; set; }
		public string uploadeFileName { get; set; }



	}
}
