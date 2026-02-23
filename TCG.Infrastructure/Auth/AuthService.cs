using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace TCG.Infrastructure.Auth
{
    public class AuthService
    {
        // 2/2/2026 https://www.youtube.com/watch?v=b7-BC7VyyLk

        public AuthService()
        {
            Users = new Dictionary<string, ClaimsPrincipal>();
        }

        public Dictionary<string, ClaimsPrincipal> Users { get; set; }
    }
}
