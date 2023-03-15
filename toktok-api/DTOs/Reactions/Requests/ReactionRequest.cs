using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using toktok_api.Enums;

namespace toktok_api.DTOs.Reactions.Requests
{
    public class ReactionRequest
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid MovieId { get; set; }
        [Required]
        public ReactionType ReactionType { get; set; }
    }
}