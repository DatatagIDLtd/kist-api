using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kist_api.Models.Account
{
    public class RolesModel
    {
        public long ID { get; set; }
        public string RoleName { get; set; }
        public string RoleCode { get; set; }
        public bool IsActive { get; set; }
    }
}