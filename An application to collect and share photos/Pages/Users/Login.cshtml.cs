using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using AspNetCore.Identity.MongoDbCore;
using Microsoft.AspNetCore.Identity;
using Domain.Models;

namespace An_application_to_collect_and_share_photos.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly SignInManager<User> _signInManager;
        public LoginModel(IUserService userService, SignInManager<User> signInManager)
        {
            _userService = userService;
            _signInManager = signInManager;
        }

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
            if (!ModelState.IsValid)
            {
                // Якщо модель недійсна, поверніть сторінку з помилками валідації
                return Page();
            }
            var user = await _userService.GetUserByEmailAsync(Email);
            if (user != null)
            {
                var passwordCheck = await _userService.CheckPasswordAsync(user.Id, Password);
                if (passwordCheck)
                {
                    //   var result = _signInManager.PasswordSignInAsync(user, Password, false, false);
                    //   if (result.IsCompletedSuccessfully)
                    //   {
                        return RedirectToPage("/Index");
                 //   }
                }
            }
            TempData["Error"] = "Wrong credentials. Please, try again!";
            return Page();
        }
    } 
}
