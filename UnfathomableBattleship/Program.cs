using UnfathomableBattleship.Forms;
using UnfathomableBattleship.Interfaces;
using UnfathomableBattleship.Services;

namespace UnfathomableBattleship;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        IAuthenticationService authService = new AuthenticationService(getDBConnectionString());
        Application.Run(new MainForm(authService));
    }

    private static string getDBConnectionString()
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string dbPath = Path.Combine(baseDirectory, "DataBase", "UBS_DB.db");
        string connectionString = $"Data Source={dbPath};Version=3;Foreign Keys=True;";
        return connectionString;
    }
}
