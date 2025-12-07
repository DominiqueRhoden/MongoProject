using MongoDB.Driver;
using MongoDB.Bson;

namespace DbConnectors
{
    public class MongoDBConnector : IDBConnector
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<BsonDocument> _collection;

        public MongoDBConnector(string connectionString)
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase("testDB");
            _collection = _database.GetCollection<BsonDocument>("testCollection");
        }

        public bool Ping()
        {
            try
            {
                var command = new BsonDocument("ping", 1);
                _database.RunCommand<BsonDocument>(command);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void InsertData(string key, string value)
        {
            var doc = new BsonDocument { { "key", key }, { "value", value } };
            _collection.InsertOne(doc);
        }

        public string ReadData(string key)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("key", key);
            var doc = _collection.Find(filter).FirstOrDefault();
            return doc != null ? doc["value"].AsString : null;
        }
    }
}
