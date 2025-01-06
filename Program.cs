using System.Configuration;

namespace Coding_Tracker
{
    internal class Program
    {
        static string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");


        static void Main(string[] args)
        {
            DataBaseManager databaseManager = new();
            GetUserInput getUserInput = new();

            databaseManager.CreateTable(connectionString);
            getUserInput.MainMenu();
        }
    }
}
