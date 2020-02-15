using ContentManagementSystem.Local.CustomIdentity;
using ContentManagementSystem.Local.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using static ContentManagementSystem.Local.Models.IdentityModel;

namespace ContentManagementSystem.Local.Controllers
{
    public class UserController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public UserController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public UserController()
        {
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
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

        [HttpGet]
        [OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Question");
            }

            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Question");
            }

            ApplicationUser user = await UserManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                if (user.Block)
                {
                    ModelState.AddModelError("", "Please contact with Admin.");
                }
                else if (!user.PasswordSet)
                {
                    IdentityResult passwordAdded = await UserManager.AddPasswordAsync(user.Id, model.Password);
                    if (passwordAdded.Succeeded)
                    {
                        var loggerUser = await UserManager.FindByIdAsync(user.Id);
                        loggerUser.PasswordSet = true;
                        IdentityResult response = await UserManager.UpdateAsync(loggerUser);
                        if (response.Succeeded)
                        {
                            await SignInManager.SignInAsync(user, isPersistent: model.RememberMe, rememberBrowser: model.RememberMe);
                            return RedirectToAction("Index", "Question");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Sorry, Something went wrong. Please try again.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Sorry, Unable to set your new password. Please try again");
                    }

                }
                else
                {
                    SignInStatus result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
                    switch (result)
                    {
                        case SignInStatus.Success:
                            return RedirectToAction("Index", "Question");
                        default:
                            ModelState.AddModelError("", "Invalid login attempt.");
                            break;
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Sorry, your email is not registered with us.");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Register()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, PasswordSet = false };

               ApplicationUser appUser = await UserManager.FindByEmailAsync(model.Email);
               if(appUser == null)
               {
                    var result = await UserManager.CreateAsync(user);
                    if (result.Succeeded)
                    {
                        await UserManager.AddToRoleAsync(user.Id, "Admin");

                        var callbackUrl = Url.Action("Login", "User", null, protocol: Request.Url.Scheme);

                        await UserManager.SendEmailAsync(user.Id, "Email Register", "You are registered with us to upload Blogs. Please click link to set your password <a href=\"" + callbackUrl + "\">here</a>");

                        return RedirectToAction("Index", "Question");
                    }
                   AddErrors(result);
               }
                else
                {
                    ModelState.AddModelError("", "Sorry, Email is already registered with us.");
                }
            }

            return View(model);
        }


        [HttpGet]
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", "Question");
            }
            AddErrors(result);
            return View(model);
        }


        //
        // GET: /User/ForgetPassword
        [AllowAnonymous]
        public ActionResult NewUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Question");
            }

            return View();
        }

        //
        // GET: /User/ForgetPassword
        [AllowAnonymous]
        public ActionResult ForgetPassword()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Question");
            }

            return View();
        }

        //
        // POST: /User/ForgetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> ForgetPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Question");
            }

            ApplicationUser user = await UserManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

                var callbackUrl = Url.Action(
               "ResetPassword", "User",
               new { userId = user.Id, code = code },
               protocol: Request.Url.Scheme);

                await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");

                return RedirectToAction("ForgotPasswordConfirmation", "User");
            }
            else
            {
                ModelState.AddModelError("", "Sorry, your email is not registered with us.");
            }

            return View(model);
        }

        //
        // GET: /User/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Question");

            }
            return View();
        }

        //
        // GET: /User/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Question");
            }


            return code == null ? View("Error") : View();
        }

        //
        // POST: /User/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Question");
            }


            var user = await UserManager.FindByNameAsync(model.Email);
            if (user != null)
            {
                var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("Index", "Question");
                }
                AddErrors(result);
            }
            
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }


        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "User");
        }

        #region helpers
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        #endregion
    }
}