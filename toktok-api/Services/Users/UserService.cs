using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CryptoHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using toktok_api.Context;
using toktok_api.DTOs.Commons.Responses;
using toktok_api.DTOs.User.Requests;
using toktok_api.DTOs.User.Responses;
using toktok_api.Models;

namespace toktok_api.Services.Users
{
    public class UserService : IUserService
    {
        private readonly TokTokDbContext _context;
        private readonly IConfiguration _configuration;
        public UserService(TokTokDbContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }
        public async Task<APIResponse<string>> LoginAsync(LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.LoginName || u.Email == request.LoginName);
            if (user is null) throw new KeyNotFoundException("Username or email not found");
            if (!Crypto.VerifyHashedPassword(user.Password, request.Password))
            {
                throw new ArgumentException("Username or Password is incorrect");
            }
            var token = CreateToken(user, false, _configuration);
            return new APISuccessResponse<string>(token);
        }
        public async Task<APIResponse<RegisterResponse>> RegisterAsync(RegisterRequest request)
        {
            var userExist = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username || u.Email == request.Email);
            if (userExist != null)
                throw new ArgumentException("Username or Email already exist!");
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                Email = request.Email,
                Password = Crypto.HashPassword(request.Password),
                Active = true
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            RegisterResponse response = new RegisterResponse()
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };
            return new APISuccessResponse<RegisterResponse>(response);
        }

        public static string CreateToken(User user, bool isSocial, IConfiguration _configuration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.GivenName, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                };
            var key = Encoding.ASCII.GetBytes(_configuration["Authentication:Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _configuration["Authentication:Jwt:Audience"],
                Issuer = _configuration["Authentication:Jwt:Issuer"],
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(_configuration.GetSection("Authentication:Jwt:ExpireHour").Get<int>()),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}