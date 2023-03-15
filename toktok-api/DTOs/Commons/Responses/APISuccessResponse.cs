using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace toktok_api.DTOs.Commons.Responses
{
    public class APISuccessResponse<T> : APIResponse<T>
    {
        public APISuccessResponse(T data)
        {
            IsSuccess = true;
            Data = data;
        }
    }
}