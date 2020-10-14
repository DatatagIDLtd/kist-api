using kist_api.Models.Account;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace kist_api.ViewModels.Account
{
    public class UserDetailsViewModel
    {
        public UserDetailsViewModel()
        {
            ID = 0;
        }

        public long? SupplierInviteID { get; set; }

        [Required]
        [Display(Name = "*Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "*Password")]
        public string Password { get; set; }

        [Display(Name = "*Confirm Password")]
        public string PasswordConfirm { get; set; }

        public RolesModel RolesModel { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Company Postcode")]
        public string CompanyPostcode { get; set; }

        public long ID { get; set; }
        public Guid MemProviderUserID { get; set; }

       // [Display(Name = "*Title")]
      //  public TitleListViewModel TitleViewModel { get; set; }

        [Display(Name = "*Forename")]
        public string Forename { get; set; }

        [Display(Name = "*Surname")]
        public string Surname { get; set; }

        [Display(Name = "Building No")]
        public string BuildingNo { get; set; }

        [Display(Name = "Building Name")]
        public string BuildingName { get; set; }

        [Display(Name = "*Address Line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [Display(Name = "Address Line 3")]
        public string AddressLine3 { get; set; }

        [Display(Name = "Address Line 4")]
        public string AddressLine4 { get; set; }

        [Display(Name = "*Town/ City")]
        public string TownCity { get; set; }

        [Display(Name = "District")]
        public string District { get; set; }

        [Display(Name = "*County")]
        public string County { get; set; }

        [Display(Name = "*Postcode")]
        public string PostCode { get; set; }

      //  [Display(Name = "*Country")]
        //public CountryListViewModel CountryViewModel { get; set; }
//
        [Display(Name = "Telephone")]
        public string Telephone { get; set; }

        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [Display(Name = "*Email")]
        public string Email { get; set; }

        public bool? IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string AddressList { get; set; }
    }
}