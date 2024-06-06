using LibraryMVC.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LibraryMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Home/AddUser
        [AllowAnonymous]
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    UserName = model.UserName
                };
                ApplicationUserManager UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                // IdentityResult result = UserManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    var SignInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("Index", "Home");
                }
                //Get the ID of the newly created user
                //if (result.Succeeded)
                //{
                //    string UserId = user.Id;
                //    return RedirectToAction("Index", "Home", new { Id = UserId });
                //}
                foreach (string error in result.Errors)
                    ModelState.AddModelError("", error);
            }
            return View(model);
        }

        // GET: /Home/UpdateUser
        
        public ActionResult UpdateUser(string UserId)
        {
            //Creating an Instance of EditUserViewModel
            EditUserViewModel model = new EditUserViewModel();

            //Create an instance of ApplicationUserManager class as we want to fetch the user details
            ApplicationUserManager UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            //Fetch the User Details by UserId using the FindById method
            ApplicationUser UserToEdit = UserManager.FindById(UserId);

            //If the user exists then map the data to EditUserViewModel properties
            if (UserToEdit != null)
            {
                model.UserId = UserToEdit.Id;
                model.UserName = UserToEdit.UserName;
                model.FirstName = UserToEdit.FirstName;
                model.LastName = UserToEdit.LastName;
                model.Email = UserToEdit.Email;
                model.PhoneNumber = UserToEdit.PhoneNumber;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Create an instance of ApplicationUserManager class as we want to fetch the user details
                ApplicationUserManager UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

                //Fetch the User Details by UserId using the FindById method
                ApplicationUser UserToEdit = UserManager.FindById(model.UserId);

                if (UserToEdit.UserName != model.UserName)
                    UserToEdit.UserName = model.UserName;

                if (UserToEdit.FirstName != model.FirstName)
                    UserToEdit.FirstName = model.FirstName;

                if (UserToEdit.LastName != model.LastName)
                    UserToEdit.LastName = model.LastName;

                if (UserToEdit.Email != model.Email)
                    UserToEdit.Email = model.Email;

                if (UserToEdit.PhoneNumber != model.PhoneNumber)
                    UserToEdit.PhoneNumber = model.PhoneNumber;

                //Call the Update method to Update the User data
                IdentityResult result = UserManager.Update(UserToEdit);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (string error in result.Errors)
                    ModelState.AddModelError("", error);
            }

            return View(model);
        }

        // GET: /Home/AddUser
        [AllowAnonymous]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangeUserPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Create an Instance of ApplicationUserManager
                ApplicationUserManager UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

                //Then Fetch the Logged In User Detail Based on User ID
                //User.Identity.GetUserId() Will give you the current logged in user id
                ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
                //ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                if (user != null)
                {
                    //If The User Exists Then Change the Password
                    IdentityResult result = UserManager.ChangePassword(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

                    if (result.Succeeded)
                    {
                        //After Successful Password Change, you can also sign-in the user and redirect to the Home Page
                        user = UserManager.FindById(User.Identity.GetUserId());
                        //user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                        if (user != null)
                        {
                            ApplicationSignInManager SignInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
                            SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                            //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        }

                        //Redirect to the Home Page
                        return RedirectToAction("Index", "Home");
                    }

                    //If Failure, add the Errors into the Model
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }

                    //Return to the Change Password View and Show the Error Details
                    return View(model);
                }

                //If the User Not Found, then display HttpNotFound Error
                return HttpNotFound();
            }

            //If any Validation error, then stay in the same Change Password View and Show the Error Details
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Library Management Website - Happy Happy Happy";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}