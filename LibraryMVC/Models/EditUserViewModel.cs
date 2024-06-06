using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryMVC.Models
{
    public class EditUserViewModel
    {
        public string UserId { get; set; }
        [Display(Name = "Username")]
        [Required]
        public string UserName { get; set; }
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Phone number")]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}