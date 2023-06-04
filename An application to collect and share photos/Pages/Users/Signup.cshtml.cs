using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Infrastructure.Data.UserRoles__Static_;

namespace An_application_to_collect_and_share_photos.Pages
{
    public class SignupModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;

        public SignupModel(IUserService userService, IUserRoleService userRoleService)
        {
            _userService = userService;
            _userRoleService = userRoleService;
        }

        [BindProperty]
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please enter your username")]
        [MinLength(5,ErrorMessage = "Your username is too short")]
        public string Username { get; set; }

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
                // Якщо модель недійсна, поверніть сторінку з помилками валідації
                return Page();
            }

            var user = new User
            {
                Id = ObjectId.GenerateNewId(),
                UserName = Username,
                Email = Email,
                PasswordHash = Password
            };
            await _userService.CreateUserAsync(user);

            
            var userRole = new UserRole
            {
                Id = ObjectId.GenerateNewId(),
                UserId = user.Id,
                Name = UserRoles.User
            };

            await _userRoleService.CreateUserRoleAsync(userRole);

            return RedirectToPage("/Users/SignUpSuccess");

        }
    }
}
