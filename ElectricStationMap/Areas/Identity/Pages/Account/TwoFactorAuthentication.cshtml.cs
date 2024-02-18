using ElectricStationMap.Models.EF;
using ElectricStationMap.Services.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Framework;

namespace ElectricStationMap.Areas.Identity.Pages.Account
{
    public class TwoFactorAuthenticationModel : PageModel
    {
        private readonly ILogger<TwoFactorAuthenticationModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _signInManager;

        [BindProperty]
        public InputModel Input { get; set; }

        [Required]
        public string ReturnUrl { get; set; }


        public TwoFactorAuthenticationModel(IEmailSender emailSender, ILogger<TwoFactorAuthenticationModel> logger, SignInManager<ApplicationUser> signInManager)
        {
            _emailSender = emailSender;
            _logger = logger;
            _signInManager = signInManager;
        }

        public class InputModel
        {
            public string TwoFactorCode { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(bool rememberMe = false, string returnUrl = null)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
                throw new InvalidOperationException($"Unable to load user with two factor authentication. User is {user.UserName}");

            returnUrl ??= "/";
            ReturnUrl = returnUrl;

            var code = await _signInManager.UserManager.GenerateTwoFactorTokenAsync(user, "Email");
            var email = user.Email;
            var messageText = "Код для входа в учетную запись: " + code;

            await _emailSender.SendEmailAsync(email, "Код", messageText);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(bool rememberMe = false, string returnUrl = "/")
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
                throw new InvalidOperationException($"Unable to load user with two factor authenticatio. User is {user.UserName}");

            var code = Input.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);
            var result = await _signInManager.TwoFactorSignInAsync("Email", code, rememberMe, false);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID '{UserId}' logged in with 2fa.", user.Id);
                return LocalRedirect(returnUrl);
            }

            if (result.IsLockedOut)
            {
                _logger.LogInformation("User with ID '{UserId}' account locked out.", user.Id);
                return RedirectToPage("./Lockout");
            }
            else
            {
                _logger.LogInformation("Invalid authenticator code entered for user with ID '{UserId}'.", user.Id);
                ModelState.AddModelError(string.Empty, "Неправильный код");
                return Page();
            }
        }
    }
}
