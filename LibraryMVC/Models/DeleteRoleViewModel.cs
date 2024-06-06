using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryMVC.Models
{
    public class DeleteRoleViewModel
    {
        [Required]
        public string RoleId { get; set; }

        [Required]
        [DisplayName("Role's Name")]
        public string RoleName { get; set; }
    }
}