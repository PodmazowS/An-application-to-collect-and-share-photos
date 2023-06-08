using ApplicationLayer.AppServices;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading.Tasks;

namespace An_application_to_collect_and_share_photos.Pages.Users
{
    [Authorize(Roles = "Admin")] // Додати авторизацію для адміністратора
    public class AssignRole : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<UserRole> _roleManager;

        public AssignRole(UserManager<User> userManager, RoleManager<UserRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        [BindProperty]
        [Required(ErrorMessage = "Please select a user")]
        public string SelectedUserId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please select a role")]
        public string SelectedRoleId { get; set; }

        public IList<User> Users { get; set; }

        public IList<UserRole> Roles { get; set; }


        public PageResult OnGet(string userId)
        {
            Users = _userManager.Users.ToList();
            Roles = _roleManager.Roles.ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.FindByIdAsync(SelectedUserId);
            if (user == null)
            {
                return Page();
            }

            var role = await _roleManager.FindByIdAsync(SelectedRoleId);
            if (role == null)
            {
                return Page();
            }

            await _userManager.AddToRoleAsync(user, role.Name);

            TempData["Success"] = "Successfully assigned role!";


            return RedirectToPage("/Users/AssignRole");
        }
    }
}
