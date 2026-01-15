namespace Filmbox.Admin.Auth
{
    public class JwtTokenStore
    {
        public string? Token { get; private set; }
        public void Set(string token) => Token = token;
        public void Clear() => Token = null;
    }
}
