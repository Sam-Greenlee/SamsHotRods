using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SamsHotRods.Models;

namespace SamsHotRods.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RoleController(RoleManager<IdentityRole> roleMgr, UserManager<ApplicationUser> userMgr)
        {
            roleManager = roleMgr;
            userManager = userMgr;
        }

        public async Task<IActionResult> AddAdmin()
        {
            bool x;

            //Check and see if the Admin role exsists
            x = await roleManager.RoleExistsAsync("Admin");

            //If it does not exist then add the Admin role
            if (!x)
            {
                //Create the role 
                var role = new IdentityRole();
                role.Name = "Admin";
                await roleManager.CreateAsync(role);

                //Create a Admin User
                var user = new Models.ApplicationUser();
                user.UserName = "AdminUser";
                user.Email = "AdminUser@Sams.com";

                //Assign a password to the user
                string userPWD = "Admin@1234";

                //create user with password
                IdentityResult chkUser = await userManager.CreateAsync(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var result1 = await userManager.AddToRoleAsync(user, "Admin");

                    ViewData["Message"] = "Success role created";
                }

            }
            else
            {
                ViewData["Message"] = "Sorry that role already exists!";
            }


            return View("RoleStatus");
        }

        public async Task<IActionResult> AddPaidCust()
        {
            bool x;

            //Check and see if the Admin role exsists
            x = await roleManager.RoleExistsAsync("PaidCust");

            //If it does not exist then add the Admin role
            if (!x)
            {
                //Create the role 
                var role = new IdentityRole();
                role.Name = "PaidCust";
                await roleManager.CreateAsync(role);

                //Create a Admin User
                var user = new Models.ApplicationUser();
                user.UserName = "PaidCust";
                user.Email = "PaidCustUser@Sams.com";

                //Assign a password to the user
                string userPWD = "PaidCust@1234";

                //create user with password
                IdentityResult chkUser = await userManager.CreateAsync(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var result1 = await userManager.AddToRoleAsync(user, "PaidCust");

                    ViewData["Message"] = "Success role created";
                }

            }
            else
            {
                ViewData["Message"] = "Sorry that role already exists!";
            }


            return View("RoleStatus");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}