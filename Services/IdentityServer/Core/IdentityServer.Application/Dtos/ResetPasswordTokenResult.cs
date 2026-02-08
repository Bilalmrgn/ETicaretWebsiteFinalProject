using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Application.Dtos
{
    public class ResetPasswordTokenResult
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
