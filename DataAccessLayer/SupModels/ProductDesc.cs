using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccessLayer.SupModels
{
    public class ProductDesc
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Object ProductCharacteristics { get; set; }
        public string ProductDescription { get; set; }
        public int ProductId { get; set; }
        public Object Colors { get; set; }
    }
}
