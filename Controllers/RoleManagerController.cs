using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Assign1.Controllers
{
    public class RoleManagerController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RoleManagerController> _logger;

        public RoleManagerController(RoleManager<IdentityRole> roleManager, ILogger<RoleManagerController> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var roles = await _roleManager.Roles.ToListAsync();
                return View(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load roles.");
                return RedirectToAction("Error500", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (!string.IsNullOrWhiteSpace(roleName))
            {
                try
                {
                    var result = await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
                    if (!result.Succeeded)
                    {
                        _logger.LogWarning("Failed to create role: {RoleName}. Errors: {Errors}", roleName, string.Join(", ", result.Errors));
                        // Optionally handle the error display to the user in a user-friendly manner
                        TempData["Error"] = "Role creation failed: " + string.Join(", ", result.Errors);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Exception occurred when trying to create a role: {RoleName}.", roleName);
                    TempData["Error"] = "Exception occurred when creating role.";
                }
            }
            else
            {
                TempData["Error"] = "Role name cannot be empty.";
            }
            return RedirectToAction("Index");
        }
    }
}
