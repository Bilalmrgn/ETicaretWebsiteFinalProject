using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Duende.IdentityServer.Validation;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using IdentityServer.Domain;

namespace IdentityServer.Persistence.Concrete
{
    //şifre ve username kontrolü ve kullanıcının kim olduğunu kontrol eden sınıftır
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<AppUser> _userManager;
        public CustomResourceOwnerPasswordValidator(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await _userManager.FindByNameAsync(context.UserName);

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(context.UserName);
            }

            if (user == null)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Kullanıcı Bulunamadı. (CustomResourceOwnerPassword Validator sınıfında)");
                return;
            }

            var passwordValidation = await _userManager.CheckPasswordAsync(user, context.Password);

            if (!passwordValidation)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Şifre yanlış");
                return;
            }

            context.Result = new GrantValidationResult(subject: user.Id, authenticationMethod: "password");
        }
    }
}
