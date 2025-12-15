namespace Lab_DKV
{
    public static class Session
    {
        public static int UserId { get; set; } = 0;
        public static string UserName { get; set; } = "";
        public static string Role { get; set; } = "";

        public static void Clear()
        {
            UserId = 0;
            UserName = "";
            Role = "";
        }
    }
}