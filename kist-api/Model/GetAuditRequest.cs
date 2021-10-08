using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model.dtmobile
{
    public class GetAuditRequest
    {
        //@userId as bigint , @operatorid as bigint , @assetid as bigint , @auditType nvarchar(30) ,@auditDate as datetime
        public long? auditId { get; set; }
        public long? userId { get; set; }
      
     



    }
}
