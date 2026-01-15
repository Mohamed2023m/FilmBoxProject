namespace FilmBox.Api.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
        public int UserId { get; internal set; }
    }
}
