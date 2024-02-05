using ElectricStationMap.Models.EF;
using ElectricStationMap.Services.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ElectricStationMap.Areas.Identity.Pages.Account
{
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ResetPasswordModel> _logger;
        private readonly IEmailSender _emailSender;

        public ResetPasswordModel(UserManager<ApplicationUser> userManager, 
            IEmailSender emailSender,
            ILogger<ResetPasswordModel> logger) 
        {
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [Required]
        public string ReturnUrl { get; set; }

        public class InputModel 
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Электронная почта")]
            public string Email { get; set; }
        }

        public void OnGet(string returnUrl) 
        {
			returnUrl ??= Url.Content("~/");
			ReturnUrl = returnUrl;
		}

        public async Task<IActionResult> OnPostAsync(string returnUrl = null) 
        {
			returnUrl ??= Url.Content("~/");

			if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);

                if (user == null)
                {
                    ModelState.AddModelError(String.Empty, "Пользователь не найден");
                    return Page();
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                var callbackUrl = Url.Page("/Account/ResetPasswordByEmail",
                    pageHandler: null,
                    values: new { area = "Identity", Input.Email, code},
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(Input.Email, 
                    "Сброс пароля",
                    $"Для сброса пароля перейдите по ссылке:{callbackUrl}");

                _logger.LogInformation($"The letter was sent by email:{Input.Email}. User ID is {user.Id}");

                return RedirectToPage("/Account/LetterWasSentToEmail");
            }

            return Page();
        } 
    }
}
