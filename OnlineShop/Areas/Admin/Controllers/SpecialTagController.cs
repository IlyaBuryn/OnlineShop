using System.Threading.Tasks;
using BusinessLogic.DtoModels;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SpecialTagController : Controller
    {

        private readonly ISpecialTagManager _specialTagManager;

        public SpecialTagController(ISpecialTagManager specialTagManager)
        {
            _specialTagManager = specialTagManager;
        }

        //GET Index Action Method
        public IActionResult Index()
        {
            return View(_specialTagManager.GetAllSpecialTags());
        }

        //GET Create Action Method

        public ActionResult Create()
        {
            return View();
        }

        //POST Create Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Dto_SpecialTag specialTag)
        {
            if (ModelState.IsValid)
            {
                await _specialTagManager.AddSpecialTag(specialTag);
                TempData["save"] = "Special tag has been saved";
                return RedirectToAction(nameof(Index));
            }

            return View(specialTag);
        }

        //GET Edit Action Method

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialTag = _specialTagManager.FindSpecialTagsById(id);
            if (specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);
        }

        //POST Edit Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Dto_SpecialTag specialTag)
        {
            if (ModelState.IsValid)
            {
                await _specialTagManager.UpdateSpecialTag(specialTag);
                TempData["edit"] = "Special tag has been updated";
                return RedirectToAction(nameof(Index));
            }

            return View(specialTag);
        }

        //GET Details Action Method

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialTag = _specialTagManager.FindSpecialTagsById(id);
            if (specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);
        }

        //POST Edit Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(Dto_SpecialTag specialTag)
        {
            return RedirectToAction(nameof(Index));
        }

        //GET Delete Action Method

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialTag = _specialTagManager.FindSpecialTagsById(id);
            if (specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);
        }

        //POST Delete Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, Dto_SpecialTag specialTag)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (id != specialTag.Id)
            {
                return NotFound();
            }

            var specialTags = _specialTagManager.FindSpecialTagsById(id);
            if (specialTags == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _specialTagManager.DeleteSpecialTag(specialTags);
                TempData["delete"] = "Special tag been deleted";
                return RedirectToAction(nameof(Index));
            }

            return View(specialTag);
        }
    }
}
