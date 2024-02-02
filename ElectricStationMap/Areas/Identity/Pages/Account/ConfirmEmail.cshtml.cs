using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace ElectricStationMap.Areas.Identity.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<ConfirmEmailModel> _logger;

        public ConfirmEmailModel(UserManager<IdentityUser> userManager, ILogger<ConfirmEmailModel> logger) 
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(string code, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
				_logger.LogInformation($"User with email: {email} didn't found");
				return RedirectToPage("/Identity/Error/Error");
            }

			var token = System.Text.Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                _logger.LogInformation($"User with email: {email} confirmed by code: {code}");
                return Page();
            }
            else
            {
				_logger.LogError($"User with email: {email} didn't confirm by code: {code}");
				return RedirectToPage("/Identity/Error/Error");
            }
        }
    }
}
