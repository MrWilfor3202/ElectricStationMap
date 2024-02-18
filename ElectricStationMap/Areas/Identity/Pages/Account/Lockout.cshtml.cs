using ElectricStationMap.Models.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElectricStationMap.Areas.Identity.Pages.Account
{
    public class LockoutModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _signInManager;

        public LockoutModel(UserManager<ApplicationUser> signInManager) 
        {
            _signInManager = signInManager;
        }

        public DateTimeOffset? LockoutEnd { get; set; }

        public async void OnGet(int id)
        {
            var user = await _signInManager.FindByIdAsync(id.ToString());
			LockoutEnd = user.LockoutEnd;
        }
    }
}
