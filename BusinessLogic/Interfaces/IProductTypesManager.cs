using BusinessLogic.DtoModels;

namespace BusinessLogic.Interfaces
{
    public interface IProductTypesManager
    {
        List<Dto_ProductType> GetAllProductTypes();
        Task AddProductType(Dto_ProductType productType);
        Task UpdateProductType(Dto_ProductType productType);
        Task DeleteProductType(Dto_ProductType productType);
        Dto_ProductType FindProductType(int? id);
        IEnumerable<Dto_ProductType> GetFewRandomCategories(int count);
    }
}
