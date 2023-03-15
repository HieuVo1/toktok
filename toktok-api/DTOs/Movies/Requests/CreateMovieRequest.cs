using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace toktok_api.DTOs.Movies.Requests
{
    public class CreateMovieRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}