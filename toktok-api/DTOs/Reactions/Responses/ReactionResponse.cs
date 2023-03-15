using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toktok_api.Enums;

namespace toktok_api.DTOs.Reactions.Responses
{
    public class ReactionResponse
    {
        public Guid UserId { get; set; }
        public Guid MovieId { get; set; }
        public ReactionType ReactionType { get; set; }
    }
}