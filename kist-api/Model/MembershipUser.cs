using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class MembershipUser
    {
        public string _ProviderName { get; set; }
        public string _UserName { get; set; }
        public string _Email { get; set; }
        public string _PasswordQuestion { get; set; }
        public string _ProviderUserKey { get; set; }

        public bool _IsApproved { get; set; }
        public bool _IsLockedOut { get; set; }

        public DateTime _LastLoginDate { get; set; }

        public string token { get; set; }
    }
}

 //"_UserName": "MattTest",
 //       "_ProviderUserKey": "7d642777-70bc-4556-8542-a2899051924b",
 //       "_Email": "matthew.green@datatag.co.uk",
 //       "_PasswordQuestion": null,
 //       "_Comment": null,
 //       "_IsApproved": true,
 //       "_IsLockedOut": false,
 //       "_LastLockoutDate": "1754-01-01T00:00:00Z",
 //       "_CreationDate": "2020-10-01T12:42:02Z",
 //       "_LastLoginDate": "2020-10-01T12:56:23.08Z",
 //       "_LastActivityDate": "2020-10-01T12:56:23.08Z",
 //       "_LastPasswordChangedDate": "2020-10-01T12:42:02Z",
 //       "_ProviderName": "AspNetSqlMembershipProvider"