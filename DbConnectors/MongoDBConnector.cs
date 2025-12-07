using MongoDB.Driver;

namespace DbConnectors
{
    public class MongoDBConnector
    {
        private readonly MongoClient _client;

        public MongoDBConnector(string connectionString)
        {
            _client = new MongoClient(connectionString);
        }
    }
}
