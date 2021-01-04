using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model.dtcode
{
    public class GetQRCodeRequest
    {
        public string IDNumber { get; set; }
        public string SystemType { get; set; }
    }
}
