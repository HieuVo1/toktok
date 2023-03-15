using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace toktok_api.Models
{
    public class BaseEntity
    {
        public DateTime ModifiedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}