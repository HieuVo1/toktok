using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toktok_api.DTOs.Reactions.Responses;

namespace toktok_api.DTOs.Movies.Responses
{
    public class MovieResponse
    {
        public Guid MovieId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public List<ReactionResponse> Reactions { get; set; }
    }
}