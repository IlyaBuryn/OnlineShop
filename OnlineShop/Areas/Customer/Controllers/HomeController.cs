﻿using BusinessLogic.DtoModels;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Areas.Customer.Models;
using OnlineShop.Utility;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace OnlineShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IProductManager _productManager;
        private readonly IProductReviewManager _productReviewManager;
        private readonly IProductSpecManager _productSpecManager;
        private readonly IApplicationUserManager _userManager;
        private readonly IProductTypesManager _productTypesManager;

        public HomeController(IProductManager productManager,
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

        public IActionResult Index(int? page)
        {
            var resultItem = new HomeControllerIndexModel();
            resultItem.Products = _productManager.GetFullProducts().ToPagedList(page ?? 1, 16);
            resultItem.ProductTypes = _productTypesManager.GetFewRandomCategories(8);
            return View(resultItem);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new Dto_ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }






        public async Task<IActionResult> Detail(int? id)
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

            await MakeViewData(product);

            return View(product);
        }


        [HttpPost]
        public async Task<IActionResult> AddToCart(int? id)
        {
            List<Dto_Product> products;
            if (id == null)
            {
                return NotFound();
            }

            var product = _productManager.GetFullProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            products = HttpContext.Session.Get<List<Dto_Product>>("products");
            if (products == null)
            {
                products = new List<Dto_Product>();
            }

            await MakeViewData(product);

            products.Add(product);
            HttpContext.Session.Set("products", products);
            if (HttpContext.Session.Get<bool>("IsFromDetails") == true)
                return RedirectToAction(nameof(Detail), new { id = id });
            else
                return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> AddToWishlist(int? id)
        {
            List<Dto_Product> products;
            if (id == null)
            {
                return NotFound();
            }

            var product = _productManager.GetFullProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            products = HttpContext.Session.Get<List<Dto_Product>>("wishlist");
            if (products == null)
            {
                products = new List<Dto_Product>();
            }

            await MakeViewData(product);

            products.Add(product);
            HttpContext.Session.Set("wishlist", products);
            if (HttpContext.Session.Get<bool>("IsFromDetails") == true)
                return RedirectToAction(nameof(Detail), new {id = id});
            else
                return RedirectToAction(nameof(Index));

        }


        [ActionName("Remove")]
        public IActionResult RemoveToCart(int? id)
        {
            List<Dto_Product> products = HttpContext.Session.Get<List<Dto_Product>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }

            return products.Count == 0 ? RedirectToAction(nameof(Index)) :
                RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        public IActionResult Remove(int? id)
        {
            List<Dto_Product> products = HttpContext.Session.Get<List<Dto_Product>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Detail), new { id = id });
        }





        public IActionResult Cart()
        {
            List<Dto_Product> products = HttpContext.Session.Get<List<Dto_Product>>("products");
            if (products == null)
            {
                products = new List<Dto_Product>();
            }
            return View(products);
        }

        public IActionResult WishList()
        {
            List<Dto_Product> products = HttpContext.Session.Get<List<Dto_Product>>("wishlist");
            if (products == null)
            {
                products = new List<Dto_Product>();
            }
            return View(products);
        }




        private async Task MakeViewData(Dto_Product product)
        {
            ViewData["productDesc"] = await _productSpecManager.GetProductDescription(product);
            ViewData["productChars"] = await _productSpecManager.GetProductSpecs(product);
            ViewData["reviewRate"] = await _productReviewManager.GetAverageRate(product);
            ViewData["reviews"] = await _productReviewManager.GetReviews(product);
            ViewData["colors"] = await _productSpecManager.GetProductColors(product);

            ViewData["rateof5"] = await _productReviewManager.GetCountOfrates(5, product);
            ViewData["rateof4"] = await _productReviewManager.GetCountOfrates(4, product);
            ViewData["rateof3"] = await _productReviewManager.GetCountOfrates(3, product);
            ViewData["rateof2"] = await _productReviewManager.GetCountOfrates(2, product);
            ViewData["rateof1"] = await _productReviewManager.GetCountOfrates(1, product);
        }
    }
}
