using BusinessLogic.DtoModels;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.Areas.Customer.Models;
using OnlineShop.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly IProductManager _productManager;
        private readonly IOrderManager _orderManager;

        private readonly ICustomGenericService<Dto_PaymentType> _paymentTypesManager;
        private readonly ICustomGenericService<Dto_DeliveryType> _deliveryTypesManeger;
        private readonly ICustomGenericServiceAsync<Dto_DeliveryDetails> _deliveryManager;
        private readonly ICustomGenericServiceAsyncMembers<Dto_DeliveryStatus> _deliveryStatusManager;

        private readonly IProductReviewManager _productReviewManager;
        private readonly IProductSpecManager _productSpecManager;
        private readonly IApplicationUserManager _userManager;
        private readonly IProductTypesManager _productTypesManager;

        public OrderController(IProductManager productManager,
            IOrderManager orderManager,
            ICustomGenericService<Dto_PaymentType> paymentTypesManager,
            ICustomGenericService<Dto_DeliveryType> deliveryTypesManeger,
            ICustomGenericServiceAsync<Dto_DeliveryDetails> deliveryManager,
            ICustomGenericServiceAsyncMembers<Dto_DeliveryStatus> deliveryStatusManager,
            IProductReviewManager productReviewManager,
            IProductSpecManager productSpecManager,
            IApplicationUserManager userManager,
            IProductTypesManager productTypesManager)
        {
            _productManager = productManager;
            _orderManager = orderManager;
            _paymentTypesManager = paymentTypesManager;
            _deliveryTypesManeger = deliveryTypesManeger;
            _deliveryManager = deliveryManager;
            _productReviewManager = productReviewManager;
            _productSpecManager = productSpecManager;
            _userManager = userManager;
            _productTypesManager = productTypesManager;
            _deliveryStatusManager = deliveryStatusManager;
        }
        
        [HttpGet]
        public IActionResult Checkout()
        {
            var products = HttpContext.Session.Get<List<Dto_Product>>("products");
            ViewData["deliveryTypes"] = new SelectList(_deliveryTypesManeger.GetAllObjects(), "Id", "Name");
            ViewData["paymentTypes"] = new SelectList(_paymentTypesManager.GetAllObjects(), "Id", "Name");
            if (products == null)
            {
                return RedirectToAction(nameof(EmptyCart));
            }
            if (products.Count == 0)
            {
                return RedirectToAction(nameof(EmptyCart));
            }
            HttpContext.Session.Set("deliveryTypes", _deliveryTypesManeger.GetAllObjects());
            HttpContext.Session.Set("payTypes", _paymentTypesManager.GetAllObjects());
            return View();
        }

        [HttpGet]
        public IActionResult EmptyCart()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(OrderModel anOrder)
        {
            var products = HttpContext.Session.Get<List<Dto_Product>>("products");
            if (products != null)
            {
                foreach (var product in products)
                {
                    Dto_OrderDetails orderDetails = new()
                    {
                        ProductId = product.Id,                      
                    };
                    anOrder.Order.OrderDetails.Add(orderDetails);
                }

                Dto_DeliveryDetails deliveryDetails = anOrder.Delivery;

                anOrder.Order.DeliveryDetails = deliveryDetails;
                anOrder.Order.DeliveryDetails.IsBusyShipping = false;
                if (anOrder.Order.DeliveryDetails.PaymentTypeId == 4)
                    anOrder.Order.DeliveryDetails.DeliveryStatusId = 8;
                else
                    anOrder.Order.DeliveryDetails.DeliveryStatusId = 1;

            }
            else
            {
                return Redirect("/Customer/Home");
            }


            anOrder.Order.OrderDate = DateTime.Now;
            anOrder.Order.OrderNo = GetOrderNo();
            var ordId = await _orderManager.AddOrder(anOrder.Order);

            anOrder.Delivery.OrderId = ordId;
            await _deliveryManager.CreateObjectAsync(anOrder.Order.DeliveryDetails);

            HttpContext.Session.Set("products", new List<Dto_Product>());
            return RedirectToAction(nameof(Invoice), new { id = ordId });
        }


        public string GetOrderNo()
        {
            int rowCount = _orderManager.GetGeneralOrderCount();
            return rowCount.ToString("000");
        }


        [HttpGet]
        public IActionResult Invoice(int id)
        {
            var model = _orderManager.GetOrderById(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Pay(int? id)
        {
            var model = _orderManager.GetOrderById((int)id);
            if (model == null)
            {
                return NotFound();
            }
            model.DeliveryDetails.DeliveryStatusId = 1;
            await _orderManager.UpdateOrder(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult Pay()
        {
            TempData["save"] = "Purchase paid";
            return RedirectToAction("Index", "Home");
        }

        [ActionName("Remove")]
        public IActionResult RemoveToCart(int? id)
        {
            var products = HttpContext.Session.Get<List<Dto_Product>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }

            return products.Count == 0 ? RedirectToAction(nameof(EmptyCart)) :
                RedirectToAction(nameof(Checkout));
        }

        [HttpPost]
        public IActionResult Remove(int? id)
        {
            var products = HttpContext.Session.Get<List<Dto_Product>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Checkout));
        }


        public IActionResult Cart()
        {
            var products = HttpContext.Session.Get<List<Dto_Product>>("products");
            if (products == null)
            {
                products = new List<Dto_Product>();
            }
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int? id)
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

            var products = HttpContext.Session.Get<List<Dto_Product>>("products");
            if (products == null)
            {
                products = new List<Dto_Product>();
            }


            products.Add(product);
            HttpContext.Session.Set("products", products);

            TempData["Save"] = "Added to cart";

            if (HttpContext.Session.Get<bool>("IsFromDetails") == true)
                return RedirectToAction("Detail", "Home", new { id = id });
            else
                return RedirectToAction("Index", "Home");

        }
    }
}
