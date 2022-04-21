using DataAccessLayer.Models;
using DataAccessLayer.SupModels;

namespace DataAccessLayer.Data.Interfaces
{
    public interface IDescriptionRepository
    {
        Task Add(ProductDesc desc);
        Task Update(ProductDesc desc);
        Task Delete(string id);
        Task<ProductDesc> GetProductDesc(string id);
        Task<ProductDesc> GetProductDescByProduct(Products product);
        Task<IEnumerable<ProductDesc>> GetAll();
    }
}
