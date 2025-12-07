namespace DbConnectors
{
    public interface IDBConnector
    {
        bool Ping(); // Step 4

        // Step 7: Insert and read data
        void InsertData(string key, string value);
        string ReadData(string key);
    }
}
