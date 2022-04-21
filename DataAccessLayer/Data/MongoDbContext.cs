using DataAccessLayer.SupModels;
using MongoDB.Driver;

namespace DataAccessLayer.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _mongoDb;
        public MongoDbContext()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _mongoDb = client.GetDatabase("OnlineStore");
        }
        public IMongoCollection<ProductDesc> desc
        {
            get
            {
                return _mongoDb.GetCollection<ProductDesc>("ProductContent");
            }
        }
        public IMongoCollection<ProductReview> Reviews
        {
            get
            {
                return _mongoDb.GetCollection<ProductReview>("ProductRate");
            }
        }
    }
}
