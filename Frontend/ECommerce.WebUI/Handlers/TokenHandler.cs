using ECommerce.WebUI.Services;
using System.Net.Http.Headers;

namespace ECommerce.WebUI.Handlers
{
    public class TokenHandler : DelegatingHandler
    {
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _contextAccessor;

        public TokenHandler(ITokenService tokenService, IHttpContextAccessor contextAccessor)
        {
            _tokenService = tokenService;
            _contextAccessor = contextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _tokenService.GetAccessToken(_contextAccessor.HttpContext);

            if(!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
