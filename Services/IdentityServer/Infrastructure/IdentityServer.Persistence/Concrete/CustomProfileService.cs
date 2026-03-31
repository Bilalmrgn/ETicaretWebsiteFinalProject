using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityServer.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Duende.IdentityModel;

namespace IdentityServer.Persistence.Concrete
{
    /// <summary>
    /// IProfileService: IdentityServer'ın kullanıcı bilgilerini bulundurur. (Claim)
    /// Token içerisine yerleştirmek için kullanılır
    /// </summary>
    public class CustomProfileService : IProfileService
    {
        private readonly UserManager<AppUser> _userManager;

        public CustomProfileService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);

            if (user != null)
            {
                //manuel eklenen claim leri al
                var claims = await _userManager.GetClaimsAsync(user);

                //kullanıcının rolleri
                var roles = await _userManager.GetRolesAsync(user);

                //rolleri claim listesine ekle
                foreach (var role in roles)
                {
                    claims.Add(new System.Security.Claims.Claim(JwtClaimTypes.Role, role));
                }

                context.IssuedClaims.AddRange(claims);
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();

            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
