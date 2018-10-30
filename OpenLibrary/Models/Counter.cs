using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenLibrary.Models
{
    public class Counter
    {
        [BsonElement("_id")]
        public string _id { get; set; }
        [BsonElement("sequence_value")]
        public int sequence_value { get; set; }

        private IMongoDatabase db { get; set; }
        public Counter()
        {
            Dbconnect dbconnect = new Dbconnect();
            db = dbconnect.GetDB();
        }

        internal static int AI(string sequenceName, IMongoDatabase database)
        {
            
            var collection = database.GetCollection<Counter>("counters");
            var filter = Builders<Counter>.Filter.Eq(a => a._id, sequenceName);
            var update = Builders<Counter>.Update.Inc(a => a.sequence_value, 1);
            var sequence = collection.FindOneAndUpdate(filter, update);

            return sequence.sequence_value;
        }
    }
}
