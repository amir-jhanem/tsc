using System;

namespace TSC.Controllers.Resources
{
    public class TokenResultResource
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public bool isAdmin { get; set; }
        public string FullName { get; set; }
    }
}