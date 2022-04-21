using BusinessLogic.DtoModels;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly IProductManager _productManager;
        private readonly IOrderManager _orderManager;

        public OrderController(IProductManager productManager,
            IOrderManager orderManager)
        {
            _productManager = productManager;
            _orderManager = orderManager;
        }

        // GET Checkout action method
        public IActionResult Checkout()
        {
            return View();
        }

        // POST Checkout action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(Dto_Order anOrder)
        {
            List<Dto_Product> products = HttpContext.Session.Get<List<Dto_Product>>("products");
            if (products != null)
            {
                foreach (var product in products)
                {
                    Dto_OrderDetails orderDetails = new()
                    {
                        ProductId = product.Id
                    };
                    anOrder.OrderDetails.Add(orderDetails);
                }
            }
            else
            {
                return Redirect("/Customer/Home");
            }

            anOrder.OrderDate = DateTime.Now;
            anOrder.OrderNo = GetOrderNo();
            await _orderManager.AddOrder(anOrder);
            HttpContext.Session.Set("products", new List<Dto_Product>());
            return Redirect("/Customer/Home"); // TODO: Make speial page fo additional information
        }


        public string GetOrderNo()
        {
            int rowCount = _orderManager.GetGeneralOrderCount();
            return rowCount.ToString("000");
        }
    }
}
