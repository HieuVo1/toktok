using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using toktok_api.DTOs.Commons.Requests;
using toktok_api.DTOs.Commons.Responses;
using toktok_api.DTOs.Movies.Requests;
using toktok_api.DTOs.Movies.Responses;
using toktok_api.Services.Movies;

namespace toktok_api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        [HttpGet("get-all")]
        public async Task<ActionResult<PagedList<MovieResponse>>> GetAll([FromQuery] QueryStringParameters parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _movieService.GetAllAsync(parameters);

            result.Metadata = new
            {
                result.Data.TotalCount,
                result.Data.PageSize,
                result.Data.CurrentPage,
                result.Data.TotalPages,
                result.Data.HasNext,
            };

            return Ok(result);
        }
        [HttpPost("create")]
        public async Task<ActionResult<MovieResponse>> Create(CreateMovieRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _movieService.CreateAsync(request);
            return Ok(result);
        }
    }
}