using BusinessLogic.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Areas.Customer.Models
{
    public class OrderModel
    {
        public Dto_Order Order { get; set; }
        public Dto_DeliveryDetails Delivery { get; set; }
    }
}
