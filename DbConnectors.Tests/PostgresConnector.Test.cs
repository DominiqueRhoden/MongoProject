using Xunit;
using DbConnectors;
using Testcontainers.PostgreSql;
using System.Threading.Tasks;

namespace DbConnectors.Tests
{
    public class PostgresConnectorTest
    {
        [Fact]
        public async Task Ping_ShouldReturnTrue_WhenPostgresIsRunning()
        {
            await using var postgresContainer = new PostgreSqlBuilder()
                .WithImage("postgres:16")
                .WithUsername("postgres")
                .WithPassword("postgres")
                .WithDatabase("testdb")
                .Build();

            await postgresContainer.StartAsync();

            var connector = new PostgresConnector(postgresContainer.GetConnectionString());
            var result = connector.Ping();
            Assert.True(result);

            await postgresContainer.StopAsync();
        }

        [Fact]
        public async Task InsertAndReadData_ShouldReturnCorrectValue()
        {
            await using var postgresContainer = new PostgreSqlBuilder()
                .WithImage("postgres:16")
                .WithUsername("postgres")
                .WithPassword("postgres")
                .WithDatabase("testdb")
                .Build();

            await postgresContainer.StartAsync();

            var connector = new PostgresConnector(postgresContainer.GetConnectionString());

            for (int i = 1; i <= 20; i++)
            {
                connector.InsertData($"key{i}", $"value{i}");
            }

            var value = connector.ReadData("key10");
            Assert.Equal("value10", value);

            await postgresContainer.StopAsync();
        }
    }
}
