using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DtoModels
{
    public class Dto_ProductType
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Product Type")]
        public string ProductType { get; set; }

        public string Image { get; set; }
    }
}
