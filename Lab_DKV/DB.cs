using MySql.Data.MySqlClient;

namespace Lab_DKV
{
    public static class DB
    {
        private static readonly string connectionString =
            "server=localhost;user id=root;password=;database=lab_dkv";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
