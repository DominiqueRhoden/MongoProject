using System;
using DbConnectors; // Must match the namespace in DBConnectors

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("==== DB REPL Application ====");
        Console.WriteLine("Choose a database:");
        Console.WriteLine("1. MongoDB");
        Console.WriteLine("2. PostgreSQL");

        Console.Write("Enter choice (1 or 2): ");
        string? choice = Console.ReadLine()?.Trim();

        Console.Write("Enter connection string: ");
        string? connString = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(choice) || string.IsNullOrWhiteSpace(connString))
        {
            Console.WriteLine("Choice and connection string cannot be empty.");
            return;
        }

        IDBConnector connector = null;

        switch (choice)
        {
            case "1":
                connector = new MongoDBConnector(connString);
                break;
            case "2":
                connector = new PostgresConnector(connString);
                break;
            default:
                Console.WriteLine("Invalid database choice.");
                return;
        }

        Console.WriteLine("Pinging database...");
        bool result;

        try
        {
            result = connector.Ping();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while pinging database: {ex.Message}");
            return;
        }

        Console.WriteLine(result ? "Ping successful!" : "Ping failed.");
    }
}
