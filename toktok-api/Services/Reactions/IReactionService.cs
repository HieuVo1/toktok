using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toktok_api.DTOs.Commons.Responses;
using toktok_api.DTOs.Reactions.Requests;

namespace toktok_api.Services.Reactions
{
    public interface IReactionService
    {
        Task<APIResponse<bool>> ReactMovieAsync(ReactionRequest request);
    }
}