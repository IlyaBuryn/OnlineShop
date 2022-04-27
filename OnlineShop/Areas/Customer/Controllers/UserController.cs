using BusinessLogic.DtoModels;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OnlineShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserRoleManager _userRoleManager;
        private readonly IApplicationUserManager _appUserManager;

        public UserController(UserManager<IdentityUser> userManager,
            IUserRoleManager userRoleManager,
            IApplicationUserManager appUserManager)
        {
            _userManager = userManager;
            _userRoleManager = userRoleManager;
            _appUserManager = appUserManager;
        }
        public IActionResult Index()
        {
            _userManager.GetUserId(HttpContext.User);
            return View(_appUserManager.GetAllAppUsers());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityUser user)
        {
            if (ModelState.IsValid)
            {
                user.EmailConfirmed = true;
                var result = await _userManager.CreateAsync(user, user.PasswordHash);

                if (result.Succeeded)
                {
                    var isSaveRole = await _userManager.AddToRoleAsync(user, role: "User");
                    TempData["Save"] = "User has been created successfully";
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                TempData["Delete"] = "Error! User has not been created";
            }
            return View();
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = _appUserManager.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IdentityUser user)
        {
            var userInfo = _appUserManager.GetById(user.Id);
            if (userInfo == null)
            {
                return NotFound();
            }

            var result = await _userManager.UpdateAsync(userInfo);
            if (result.Succeeded)
            {
                TempData["Save"] = "User has been updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(userInfo);
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = _appUserManager.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        public async Task<IActionResult> Locout(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = _appUserManager.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Locout(IdentityUser user)
        {
            var userInfo = _appUserManager.GetById(user.Id);
            if (userInfo == null)
            {
                return NotFound();
            }
            userInfo.LockoutEnd = System.DateTime.Now.AddYears(100);
            int rowAffected = _appUserManager.UpdateAppUser(userInfo);
            if (rowAffected > 0)
            {
                TempData["Save"] = "User has been lockout successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public async Task<IActionResult> Active(string id)
        {
            var user = _appUserManager.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Active(IdentityUser user)
        {
            var userInfo = _appUserManager.GetById(user.Id);
            if (userInfo == null)
            {
                return NotFound();
            }
            userInfo.LockoutEnd = System.DateTime.Now.AddDays(-1);
            int rowAffected = _appUserManager.UpdateAppUser(userInfo);
            if (rowAffected > 0)
            {
                TempData["Save"] = "User has been active successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = _appUserManager.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(IdentityUser user)
        {
            var userInfo = _appUserManager.GetById(user.Id);
            if (userInfo == null)
            {
                return NotFound();
            }
            int rowAffected = _appUserManager.RemoveAppUser(userInfo);
            if (rowAffected > 0)
            {
                TempData["Save"] = "User has been delete successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
    }
}
