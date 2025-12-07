using System;
using Npgsql;

namespace DbConnectors
{
    public class PostgresConnector : IDBConnector
    {
        private readonly string _connectionString;

        public PostgresConnector(string connectionString)
        {
            _connectionString = connectionString;

            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            using var cmd = new NpgsqlCommand(
                "CREATE TABLE IF NOT EXISTS test_table(key TEXT PRIMARY KEY, value TEXT);", conn);
            cmd.ExecuteNonQuery();
        }

        public bool Ping()
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                conn.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void InsertData(string key, string value)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            using var cmd = new NpgsqlCommand(
                "INSERT INTO test_table(key, value) VALUES(@key, @value) ON CONFLICT(key) DO NOTHING;", conn);
            cmd.Parameters.AddWithValue("key", key);
            cmd.Parameters.AddWithValue("value", value);
            cmd.ExecuteNonQuery();
        }

        public string ReadData(string key)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            using var cmd = new NpgsqlCommand("SELECT value FROM test_table WHERE key = @key;", conn);
            cmd.Parameters.AddWithValue("key", key);
            var result = cmd.ExecuteScalar();
            return result != null ? result.ToString() : null;
        }
    }
}
