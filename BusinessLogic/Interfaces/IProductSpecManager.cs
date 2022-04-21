using BusinessLogic.DtoModels;

namespace BusinessLogic.Interfaces
{
    public interface IProductSpecManager
    {
        Task<string[]> GetProductDescription(Dto_Product product);
        Task<Dictionary<string, Dictionary<string, string>>> GetProductSpecs(Dto_Product product);
        Task<List<string>> GetProductColors(Dto_Product product);
    }
}
