using BusinessLogic.DtoModels;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Areas.Customer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace OnlineShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class SearchController : Controller
    {
        private readonly IProductManager _productManager;
        private readonly IProductReviewManager _productReviewManager;
        private readonly IProductSpecManager _productSpecManager;
        private readonly IApplicationUserManager _userManager;
        private readonly IProductTypesManager _productTypesManager;

        public SearchController(IProductManager productManager,
            IProductReviewManager productReviewManager,
            IProductSpecManager productSpecManager,
            IApplicationUserManager userManager,
            IProductTypesManager productTypesManager)
        {
            _productManager = productManager;
            _productReviewManager = productReviewManager;
            _productSpecManager = productSpecManager;
            _userManager = userManager;
            _productTypesManager = productTypesManager;
        }

        public async Task<IActionResult> SearchIndex(int? page, string searchString)
        {
            List<Dto_Product> products = _productManager.FindFullProductsThatContainName(searchString).ToList();
            if (products == null)
            {
                return NotFound();
            }
            ViewBag.SearchString = searchString;
            return View(products.ToPagedList(page ?? 1, 1));
        }


        public async Task<IActionResult> SearchByProductType(int? page, int? id)
        {
            if (id == null)
                return NotFound();
            var products = _productManager.GetProductsByProductType((int)id).ToList();

            if (products == null)
            {
                return NotFound();
            }

            string type = _productTypesManager.FindProductType(id).ProductType;
            ViewBag.TypeString = type ?? "Category";
            return View(products.ToPagedList(page ?? 1, 1));

        }
    }
}
