namespace Lab_DKV.DataBase
{
    public static class DB
    {
        private static string connectionString =
            ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
