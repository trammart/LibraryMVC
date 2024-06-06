using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryMVC.Models
{
    public class RegisterRoleViewModel
    {
        [Display(Name = "Role name")]
        [Required]
        public string RoleName { get; set; }
    }
}