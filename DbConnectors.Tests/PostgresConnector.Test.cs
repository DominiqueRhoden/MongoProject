using Xunit;
using DbConnectors;

namespace DbConnectors.Tests
{
    public class PostgresConnectorTest
    {
        private readonly string _connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=testdb";

        [Fact]
        public void Ping_ShouldReturnTrue_WhenPostgresIsRunning()
        {
            var connector = new PostgresConnector(_connectionString);
            var result = connector.Ping();
            Assert.True(result);
        }

        [Fact]
        public void InsertAndReadData_ShouldReturnCorrectValue()
        {
            var connector = new PostgresConnector(_connectionString);

            // Insert 20 data points
            for (int i = 1; i <= 20; i++)
            {
                connector.InsertData($"key{i}", $"value{i}");
            }

            // Retrieve a specific value
            var value = connector.ReadData("key10");
            Assert.Equal("value10", value);
        }
    }
}
