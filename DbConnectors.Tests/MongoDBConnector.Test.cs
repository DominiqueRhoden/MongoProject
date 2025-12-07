using Xunit;
using DbConnectors;
using Testcontainers.MongoDb;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace DbConnectors.Tests
{
    public class MongoDBConnectorTest
    {
        [Fact]
        public void Ping_ShouldReturnFalse_WhenConnectionIsInvalid()
        {
            var connector = new MongoDBConnector("mongodb://invalid:27017");
            var result = connector.Ping();
            Assert.False(result);
        }

        public async Task Ping_ShouldReturnTrue_WhenMongoIsRunning()
        {
            await using var mongoContainer = new MongoDbBuilder()
                .WithImage("mongo:7.0")
                .Build();

            await mongoContainer.StartAsync();

            var connector = new MongoDBConnector(mongoContainer.GetConnectionString());
            var result = connector.Ping();

            Assert.True(result);

            await mongoContainer.StopAsync();
        }
    }
}
