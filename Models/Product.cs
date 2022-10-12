using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NET_Mongo.Models
{

    public class Product
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        public int Stock { get; set; }

        public DateTime ExpiryDate { get; set; }

        public string category { get; set; }
    }
}