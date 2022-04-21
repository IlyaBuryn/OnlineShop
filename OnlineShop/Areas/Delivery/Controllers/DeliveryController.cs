using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OnlineShop.Areas.Delivery.Controllers
{
    [Area("Delivery")]
    public class DeliveryController : Controller
    {

        public DeliveryController()
        {

        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> StartDelivery()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> StartDelivery(int? id)
        {
            return View();
        }

        public async Task<IActionResult> Edit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id)
        {
            return View();
        }

        public async Task<IActionResult> Details()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Details(int? id)
        {
            return View();
        }
    }
}
