using BusinessLogic.DtoModels;
using BusinessLogic.Interfaces;
using BusinessLogic.Maps;
using DataAccessLayer.Data.Interfaces;

namespace BusinessLogic.Services
{
    public class ProductReviewManager : IProductReviewManager
    {
        private readonly IReviewRepository repository;

        public ProductReviewManager(IReviewRepository _ropository)
        {
            repository = _ropository;
        }
        public async Task<double?> GetAverageRate(Dto_Product product)
        {
            var items = await GetReviews(product);
            var result = 0;
            if (items != null)
            {
                foreach (var item in items)
                {
                    result += int.Parse(item.Value.Where(x => x.Key == "Rate").Select(x => x.Value).First());
                }
                return result / items.Count;
            }
            else
            {
                return null;
            }
        }

        public async Task<Dictionary<string, Dictionary<string, string>>> GetReviews(Dto_Product product)
        {
            var item = await repository.GetProductReview(ProductMapper.CastDtoToProductModel(product));
            if (item != null)
            {
                var expand = item.Reviews;
                return Splitter.ConvertExpandObjectToDictionary(expand);
            }
            return null;
        }

        public async Task<int[]> GetCountOfrates(int rate, Dto_Product product)
        {
            int result = 0;
            var items = await GetReviews(product);
            if (items != null)
            {
                foreach (var item in items)
                {
                    int tmp = int.Parse(item.Value.Where(x => x.Key == "Rate").Select(x => x.Value).First());
                    if (tmp == rate)
                        result++;
                }
                int percent = (int)System.Math.Round(((result / items.Count()) * 100.0));
                return new int[] { result, percent };
            }
            return new int[] { 0, 0 };
        }
    }
}
