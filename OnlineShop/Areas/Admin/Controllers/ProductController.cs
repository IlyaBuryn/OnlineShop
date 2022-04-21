using BusinessLogic.DtoModels;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Threading.Tasks;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductManager _productManager;
        private readonly IProductTypesManager _productTypesManager;
        private readonly ISpecialTagManager _specialTagManager;
        private readonly IWebHostEnvironment _he;

        public ProductController(IWebHostEnvironment he,
            IProductTypesManager productTypesManager,
            IProductManager productManager,
            ISpecialTagManager specialTagManager)
        {
            _he = he;
            _productTypesManager = productTypesManager;
            _productManager = productManager;
            _specialTagManager = specialTagManager;
        }
        public IActionResult Index()
        {
            return View(_productManager.GetFullProducts());
        }

        // POST Index Action method
        [HttpPost]
        public IActionResult Index(decimal? lowAmount, decimal? largeAmount)
        {
            var products = _productManager.GetProductsBetweenPrice(lowAmount, largeAmount);
            if (lowAmount == null || largeAmount == null)
            {
                products = _productManager.GetFullProducts();
            }
            return View(products);
        }

        // Get Create method
        public IActionResult Create()
        {
            ViewData["productTypeId"] = new SelectList(_productTypesManager.GetAllProductTypes(), "Id", "ProductType");
            ViewData["TagId"] = new SelectList(_specialTagManager.GetAllSpecialTags(), "Id", "Name");
            return View();
        }

        // Post Create method
        [HttpPost]
        public async Task<IActionResult> Create(Dto_Product product, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var searchProduct = _productManager.GetProductByName(product.Name);
                if (searchProduct != null)
                {
                    ViewBag.message = "This product is already exist";
                    ViewData["productTypeId"] = new SelectList(_productTypesManager.GetAllProductTypes(), "Id", "ProductType");
                    ViewData["TagId"] = new SelectList(_specialTagManager.GetAllSpecialTags(), "Id", "Name");
                    return View(product);
                }

                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "Images/" + image.FileName;
                }

                if (image == null)
                {
                    product.Image = "Images/noImage.png";
                }
                await _productManager.AddProduct(product);
                TempData["save"] = "Product has been saved";
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        // GET Edit Action Method
        public ActionResult Edit(int? id)
        {
            ViewData["productTypeId"] = new SelectList(_productTypesManager.GetAllProductTypes(), "Id", "ProductType");
            ViewData["TagId"] = new SelectList(_specialTagManager.GetAllSpecialTags(), "Id", "Name");
            if (id == null)
            {
                return NotFound();
            }

            var product = _productManager.GetFullProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST Edit Action Method
        [HttpPost]
        public async Task<IActionResult> Edit(Dto_Product product, IFormFile image)
        {
            ModelState.Remove("Image");
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "Images/" + image.FileName;
                }
                if (image == null)
                {
                    if (product.Image == null)
                        product.Image = "Images/noImage.png";
                }

                await _productManager.UpdateProduct(product);
                TempData["edit"] = "Product has been updated";
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        // GET Details Action Method
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["productTypeId"] = new SelectList(_productTypesManager.GetAllProductTypes(), "Id", "ProductType");
            ViewData["TagId"] = new SelectList(_specialTagManager.GetAllSpecialTags(), "Id", "Name");
            if (id == null)
            {
                return NotFound();

            }
            var product = _productManager.GetFullProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET Delete Action Method
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _productManager.GetFullProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST Delete Action Method
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _productManager.GetProductByIdWithoutTags(id);
            if (product == null)
            {
                return NotFound();
            }
            _productManager.RemoveProduct(product);
            TempData["delete"] = "Product has been deleted";
            return RedirectToAction(nameof(Index));
        }
    }
}
