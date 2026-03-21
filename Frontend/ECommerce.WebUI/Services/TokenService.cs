
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
        public TokenService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<string> GetAccessToken(HttpContext context)
        {

            var accessToken = await context.GetTokenAsync("Cookies", "access_token");
            var refreshToken = await context.GetTokenAsync("Cookies", "refresh_token");

            if (string.IsNullOrEmpty(accessToken))
            {
                return null;
            }

            //token içerisindeki bilgileri okumak için
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(accessToken);

            var expire = jwtToken.Claims.FirstOrDefault(x => x.Type == "exp")?.Value;

            var clientId = jwtToken.Claims.FirstOrDefault(x => x.Type == "client_id").Value;

            if (expire != null)//yani exp == süresi dolmuş ise if içine gireceksin
            {
                var expireDate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expire));

                if (expireDate <= DateTime.UtcNow)
                {
                    accessToken = await RefreshToken(context, refreshToken, clientId);
                }
            }
            return accessToken;
        }

        public async Task<string> RefreshToken(HttpContext context, string refreshToken, string clientId)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.PostAsync(
            "https://localhost:5001/connect/token",
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"grant_type","refresh_token"},
                {"refresh_token",refreshToken},
                {"client_id", clientId},
        {"client_secret","ecommercesecret"}
            }));

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            //ekleme ve güncelleme işlemlerinde serialize 
            //listeleme işlemlerinde deserialize kullanılır
            //refresh token da bir getirme yani listeleme işlemidir
            var json = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<TokenResponseDto>(json);

            var auth = await context.AuthenticateAsync();

            // access token güncellenir
            auth.Properties.UpdateTokenValue("access_token", token.access_token);

            // refresh token güncellenir
            auth.Properties.UpdateTokenValue("refresh_token", token.refresh_token);

            // cookie yeniden yazılır
            await context.SignInAsync(
                auth.Principal,
                auth.Properties);

            return token.access_token;
        }
    }
}
