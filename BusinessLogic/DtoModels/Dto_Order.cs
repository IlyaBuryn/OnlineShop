using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.DtoModels
{
    public class Dto_Order
    {
        public Dto_Order()
        {
            OrderDetails = new List<Dto_OrderDetails>();
        }

        public int Id { get; set; }

        [Display(Name = "Order Number")]
        public string OrderNo { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNo { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual List<Dto_OrderDetails> OrderDetails { get; set; }
        public virtual Dto_DeliveryDetails DeliveryDetails { get; set; }
    }
}
