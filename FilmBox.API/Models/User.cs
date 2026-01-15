namespace FilmBox.Api.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; }
        


    }
}
