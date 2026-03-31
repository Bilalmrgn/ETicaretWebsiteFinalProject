
using Duende.IdentityModel.Client;
using Frontend.DtosLayer.TokenResponseDtos;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ECommerce.WebUI.Services
{
    public class TokenService : ITokenService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public TokenService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public async Task<string> GetAccessToken(HttpContext context)
        {
            var accessToken = await context.GetTokenAsync("access_token");
            var refreshToken = await context.GetTokenAsync("refresh_token");

            if(string.IsNullOrEmpty(accessToken) )
            {
                return null;
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(accessToken);

            //token süre konttrolü
            var expClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == "exp")?.Value;

            if(expClaim != null )
            {
                var expireDate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expClaim));

                if (expireDate <= DateTimeOffset.UtcNow.AddSeconds(30))
                {
                    // ClientId'yi claimlerden alırken "client_id" bazen farklı isimlendirilebilir
                    var clientId = jwtToken.Claims.FirstOrDefault(x => x.Type == "client_id")?.Value;
                    accessToken = await RefreshToken(context, refreshToken, clientId);
                }
            }
            return accessToken;
        }

        public async Task<string> RefreshToken(HttpContext context,string refreshToken, string clientId)
        {
            var client = _httpClientFactory.CreateClient();

            //token i discovery ile alacağız
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:7222");

            if (disco.IsError)
            {
                return null;
            }

            var tokenResponse = await client.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = clientId,
                ClientSecret = _configuration["Client:clientSecret"],
                RefreshToken = refreshToken,
            });

            if(tokenResponse.IsError)
            {
                return null;
            }

            //cookie ve claims güncelleme
            var authResult = await context.AuthenticateAsync();

            authResult.Properties.UpdateTokenValue("access_token", tokenResponse.AccessToken);
            authResult.Properties.UpdateTokenValue("refresh_token", tokenResponse.RefreshToken);

            await context.SignInAsync(authResult.Principal, authResult.Properties);

            return tokenResponse.AccessToken;

        }
    }
}
