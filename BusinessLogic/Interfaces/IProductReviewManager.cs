using BusinessLogic.DtoModels;

namespace BusinessLogic.Interfaces
{
    public interface IProductReviewManager
    {
        Task<double?> GetAverageRate(Dto_Product product);
        Task<Dictionary<string, Dictionary<string, string>>> GetReviews(Dto_Product product);
        Task<int[]> GetCountOfrates(int rate, Dto_Product product);
    }
}
