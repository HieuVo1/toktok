using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace toktok_api.DTOs.User.Responses
{
    public class RegisterResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}