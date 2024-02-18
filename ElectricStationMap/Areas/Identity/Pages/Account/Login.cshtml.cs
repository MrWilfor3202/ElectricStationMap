using ElectricStationMap.Models.EF;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ElectricStationMap.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<ApplicationUser> signInManager,
                          UserManager<ApplicationUser> userManager,
                          ILogger<LoginModel> logger) 
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel 
        {
            [Required]
            [EmailAddress]
            [Display(Name = "����������� �����")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "������")]
            public string Password { get; set; }

            [Display(Name = "��������� ����")]
            public bool RememberMe { get; set; }   
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
			if (!string.IsNullOrEmpty(ErrorMessage))
                ModelState.AddModelError(string.Empty, ErrorMessage);

            returnUrl ??= Url.Content("~/");

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null) 
        {
            returnUrl ??= Url.Content("~/");
			ReturnUrl = returnUrl;

			if (ModelState.IsValid)
            { 
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, false);
				var user = await _userManager.FindByEmailAsync(Input.Email);
				
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in");

                    if(Url.IsLocalUrl(returnUrl))
                        return LocalRedirect(returnUrl);
                    else
                        return RedirectToPage("/Index");
                }

                if(result.RequiresTwoFactor)
                    return RedirectToPage("./TwoFactorAuthentication", new { rememberMe = false, returnUrl});

                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout", new { id = user.Id });
                }

                ModelState.AddModelError(String.Empty, "�� ������� �����");
            }
            
            return Page();
        }
    }
}
