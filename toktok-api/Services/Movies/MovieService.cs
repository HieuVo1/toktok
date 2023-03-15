using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using toktok_api.Context;
using toktok_api.DTOs.Commons.Requests;
using toktok_api.DTOs.Commons.Responses;
using toktok_api.DTOs.Movies.Requests;
using toktok_api.DTOs.Movies.Responses;
using toktok_api.DTOs.Reactions.Responses;
using toktok_api.Models;

namespace toktok_api.Services.Movies
{
    public class MovieService : IMovieService
    {
        private readonly TokTokDbContext _context;
        public MovieService(TokTokDbContext context)
        {
            _context = context;
        }

        public async Task<APIResponse<MovieResponse>> CreateAsync(CreateMovieRequest request)
        {
            var movie = new Movie()
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                ImageUrl = request.ImageUrl
            };
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            var moviesResponse = new MovieResponse()
            {
                MovieId = movie.Id,
                ImageUrl = movie.ImageUrl,
                Title = movie.Title,
                Reactions = new List<ReactionResponse>()
            };
            return new APISuccessResponse<MovieResponse>(moviesResponse);
        }

        public async Task<APIResponse<PagedList<MovieResponse>>> GetAllAsync(QueryStringParameters parameters)
        {
            var source = _context.Movies.Include(mv => mv.Reactions);

            var count = source.Count();

            var movies = await source.Skip((parameters.PageNumber - 1) * parameters.PageSize).Take(parameters.PageSize).ToListAsync();

            var moviesResponse = movies.Select(mv => new MovieResponse()
            {
                MovieId = mv.Id,
                ImageUrl = mv.ImageUrl,
                Title = mv.Title,
                Reactions = mv.Reactions.Select(rc => new ReactionResponse()
                {
                    MovieId = rc.MovieId,
                    UserId = rc.UserId,
                    ReactionType = rc.ReactionType
                }).ToList()
            }).ToList();

            var result = new PagedList<MovieResponse>(moviesResponse, count, parameters.PageNumber, parameters.PageSize);

            return new APISuccessResponse<PagedList<MovieResponse>>(result);
        }
    }
}