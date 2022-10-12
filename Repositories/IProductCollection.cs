using System;
using NET_Mongo.Models;

namespace NET_Mongo.Repositories
{
    
    interface IProductCollection
    {
        Task InsertProduct(Product product);

        Task UpdateProduct(Product product);

        Task DeleteProduct(string id);

        Task<List<Product>> GetAllProducts();

        Task<Product> GetProductById(string id);
    }
}