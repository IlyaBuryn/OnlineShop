using MongoDB.Driver;
using DataAccessLayer.Data.Interfaces;
using DataAccessLayer.SupModels;
using DataAccessLayer.Models;

namespace DataAccessLayer.Data.Repositories
{
    public class MongoReviewRepository : IReviewRepository
    {
        private MongoDbContext _context = new MongoDbContext();

        public async Task Add(ProductReview review)
        {
            try
            {
                await _context.Reviews.InsertOneAsync(review);
            }
            catch
            {
                throw;
            }
        }

        public async Task Delete(string id)
        {
            try
            {
                FilterDefinition<ProductReview> data = Builders<ProductReview>.Filter.Eq("Id", id);
                await _context.Reviews.DeleteOneAsync(data);
            }
            catch
            {
                throw;
            }
        }

        public async Task<ProductReview> GetProductReview(Products product)
        {
            try
            {
                FilterDefinition<ProductReview> filter = Builders<ProductReview>.Filter.Eq("ProductId", product.Id);
                return await _context.Reviews.Find(filter).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProductReview>> GetProductReviewsByProduct(Products product)
        {
            try
            {
                return await _context.Reviews.Find(x => x.ProductId == product.Id).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(ProductReview review)
        {
            try
            {
                await _context.Reviews.ReplaceOneAsync(filter: g => g.Id == review.Id, replacement: review);
            }
            catch
            {
                throw;
            }
        }
    }
}
