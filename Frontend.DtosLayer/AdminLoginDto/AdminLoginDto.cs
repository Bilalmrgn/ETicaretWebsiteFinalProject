using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Frontend.DtosLayer.AdminLoginDto
{
    public class AdminLoginDto
    {
        [JsonProperty("succeeded")]
        public bool Succeeded { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("rememberMe")]
        public bool RememberMe { get; set; }

        [JsonProperty("roles")]
        public List<string> Roles { get; set; }
    }
}
