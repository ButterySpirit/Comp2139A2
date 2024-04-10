// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Assign1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assign1.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Display(Name = "Username")]
            public string Username { get; set; }

            [Display(Name = "Profile Picture")]
            public byte[] ProfilePicture { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Preferences")]
            public string Preferences { get; set; }

            [Display(Name = "Frequent Flyer Number")]
            public string FrequentFlyerNumber { get; set; }

            [Display(Name = "Hotel Loyalty ID")]
            public string HotelLoyaltyProgramId { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            var firstName = user.FirstName;
            var lastName = user.LastName;
            var profilePicture = user.ProfilePicture;
            var preferences = user.Preferences;
            var frequentFlyerNum = user.FrequentFlyerNumber;
            var hotelLoyaltyID = user.HotelLoyaltyProgramId;

            Username = userName;

            Input = new InputModel
            {
                Username = userName,
                FirstName = firstName,
                LastName = lastName,
                ProfilePicture = profilePicture,
                PhoneNumber = phoneNumber,
                Preferences = preferences,
                FrequentFlyerNumber = frequentFlyerNum,
                HotelLoyaltyProgramId = hotelLoyaltyID

            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            bool updateRequired = false;
            if (Input.FirstName != user.FirstName)
            {
                user.FirstName = Input.FirstName;
                updateRequired = true;
            }

            if (Input.LastName != user.LastName)
            {
                user.LastName = Input.LastName;
                updateRequired = true;
            }

            if (Input.Preferences != user.Preferences)
            {
                user.Preferences = Input.Preferences;
                updateRequired = true;
            }


            if (Input.FrequentFlyerNumber != user.FrequentFlyerNumber)
            {
                user.FrequentFlyerNumber = Input.FrequentFlyerNumber;
                updateRequired = true;
            }


            if (Input.HotelLoyaltyProgramId != user.HotelLoyaltyProgramId)
            {
                user.HotelLoyaltyProgramId = Input.HotelLoyaltyProgramId;
                updateRequired = true;
            }

            if (Request.Form.Files.Count > 0)
            {
                IFormFile formFile = Request.Form.Files.FirstOrDefault();
                if (formFile != null && formFile.Length > 0)
                {
                    using (var dataStream = new MemoryStream())
                    {
                        await formFile.CopyToAsync(dataStream);
                        user.ProfilePicture = dataStream.ToArray();
                    }
                    updateRequired = true;
                }
            }

            if (updateRequired)
            {
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    // If there was an error updating, reload the page with the errors
                    return Page();
                }
            }

            // If the user's information was updated successfully, or there were no changes,
            // refresh the sign-in to update the security stamp and ensure the claims are up-to-date
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}