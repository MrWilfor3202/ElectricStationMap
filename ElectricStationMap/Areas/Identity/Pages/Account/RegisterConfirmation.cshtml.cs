using ElectricStationMap.Services.Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace ElectricStationMap.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;

        public RegisterConfirmationModel(UserManager<IdentityUser> userManager,
            IEmailSender emailSender) 
        {
            _emailSender = emailSender;
            _userManager = userManager;
        }

        public string Email { get; set; }

        public bool DisplayConfirmAccountLink { get; set; }

        public string EmailConfirmationUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string email, string returnUrl = null)
        {
            if(email == null)
                return RedirectToPage("/Index");

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return NotFound($"Unable to found user with email: {email}");

            Email = email;
			DisplayConfirmAccountLink = true;

            if (DisplayConfirmAccountLink) 
            {
                var userId = await _userManager.GetUserIdAsync(user);
				var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
				code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", email, code },
                        protocol: Request.Scheme);
            }

			return Page();
        }
    }
}
