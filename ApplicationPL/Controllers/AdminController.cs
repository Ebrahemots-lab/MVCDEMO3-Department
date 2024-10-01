using ApplicationDAL.Models;
using ApplicationPL.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationPL.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> userManger;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;

        public AdminController(UserManager<ApplicationUser> userManger , RoleManager<IdentityRole> roleManager , IMapper mapper)
        {
            this.userManger = userManger;
            this.roleManager = roleManager;
            this.mapper = mapper;
        }

        public IActionResult Users() 
        {
            //Get all users in the database 
            var allUsers = userManger.Users.ToList();

            var mappedUser = new List<UsersViewModel>();

            //Convert it to userViewModel
            foreach(var user in allUsers) 
            {
                var userAfterMapping = mapper.Map<ApplicationUser, UsersViewModel>(user);
                userAfterMapping.FirstName = user.Fname;
                userAfterMapping.Role = userManger.GetRolesAsync(user).Result;
                mappedUser.Add(userAfterMapping);

            }

            return View(mappedUser);
        }


        public IActionResult CreateRole() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {
            if (ModelState.IsValid) 
            {
                //create role and insert in into database
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole() { Name=model.Name});
                if (result.Succeeded) 
                {
                    ViewBag.sucess = true;
                    return View();
                }
                else 
                {
                    foreach(var err in result.Errors) 
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
            }
            else 
            {
                ModelState.AddModelError("", "Enter valid role name");
            }
            return View(model);
        }


        public async Task<IActionResult> Details(string id) 
        {
            ApplicationUser user =  await userManger.FindByIdAsync(id);

            if(user is not null) 
            {
                var userAfterMapping = mapper.Map<ApplicationUser, UsersViewModel>(user);
                userAfterMapping.FirstName = user.Fname;
                userAfterMapping.Role = userManger.GetRolesAsync(user).Result;

                return View(userAfterMapping);
            }

            return BadRequest();
        }

        public async Task<IActionResult> Update(string id) 
        {
            ApplicationUser user = await userManger.FindByIdAsync(id);

            if (user is not null)
            {
                var userAfterMapping = mapper.Map<ApplicationUser, UsersViewModel>(user);
                userAfterMapping.FirstName = user.Fname;
                userAfterMapping.Role = userManger.GetRolesAsync(user).Result;
                return View(userAfterMapping);

                
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(UsersViewModel model)
        {
            //check for modelstate
            if (ModelState.IsValid) 
            {
                //get the use from database
               ApplicationUser userFromDatabase =  await userManger.FindByIdAsync(model.Id);

                if(userFromDatabase is not null) 
                {
                    userFromDatabase.UserName = model.UserName;
                    userFromDatabase.Fname = model.FirstName;
                    userFromDatabase.LastName = model.LastName;
                    userFromDatabase.Email = model.Email;

                    var role = await userManger.GetRolesAsync(userFromDatabase);
                    string roleName = role.FirstOrDefault();
                    //Validate Role 
                    //if it's null that means that the user not choose any role so set the default
                    var isRoleRemoved =  await userManger.RemoveFromRolesAsync(userFromDatabase,new List<string>() { roleName });

                    if (isRoleRemoved.Succeeded) 
                    {
                        //get the name of the role from model
                       
                       var roleToAdded =  await roleManager.FindByIdAsync(model.Role.First());
                        var isNewRoleAdded = await userManger.AddToRoleAsync(userFromDatabase, roleToAdded.Name);

                        if (isNewRoleAdded.Succeeded) 
                        {
                            return RedirectToAction("Users");
                        }
                    }

                }
            }

            return View();
        }
    }
}
