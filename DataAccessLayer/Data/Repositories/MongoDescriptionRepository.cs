using DataAccessLayer.Data.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.SupModels;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace DataAccessLayer.Data.Repositories
{
    public class MongoDescriptionRepository : IDescriptionRepository
    {
        private MongoDbContext _context = new MongoDbContext();
        public async Task Add(ProductDesc desc)
        {
            try
            {
                await _context.desc.InsertOneAsync(desc);
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
                FilterDefinition<ProductDesc> data = Builders<ProductDesc>.Filter.Eq("Id", id);
                await _context.desc.DeleteOneAsync(data);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProductDesc>> GetAll()
        {
            try
            {
                return await _context.desc.Find(_ => true).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<ProductDesc> GetProductDesc(string id)
        {
            try
            {
                FilterDefinition<ProductDesc> filter = Builders<ProductDesc>.Filter.Eq("Id", id);
                return await _context.desc.Find(filter).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<ProductDesc> GetProductDescByProduct(Products product)
        {
            try
            {
                FilterDefinition<ProductDesc> filter = Builders<ProductDesc>.Filter.Eq("ProductId", product.Id);
                return await _context.desc.Find(filter).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(ProductDesc desc)
        {
            try
            {
                await _context.desc.ReplaceOneAsync(filter: g => g.Id == desc.Id, replacement: desc);
            }
            catch
            {
                throw;
            }
        }
    }
}
