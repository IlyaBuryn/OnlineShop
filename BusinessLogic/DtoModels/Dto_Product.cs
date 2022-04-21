using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.DtoModels
{
    public class Dto_Product
    {
        public int Id { get; set; }


        [Required]
        public string Name { get; set; }


        [Required]
        public decimal Price { get; set; }


        public string Image { get; set; }


        [Required]
        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }


        [Display(Name = "Product Type")]
        [Required]
        public int ProductTypeId { get; set; }


        [ForeignKey("ProductTypeId")]
        public Dto_ProductType ProductTypes { get; set; }


        [Display(Name = "Special Tag")]
        [Required]
        public int SpecialTagId { get; set; }


        [ForeignKey("SpecialTagId")]
        public Dto_SpecialTag SpecialTag { get; set; }


    }
}
