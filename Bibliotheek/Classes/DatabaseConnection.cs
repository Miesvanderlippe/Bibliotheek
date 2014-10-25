using MySql.Data.MySqlClient;

namespace Bibliotheek.Classes
{
    public static class DatabaseConnection
    {
        // Database vars 
        private const string MysqlDatabase   = "db71989";
        private const string MysqlPassword   = "usbw";
        private const string MysqlServer     = "192.178.1.15";
        private const string MysqlUsername   = "root";

        // <summary>
        // Close a database connection 
        // </summary>
        public static void DatabaseClose(MySqlConnection emp)
        {
            emp.Close();
        }

        // <summary>
        // Create connection with the database 
        // </summary>
        public static MySqlConnection DatabaseConnect()
        {
            const string connectionString = "Server=" + MysqlServer + ";Port=3306;Database="
            + MysqlDatabase + ";Uid=" + MysqlUsername +
            ";Pwd=" + MysqlPassword;

            var empConnection = new MySqlConnection(connectionString);

            return empConnection;
        }

        // <summary>
        // Open a database connection 
        // </summary>
        public static void DatabaseOpen(MySqlConnection emp)
        {
            emp.Open();
        }
    }
}