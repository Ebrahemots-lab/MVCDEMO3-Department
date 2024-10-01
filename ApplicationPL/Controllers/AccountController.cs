using ApplicationDAL.Models;
using ApplicationPL.Helpers;
using ApplicationPL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace ApplicationPL.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _user;

        public SignInManager<ApplicationUser> _SignInManager { get; }

        public AccountController(UserManager<ApplicationUser> user, SignInManager<ApplicationUser> signInManager)
        {
            _user = user;
            _SignInManager = signInManager;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid & model.IsAgree == true)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.Email.Split('@')[0],
                    Email = model.Email,
                    Fname = model.Fname,
                    LastName = model.Lname,
                    IsAgree = model.IsAgree,

                };

                //Save it and validate it
               await _user.AddToRoleAsync(user, "User");

                var result = await _user.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                   return RedirectToAction("Login");          
                }
                else
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, err.Description);
                    }
                }
            }
            ModelState.AddModelError("", "You Must Agree to the rules");
            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //check if the modelstate is valid 
            if (ModelState.IsValid)
            {
                //check if email is exist 
                var user = await _user.FindByEmailAsync(model.Email);

                if (user is not null)
                {
                    //check for password
                    var passwordFounded = await _user.CheckPasswordAsync(user, model.Password);

                    if (passwordFounded)
                    {
                        //Check Authentecation
                        var result = await _SignInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Password Is Wrong.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email Not Exist.");
                }

            }
            return View(model);
        }

      
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendForgetPasswordEmail(ForgetPasswordViewModel model) 
        {
            if (ModelState.IsValid) 
            {
                var user = await _user.FindByEmailAsync(model.Email);


                if(user is not null) 
                {


                    //Generate token to reset password 
                    var token = await _user.GeneratePasswordResetTokenAsync(user);

                    //https:localhost/account/resetpassword?email=ebrahemots&token=
                    var resetUrl = Url.Action("ResetPassword", "Account", new { email = user.Email, token }, Request.Scheme);


                    //Send him the email adress contains link to reset password
                    var mailToSend = new Email()
                    {
                        To = model.Email,
                        Subject = "Reset Password",
                        Body = resetUrl
                    };
                    EmailSetting.SendEmail(mailToSend);

                    return Content("Your Password Reset link has been sent to your mail");
                }
            }

            return View("ForgetPassword", model);
        }

        public IActionResult ResetPassword(string email, string token) 
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model) 
        {
            if (ModelState.IsValid) 
            {
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;
               ApplicationUser user =  await _user.FindByEmailAsync(email);

                if (user is not null) 
                {
                    if (user.Email == email)
                    {
                        IdentityResult result = await _user.ResetPasswordAsync(user, token, model.Password);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Login");
                        }
                    }
                }

            }
            return View();
        }


        public IActionResult AccessDenied() 
        {
            return Content("Your are not authorized");
        }


        public IActionResult Signout() 
        {
            _SignInManager.SignOutAsync();
            return View("Login");
        }


    }
}
