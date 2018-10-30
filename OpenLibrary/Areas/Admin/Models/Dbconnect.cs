using MongoDB.Driver;  

namespace OpenLibrary.Areas.Admin.Models
{
    public class Dbconnect
    {
        private IMongoDatabase mongoDB;
        
        public Dbconnect()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            mongoDB = mongoClient.GetDatabase("open_library_db");
        }

        public IMongoDatabase GetDB()
        {
            return mongoDB;
        }
    }
}
