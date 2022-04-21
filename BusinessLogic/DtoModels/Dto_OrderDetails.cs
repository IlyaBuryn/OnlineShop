using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.DtoModels
{
    public class Dto_OrderDetails
    {
        public int Id { get; set; }

        [Display(Name = "Order")]
        public int OrderId { get; set; }

        [Display(Name = "Product")]
        public int ProductId { get; set; }

        [ForeignKey("OrderId")]
        public Dto_Order Order { get; set; }


        [ForeignKey("ProductId")]
        public Dto_Product Product { get; set; }
    }
}
