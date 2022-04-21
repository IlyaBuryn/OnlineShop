using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BusinessLogic.DtoModels
{
    public class Dto_Mongo_ProductReview
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int ProductId { get; set; }
        public Object Reviews { get; set; }
    }
}
