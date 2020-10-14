using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class LoginResponse
    {
        public string response { get; set; }
        public MembershipUser userDetails { get; set; }

     
    }
}
