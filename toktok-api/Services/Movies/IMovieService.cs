using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toktok_api.DTOs.Commons.Requests;
using toktok_api.DTOs.Commons.Responses;
using toktok_api.DTOs.Movies.Requests;
using toktok_api.DTOs.Movies.Responses;

namespace toktok_api.Services.Movies
{
    public interface IMovieService
    {
        Task<APIResponse<PagedList<MovieResponse>>> GetAllAsync(QueryStringParameters parameter);
        Task<APIResponse<MovieResponse>> CreateAsync(CreateMovieRequest request);
    }
}