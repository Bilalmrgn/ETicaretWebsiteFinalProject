using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Application.Dtos
{
    public class UserResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}
