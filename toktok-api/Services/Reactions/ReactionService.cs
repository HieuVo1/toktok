using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using toktok_api.Context;
using toktok_api.DTOs.Commons.Responses;
using toktok_api.DTOs.Reactions.Requests;
using toktok_api.Models;

namespace toktok_api.Services.Reactions
{
    public class ReactionService : IReactionService
    {
        private readonly TokTokDbContext _context;
        public ReactionService(TokTokDbContext context)
        {
            _context = context;
        }
        public async Task<APIResponse<bool>> ReactMovieAsync(ReactionRequest request)
        {
            var reaction = await _context.Reactions.FirstOrDefaultAsync(rc => rc.UserId == request.UserId && rc.MovieId == request.MovieId);
            if (reaction is null)
            {
                if (request.ReactionType == Enums.ReactionType.DisLike)
                {
                    return new APISuccessResponse<bool>(true);
                };

                var newReaction = new Reaction()
                {
                    UserId = request.UserId,
                    MovieId = request.MovieId,
                    ReactionType = request.ReactionType
                };
                _context.Reactions.Add(newReaction);
                await _context.SaveChangesAsync();
                return new APISuccessResponse<bool>(true);
            }

            if (request.ReactionType == Enums.ReactionType.DisLike)
            {
                _context.Reactions.Remove(reaction);
                await _context.SaveChangesAsync();
                return new APISuccessResponse<bool>(true);
            }

            reaction.ReactionType = request.ReactionType;
            await _context.SaveChangesAsync();
            return new APISuccessResponse<bool>(true);
        }
    }
}