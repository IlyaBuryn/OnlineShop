using BusinessLogic.DtoModels;
using DataAccessLayer.Models;

namespace BusinessLogic.Maps
{
    public class ProductTypesMapper
    {
        public static List<Dto_ProductType> CastProductTypeModelToDto(List<ProductTypes> items)
        {
            var result = new List<Dto_ProductType>();
            foreach (var item in items)
            {
                result.Add(new Dto_ProductType
                {
                    Id = item.Id,
                    ProductType = item.ProductType,
                    Image = item.Image
                });
            }
            return result;
        }

        public static Dto_ProductType CastProductTypeModelToDto(ProductTypes item)
        {
            if (item != null)
                return new Dto_ProductType()
                {
                    Id = item.Id,
                    ProductType = item.ProductType,
                    Image = item.Image
                };
            else return null;
        }

        public static ProductTypes CastDtoToProductTypeModel(Dto_ProductType item)
        {
            return new ProductTypes()
            {
                Id = item.Id,
                ProductType = item.ProductType,
                Image = item.Image
            };
        }
    }
}
