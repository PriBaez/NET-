using NET_Mongo.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System;

namespace NET_Mongo.Repositories
{

    public class ProductCollection: IProductCollection
    {
        internal MongoDBRepository _repository = new MongoDBRepository();

        private IMongoCollection<Product> Collection;

        public ProductCollection()
        {
            Collection = _repository.db.GetCollection<Product>("Products");
        }

        public async Task InsertProduct(Product product)
        {
            await Collection.InsertOneAsync(product);
        }

        public async Task UpdateProduct(Product product)
        {
            var filter = Builders<Product>.Filter.Eq(s => s.Id, product.Id);

            await Collection.ReplaceOneAsync(filter, product);
        }

        public async Task DeleteProduct(string id)
        {
            //crear filtro con el parametro recibido
            var filter = Builders<Product>.Filter.Eq(s => s.Id, new ObjectId(id));
            
            //esperar que la MongoDB encuentre el ObjectId correspondiente al filter
            //y que ejecute la tarea de borrar 
            await Collection.DeleteOneAsync(filter);
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await Collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<Product> GetProductById(string id)
        {
            return await Collection.FindAsync(new BsonDocument { {"_id", new ObjectId(id)} } ).
            Result.FirstAsync();
        }
    }
    
}