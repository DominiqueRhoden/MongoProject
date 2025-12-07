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

        [Fact]
        public void Ping_ShouldReturnFalse_WhenConnectionIsInvalid()
        {
            var connector = new MongoDBConnector("mongodb://invalid:27017");
            var result = connector.Ping();
            Assert.False(result);
        }

        [Fact]
        public async Task InsertAndReadData_ShouldReturnCorrectValue()
        {
            await using var mongoContainer = new MongoDbBuilder()
                .WithImage("mongo:7.0")
                .Build();

            await mongoContainer.StartAsync();

            var connector = new MongoDBConnector(mongoContainer.GetConnectionString());

            for (int i = 1; i <= 20; i++)
            {
                connector.InsertData($"key{i}", $"value{i}");
            }

            var value = connector.ReadData("key10");
            Assert.Equal("value10", value);

            await mongoContainer.StopAsync();
        }
    }
}
