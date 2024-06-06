using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace LibraryMVC.Models
{
    public class AssignRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string SelectedRole { get; set; }
        public List<SelectListItem> RolesList { get; set; }
    }
}