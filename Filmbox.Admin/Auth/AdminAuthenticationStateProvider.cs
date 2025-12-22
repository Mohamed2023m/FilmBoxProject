using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Filmbox.Admin.Auth
{
    public class AdminAuthenticationStateProvider
     : AuthenticationStateProvider
    {
        private readonly JwtTokenStore _store;

        public AdminAuthenticationStateProvider(JwtTokenStore store)
        {
            _store = store;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (string.IsNullOrEmpty(_store.Token))
                return Task.FromResult(
                    new AuthenticationState(
                        new ClaimsPrincipal(new ClaimsIdentity())));

            var jwt = new JwtSecurityTokenHandler()
                .ReadJwtToken(_store.Token);

            var identity = new ClaimsIdentity(jwt.Claims, "jwt");
            return Task.FromResult(
                new AuthenticationState(
                    new ClaimsPrincipal(identity)));
        }

        public void SignIn(string token)
        {
            _store.Set(token);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void SignOut()
        {
            _store.Clear();
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }

}
