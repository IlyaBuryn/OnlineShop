using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DtoModels
{
    public class Dto_DeliveryDetails
    {
        public int Id { get; set; }

        [Display(Name = "Order")]
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Dto_Order Order { get; set; }

        [Display(Name = "DeliveryType")]
        public int DeliveryTypeId { get; set; }
        [ForeignKey("DeliveryTypeId")]
        public Dto_DeliveryType DeliveryType { get; set; }

        [Required, Display(Name = "Comment")]
        public string OrderComment { get; set; }

        [Display(Name = "PaymentType")]
        public int PaymentTypeId { get; set; }
        [ForeignKey("PaymentTypeId")]
        public Dto_PaymentType PaymentType { get; set; }



        [Display(Name = "DeliveryStatus")]
        public int DeliveryStatusId { get; set; }
        [ForeignKey("DeliveryStatusId")]
        public Dto_DeliveryStatus DeliveryStatus { get; set; }

        public bool? IsBusyShipping { get; set; }
    }
}
