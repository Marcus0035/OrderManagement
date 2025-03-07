namespace OrderManagement.Data.Models
{
    public class UserSettings
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;
    }
}
