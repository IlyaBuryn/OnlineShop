using BusinessLogic.DtoModels;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserRoleManager _userRoleManager;
        public RoleController(RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            IUserRoleManager userRoleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _userRoleManager = userRoleManager;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            ViewBag.Roles = roles;
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            IdentityRole role = new IdentityRole();
            role.Name = name;
            var isExist = await _roleManager.RoleExistsAsync(role.Name);

            if (isExist)
            {
                ViewBag.mgs = "This role is already exist!";
                ViewBag.name = name;
                return View();
            }
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                TempData["Save"] = "Role has been saved successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(result);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            ViewBag.id = role.Id;
            ViewBag.name = role.Name;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, string name)
        {

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            role.Name = name;
            var isExist = await _roleManager.RoleExistsAsync(role.Name);
            if (isExist)
            {
                ViewBag.mgs = "This role is aldeady exist";
                ViewBag.name = name;
                return View();
            }
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                TempData["save"] = "Role has been updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            ViewBag.id = role.Id;
            ViewBag.name = role.Name;
            return View();
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                TempData["delete"] = "Role has been deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Assign()
        {
            ViewData["UserId"] = new SelectList(_userRoleManager.GetUsersIdWithLockout(), "Id", "UserName");
            ViewData["RoleId"] = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Assign(Dto_Vm_RoleUserVm roleUser)
        {
            var user = _userRoleManager.GetUserById(roleUser.UserId);
            var isCheckRoleAssign = await _userManager.IsInRoleAsync(user, roleUser.RoleId);
            if (isCheckRoleAssign)
            {
                ViewBag.mgs = "This user already assign this role!";
                ViewData["UserId"] = new SelectList(_userRoleManager.GetUsersIdWithLockout(), "Id", "UserName");
                ViewData["RoleId"] = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
                return View();
            }
            var role = await _userManager.AddToRoleAsync(user, roleUser.RoleId);
            if (role.Succeeded)
            {
                TempData["save"] = "User Role assigned";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public ActionResult AssignUserRole()
        {
            var result = _userRoleManager.GetAssignUsers();
            ViewBag.UserRoles = result;
            return View();
        }
    }
}