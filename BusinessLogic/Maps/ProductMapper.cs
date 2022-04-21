using BusinessLogic.DtoModels;
using BusinessLogic.Interfaces;
using DataAccessLayer.Models;

namespace BusinessLogic.Maps
{
    public class ProductMapper
    {
        public static List<Dto_Product> CastProductModelToDto(List<Products> items)
        {
            var result = new List<Dto_Product>();
            foreach (var item in items)
            {
                result.Add(new Dto_Product()
                {
                    Id = item.Id,
                    Image = item.Image,
                    IsAvailable = item.IsAvailable,
                    Name = item.Name,
                    Price = item.Price,
                    ProductTypeId = item.ProductTypeId,
                    SpecialTagId = item.SpecialTagId,
                    ProductTypes = new Dto_ProductType
                    {
                        Id = item.ProductTypes.Id,
                        ProductType = item.ProductTypes.ProductType
                    },
                    SpecialTag = new Dto_SpecialTag
                    {
                        Id = item.SpecialTag.Id,
                        Name = item.SpecialTag.Name
                    }
                });
            }
            return result;
        }

        public static Dto_Product CastProductModelToDto(Products item)
        {
            return new Dto_Product()
            {
                Id = item.Id,
                Image = item.Image,
                IsAvailable = item.IsAvailable,
                Name = item.Name,
                Price = item.Price,
                ProductTypeId = item.ProductTypeId,
                SpecialTagId = item.SpecialTagId,
                ProductTypes = new Dto_ProductType
                {
                    Id = item.ProductTypes.Id,
                    ProductType = item.ProductTypes.ProductType
                },
                SpecialTag = new Dto_SpecialTag
                {
                    Id = item.SpecialTag.Id,
                    Name = item.SpecialTag.Name
                }
            };
        }

        public static Products CastDtoToProductModel(Dto_Product item)
        {
            return new Products()
            {
                Id = item.Id,
                Image = item.Image,
                IsAvailable = item.IsAvailable,
                Name = item.Name,
                Price = item.Price,
                ProductTypeId = item.ProductTypeId,
                SpecialTagId = item.SpecialTagId,
                ProductTypes = new ProductTypes
                {
                    Id = item.ProductTypes.Id,
                    ProductType = item.ProductTypes.ProductType
                },
                SpecialTag = new SpecialTag
                {
                    Id = item.SpecialTag.Id,
                    Name = item.SpecialTag.Name
                }
            };
        }
    }
}
