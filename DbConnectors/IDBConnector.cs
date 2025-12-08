namespace DbConnectors
{
    public interface IDBConnector
    {
        bool Ping();
        void InsertData(string key, string value);
        string ReadData(string key);
    }
}
