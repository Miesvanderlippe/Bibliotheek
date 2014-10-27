using MySql.Data.MySqlClient;

namespace Bibliotheek.Classes
{
    public static class DatabaseConnection
    {
        // Database vars 
        private const string MysqlDatabase   = "db71989";
        private const string MysqlPassword   = "usbw";
        private const string MysqlServer     = "localhost";
        private const string MysqlUsername   = "root";
        private const string MysqlPort       = "3307";
        // LET OP!! MYSQL POORT IS OP SCHOOL 1 POORT LAGER!

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
            const string connectionString = "Server=" + MysqlServer + ";Port=" + MysqlPort + ";Database="
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