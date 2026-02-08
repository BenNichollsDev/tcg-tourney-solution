using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Application.Models
{
    public sealed class LoginRequest
    {
        public string Email { get; init; } = "";
        public string Password { get; init; } = "";
    }
}