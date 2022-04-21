using DataAccessLayer.Models;
using DataAccessLayer.SupModels;

namespace DataAccessLayer.Data.Interfaces
{
    public interface IReviewRepository
    {
        Task Add(ProductReview review);
        Task Update(ProductReview review);
        Task Delete(string id);
        Task<ProductReview> GetProductReview(Products product);
        Task<IEnumerable<ProductReview>> GetProductReviewsByProduct(Products product);
    }
}
