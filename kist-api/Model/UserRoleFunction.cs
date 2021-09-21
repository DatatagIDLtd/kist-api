using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class UserRoleFunction
    {
      //   ,[RoleName]
      //,[AreaName]
      //,[FunctionName]
      //,[SubFunctionName]

        public long ID { get; set; }
        public long? parentid { get; set; }

        public string RoleName { get; set; }
        public string AreaName { get; set; }
        public string FunctionName { get; set; }
        public string SubFunctionName { get; set; }

    }

}
