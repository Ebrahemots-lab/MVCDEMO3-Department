using ApplicationDAL.Models;
using ApplicationPL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
            if (ModelState.IsValid)
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

            return View("Index", model);
        }


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

    }
}
