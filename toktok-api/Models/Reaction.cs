using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toktok_api.Enums;

namespace toktok_api.Models
{
    public class Reaction : BaseEntity
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
        public Movie Movie { get; set; }
        public Guid MovieId { get; set; }
        public ReactionType ReactionType { get; set; }
    }
}