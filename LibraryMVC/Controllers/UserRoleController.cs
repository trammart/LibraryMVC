using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LibraryMVC;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using LibraryMVC.Models;
using System.Linq;
using System.Collections.Generic;

[Authorize(Roles = "Administrator")]
public class UserRoleController : Controller
{
    private ApplicationUserManager _userManager;
    private ApplicationRoleManager _roleManager;

    public UserRoleController()
    {
    }

    public UserRoleController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public ApplicationUserManager UserManager
    {
        get
        {
            return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }
        private set
        {
            _userManager = value;
        }
    }

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

    // GET: UserRole/Index
    public ActionResult Index()
    {
        var users = UserManager.Users.ToList();
        var userRolesViewModel = new List<UserRolesViewModel>();

        foreach (var user in users)
        {
            var userRoles = UserManager.GetRoles(user.Id);
            userRolesViewModel.Add(new UserRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = userRoles
            });
        }

        return View(userRolesViewModel);
    }

    // GET: UserRole/AssignRole
    public async Task<ActionResult> AssignRole(string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        var user = await UserManager.FindByIdAsync(userId);
        if (user == null)
        {
            return HttpNotFound();
        }

        var model = new AssignRoleViewModel
        {
            UserId = user.Id,
            UserName = user.UserName,
            RolesList = RoleManager.Roles.Select(x => new SelectListItem
            {
                Value = x.Name,
                Text = x.Name
            }).ToList()
        };

        return View(model);
    }

    // POST: UserRole/AssignRole
    [HttpPost]
    public async Task<ActionResult> AssignRole(AssignRoleViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await UserManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return HttpNotFound();
            }

            var result = await UserManager.AddToRoleAsync(user.Id, model.SelectedRole);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Errors.FirstOrDefault());
        }

        // In case of failure, redisplay the form with error messages
        model.RolesList = RoleManager.Roles.Select(x => new SelectListItem
        {
            Value = x.Name,
            Text = x.Name
        }).ToList();

        return View(model);
    }
}
