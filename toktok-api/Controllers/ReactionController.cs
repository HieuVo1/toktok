using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using toktok_api.DTOs.Reactions.Requests;
using toktok_api.Services.Reactions;

namespace toktok_api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ReactionController : ControllerBase
    {
        private readonly IReactionService _reactionService;
        public ReactionController(IReactionService reactionService)
        {
            _reactionService = reactionService;
        }
        [HttpPost("react-movie")]
        public async Task<ActionResult<string>> ReactMovieAsync(ReactionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _reactionService.ReactMovieAsync(request);
            return Ok(result);
        }
    }
}