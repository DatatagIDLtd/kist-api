using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kist_api.Model.dtcusid
{
    public class UserDetails
    {
        public long ID { get; set; }
        public Guid MemProviderUserID { get; set; }
        public string Title { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public DateTime? DOB { get; set; }
        public string BuildingNo { get; set; }
        public string BuildingName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string TownCity { get; set; }
        public string District { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
        public string CountryCode { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}