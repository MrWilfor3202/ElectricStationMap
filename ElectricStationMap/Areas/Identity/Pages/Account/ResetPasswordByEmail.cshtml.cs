using ElectricStationMap.Models.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;

namespace ElectricStationMap.Areas.Identity.Pages.Account
{
    public class ResetPasswordByEmailModel : PageModel
    {
        private readonly ILogger<ResetPasswordByEmailModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public ResetPasswordByEmailModel(ILogger<ResetPasswordByEmailModel> logger, 
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [Required]
		public string Email { get; set; }

        [Required]
		public string Code { get; set; }

		public class InputModel 
        {
            [Required]
            [Display(Name = "Пароль")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            [Display(Name = "Повторение пароля")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Пароли не совпадают")]
            public string RepeatingPassword { get; set; }
        }

        public IActionResult OnGet(string email, string code) 
        {
            if (code == null)
            {
                _logger.LogError($"Code isn't valid. email is {email}");
                return BadRequest($"Code isn't valid. email is {email}");
            }

            if (email == null)
            {
                _logger.LogError($"Email isn't valid. Current email is {email}");
                return BadRequest($"Email isn't valid. Current email is {email}");
            }

            Email = email;
            Code = code;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string email, string code) 
        {
            Email ??= email;
            Code ??= code;

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Email);

                if (user == null)
                {
                    _logger.LogError($"User with email:{Email} not found!");
                    return BadRequest($"User with email:{Email} not found!");
                }


                if (_userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, Input.Password) 
                    == PasswordVerificationResult.Success) 
                {
                    _logger.LogInformation($"user:{user.UserName}. New password equals old password. Password not restored");
                    ModelState.AddModelError(string.Empty, "Новый пароль не должен совпадать со старым");
                    return Page();
                }

                var token = System.Text.Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(Code));
                var result = await _userManager.ResetPasswordAsync(user, token, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation($"User password restored by email:{Email}");
                    return RedirectToPage("/Account/PasswordReset");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        _logger.LogInformation($"user:{user.UserName}. Identity fail: {error.Description}");
                        ModelState.AddModelError(String.Empty, error.Description);
                    }
				}
            }

            return Page();
        }
    }
}
