using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public class DeliveryDetails
    {
        public int Id { get; set; }

        [Display(Name = "DeliveryType")]
        public int DeliveryTypeId { get; set; }
        [ForeignKey("DeliveryTypeId")]
        public DeliveryType DeliveryType { get; set; }

        [Required, Display(Name = "Comment")]
        public string OrderComment { get; set; }

        [Display(Name = "PaymentType")]
        public int PaymentTypeId { get; set; }
        [ForeignKey("PaymentTypeId")]
        public PaymentType PaymentType { get; set; }
    }
}
