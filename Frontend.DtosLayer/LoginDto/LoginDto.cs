using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.DtosLayer.LoginDto
{
    public class LoginDto
    {
        public bool Succeeded { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public List<string> Roles { get; set; }
    }
}
