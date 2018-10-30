using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenLibrary.Models
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
