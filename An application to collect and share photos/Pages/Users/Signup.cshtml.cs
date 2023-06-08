using ApplicationLayer.AppServices;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Xml.Linq;

namespace An_application_to_collect_and_share_photos.Pages
{
    public class SignupModel : PageModel
    {
        private UserService _userService;
        private UserManager<User> _userManager;
        private RoleManager<UserRole> _roleManager;

        public SignupModel(UserManager<User> userManager, RoleManager<UserRole> roleManager, UserService userService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userService = userService;
        }

        [BindProperty]
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please enter your username")]
        [MinLength(5, ErrorMessage = "Your username is too short")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Login must only contain letters and digits")]
        public string Username { get; set; }

        [BindProperty]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter an email address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [BindProperty]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter a password")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{6,}$", ErrorMessage = "Password must be at least 6 characters long and contain at least one small letter, one capital letter, one special sign and one digit.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty]
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Please confirm your password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (_userService.GetUserByEmailAsync(Email) != null)
            {
                return Page();
            }
            var user = new User
            {
                Id = ObjectId.GenerateNewId(),
                UserName = Username,
                Email = Email,
                PasswordHash = Password
            };

            IdentityResult result = await _userManager.CreateAsync(user, user.PasswordHash);
            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user,"User");
                return RedirectToPage("/Users/SignUpSuccess");
            }


            return Page();
        }
    }
}