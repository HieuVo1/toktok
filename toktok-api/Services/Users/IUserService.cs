using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toktok_api.DTOs.Commons.Responses;
using toktok_api.DTOs.User.Requests;
using toktok_api.DTOs.User.Responses;

namespace toktok_api.Services.Users
{
    public interface IUserService
    {
        Task<APIResponse<RegisterResponse>> RegisterAsync(RegisterRequest request);
        Task<APIResponse<string>> LoginAsync(LoginRequest request);
    }
}