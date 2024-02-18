using ElectricStationMap.Models.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElectricStationMap.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger) 
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet(string returnUrl = null) 
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation($"User {User.Identity.Name} logged out");

            if (returnUrl != null)
                return LocalRedirect(returnUrl);
            else
                return RedirectToPage("/Index");
        }
    }
}
