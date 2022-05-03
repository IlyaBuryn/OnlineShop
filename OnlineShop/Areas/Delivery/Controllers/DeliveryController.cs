using BusinessLogic.DtoModels;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OnlineShop.Areas.Delivery.Controllers
{
    [Area("Delivery")]
    [Authorize(Roles = "Delivery")]
    public class DeliveryController : Controller
    {
        private readonly IOrderManager _orderManager;
        public DeliveryController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        // Get general/major info about delivery person ond current orders/
        public IActionResult Index()
        {
            return View(_orderManager.GetFreeForDeliveryOrders());
        }

        [HttpGet]
        public async Task<IActionResult> TakeOrder(int? id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TakeOrder(Dto_Order anOrder)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var anOrder = _orderManager.GetOrderById((int)id);
            if (anOrder == null)
            {
                return NotFound();
            }
            return View(anOrder);
        }

        [HttpPost]
        public async Task<IActionResult> Details(Dto_Order anOrder)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> MyOrders()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ChangeStatus(int? id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(Dto_Order anOrder)
        {
            return View();
        }
    }
}
