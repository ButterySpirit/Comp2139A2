using Assign1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assign1.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")] // Ensure only admins can access this controller
    public class UserRolesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<UserRolesController> _logger;

        public UserRolesController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<UserRolesController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var users = _userManager.Users.ToList();
                var userRolesViewModelList = new List<UserRolesViewModel>();

                foreach (var user in users)
                {
                    var thisViewModel = new UserRolesViewModel
                    {
                        UserId = user.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        Roles = await GetUserRoles(user)
                    };

                    userRolesViewModelList.Add(thisViewModel);
                }

                return View(userRolesViewModelList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading user roles.");
                return RedirectToAction("Error500", "Home");
            }
        }

        private async Task<IList<string>> GetUserRoles(ApplicationUser user)
        {
            try
            {
                return new List<string>(await _userManager.GetRolesAsync(user));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get roles for user {UserId}.", user.Id);
                throw;  // Rethrow the exception after logging to handle it further up the chain if necessary.
            }
        }
    }
}
