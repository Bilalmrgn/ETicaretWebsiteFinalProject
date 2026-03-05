namespace ECommerce.WebUI.Services
{
    public interface ITokenService
    {
        Task<string> GetAccessToken(HttpContext context);
        Task<string> RefreshToken(HttpContext context, string refreshToken,string clientId);
    }
}
