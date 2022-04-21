﻿using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class ProductTypes
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Product Type")]
        public string ProductType { get; set; }

        public string Image { get; set; }

    }
}
