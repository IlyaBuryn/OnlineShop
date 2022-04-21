using BusinessLogic.DtoModels;

namespace BusinessLogic.Interfaces
{
    public interface IProductManager
    {
        IEnumerable<Dto_Product> GetFullProducts();
        IEnumerable<Dto_Product> GetProductsBetweenPrice(decimal? lowAmount, decimal? hightAmount);
        IEnumerable<Dto_Product> FindFullProductsThatContainName(string searchString);
        Dto_Product GetIncludeProductTypesProductsById(int? id);
        Dto_Product GetProductByName(string name);
        Task AddProduct(Dto_Product product);
        Dto_Product GetFullProductById(int? id);
        Dto_Product GetProductByIdWithoutTags(int? id);
        Task UpdateProduct(Dto_Product product);
        Task RemoveProduct(Dto_Product product);
        IEnumerable<Dto_Product> GetProductsByProductType(int productTypeId);

    }
}
