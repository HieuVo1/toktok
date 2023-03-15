using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace toktok_api.DTOs.Commons.Responses
{
    public class APIErrorResponse<T> : APIResponse<T>
    {
        public APIErrorResponse(string message)
        {
            IsSuccess = false;
            ErrorMessage = message;
        }
    }
}