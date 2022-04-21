using BusinessLogic.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Areas.Customer.Models
{
    public class HomeControllerIndexModel
    {
        public X.PagedList.IPagedList<Dto_Product> Products { get; set; }
        public IEnumerable<Dto_ProductType> ProductTypes { get; set; }
    }
}
