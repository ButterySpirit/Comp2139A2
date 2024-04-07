using Assign1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Assign1.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<UserController> _logger;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, ILogger<UserController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        // GET: User/Index
        public IActionResult Index()
        {
            _logger.LogInformation("Visited Index Page");
            return View();
        }

        // GET: User/Register
        public IActionResult Register()
        {
            _logger.LogInformation("Visited Register Page");
            var model = new RegisterViewModel
            {
                RolesList = _roleManager.Roles.Select(x => new SelectListItem { Value = x.Name, Text = x.Name }).ToList()
            };
            return View(model);
        }

        // POST: User/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // Debugging: Log the raw data received
            _logger.LogDebug("Received registration form submission with username: {Username}, email: {Email}, role: {Role}", model.Username, model.Email, model.Role);

            if (ModelState.IsValid)
            {
                _logger.LogDebug("Registration form is valid. Attempting to create a new user.");

                var user = new ApplicationUser { UserName = model.Username, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User {Username} created successfully.", model.Username);

                    // Check and assign role to user
                    var roleResult = await _userManager.AddToRoleAsync(user, model.Role);
                    if (roleResult.Succeeded)
                    {
                        _logger.LogInformation("User {Username} assigned to role {Role} successfully.", model.Username, model.Role);
                    }
                    else
                    {
                        // Debugging: Log each error
                        roleResult.Errors.ToList().ForEach(e => _logger.LogWarning("Failed to assign role {Role} to user {Username}: {Error}", model.Role, model.Username, e.Description));
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User {Username} signed in after registration.", model.Username);
                    return RedirectToAction("Login", "User");
                }
                else
                {
                    // Debugging: Log each error
                    result.Errors.ToList().ForEach(e => _logger.LogWarning("User creation failed for username {Username}: {Error}", model.Username, e.Description));
                }
            }
            else
            {
                // Debugging: Log model state errors
                foreach (var modelState in ViewData.ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        _logger.LogDebug("Model validation error: {ErrorMessage}", error.ErrorMessage);
                    }
                }
            }

            // Reload roles list and show form again
            model.RolesList = _roleManager.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
            return View(model);
        }

        // GET: User/Login
        public IActionResult Login()
        {
            _logger.LogInformation("Visited Login Page");
            return View();
        }

        // POST: User/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            _logger.LogInformation("User attempting to login with username/email: {UsernameOrEmail}", model.UsernameOrEmail);
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UsernameOrEmail, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User {UsernameOrEmail} logged in successfully", model.UsernameOrEmail);
                    return LocalRedirect(returnUrl ?? "/");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    _logger.LogWarning("Invalid login attempt for user {UsernameOrEmail}", model.UsernameOrEmail);
                }
            }

            // If we get here, something failed, redisplay form
            return View(model);
        }

        // POST: User/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out");
            return RedirectToAction(nameof(Index));
        }
    }
}
