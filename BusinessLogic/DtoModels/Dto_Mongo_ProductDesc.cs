using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BusinessLogic.DtoModels
{
    public class Dto_Mongo_ProductDesc
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Object ProductCharacteristics { get; set; }
        public string ProductDescription { get; set; }
        public int ProductId { get; set; }
        public Object Colors { get; set; }
    }
}
