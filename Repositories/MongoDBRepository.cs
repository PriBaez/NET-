using MongoDB.Driver;


namespace NET_Mongo.Repositories
{

    public class MongoDBRepository
    {
     public MongoClient client;

     public IMongoDatabase db;

     public MongoDBRepository()
     {
        try
        {
            client = new MongoClient("mongodb://mongonetdb:27017");

            db = client.GetDatabase("Inventory");

            Console.WriteLine("Conexion exitosa");

        }
        catch (System.AggregateException ex)
        {
            
            Console.WriteLine(ex);
        }

       
     }
    }
}