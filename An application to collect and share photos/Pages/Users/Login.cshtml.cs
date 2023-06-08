using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace An_application_to_collect_and_share_photos.Pages
{
    public class LoginModel : PageModel
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public LoginModel(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;        }


        [BindProperty]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter an email address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [BindProperty]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter a password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                User appUser = await _userManager.FindByEmailAsync(Email);
                if (appUser != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToPage("/Users/LoginSuccess");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please, try again!";
            }
            return Page();
        }

    }
}