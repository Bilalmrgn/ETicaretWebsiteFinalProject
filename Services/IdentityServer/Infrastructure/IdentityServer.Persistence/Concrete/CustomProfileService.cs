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
                //Veritabanındaki role : user ya da role : admin claim ini çekiyoruz
                var claims = await _userManager.GetClaimsAsync(user);

                //bu claim i token'ın içerisine ekliyoruz
                context.IssuedClaims.AddRange(claims);
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();

            var user= await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
