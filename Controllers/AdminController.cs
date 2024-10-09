using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Productapp_with_SQL.Models;

namespace Productapp_with_SQL.Controllers
{
    public class AdminController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
       
        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
             _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult>UserList()
        {
            var users = _userManager.Users;
            var userViewModel = new List<UserViewModel>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userViewModel.Add(new UserViewModel
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Roles = roles
                });
            }
            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToRole(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();

            }

            var result = await _userManager.AddToRoleAsync(user, role);

            if (result.Succeeded)
            {
                return RedirectToAction("UserList");

            }

            return View("Error", result.Errors);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUserFromRole(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.RemoveFromRoleAsync(user, role);
            if (result.Succeeded)
            {
                return RedirectToAction("UserList");
            }

            ModelState.AddModelError(string.Empty, "Det gick inte att ta bort rollen");
            return View("UserList", _userManager.Users.ToList());   
        }


    }
}
