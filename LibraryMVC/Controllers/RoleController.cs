using LibraryMVC.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net;

namespace LibraryMVC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RoleController : Controller
    {
        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        // GET: Role
        public ActionResult Index()
        {
            var roles = RoleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public ActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddRole(RegisterRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole { Name = model.RoleName };
                IdentityResult result = RoleManager.Create(role);
                if (result.Succeeded)
                {
                    string RoleId = role.Id;
                    return RedirectToAction("Index", "Home", new { Id = RoleId });
                }
                foreach (string error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return View(model);
        }

        
        public ActionResult UpdateRole(string roleId)
        {
            if (roleId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = RoleManager.FindById(roleId);
            if (role == null)
            {
                return HttpNotFound();
            }

            var model = new EditRoleViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateRole(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole roleToEdit = RoleManager.FindById(model.RoleId);
                if (roleToEdit == null)
                {
                    return HttpNotFound();
                }
                if (roleToEdit.Name != model.RoleName)
                {
                    roleToEdit.Name = model.RoleName;
                }

                IdentityResult result = RoleManager.Update(roleToEdit);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Role", new { Id = model.RoleId });
                }
                foreach (string error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return View(model);
        }
        public ActionResult DeleteRole(string roleId)
        {
            var role =  RoleManager.FindById(roleId);
            if (role == null)
            {
                return HttpNotFound();
            }

            var model = new DeleteRoleViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteRole(DeleteRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole roleToDelete = RoleManager.FindById(model.RoleId);
                if (roleToDelete == null)
                {
                    return HttpNotFound();
                }
                IdentityResult result = RoleManager.Delete(roleToDelete);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Role");
                }
                foreach (string error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return View(model);
        }
    }
}