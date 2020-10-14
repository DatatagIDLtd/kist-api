using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kist_api.Models.Account
{
    public class UsersInApplicationRoles
    {
        public long ID { get; set; }
        public long UserDetailID { get; set; }
        public long ApplicationID { get; set; }
        public long RoleID { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}