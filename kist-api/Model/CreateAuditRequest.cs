using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model.dtmobile
{
    public class CreateAuditRequest
    {
        //@userId as bigint , @operatorid as bigint , @assetid as bigint , @auditType nvarchar(30) ,@auditDate as datetime
        public long? ParentId { get; set; }
        public long? operatorId { get; set; }
        public long? assetId { get; set; }
        public long? siteId { get; set; }
        public long? userId { get; set; } // person being assigned audit
        public string auditType { get; set; }
        public DateTime auditDate { get; set; }
     



    }
}
