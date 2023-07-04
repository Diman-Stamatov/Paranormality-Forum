using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ForumSystemTeamFour.Models.DTOs
{
    public class VotesDto : IEquatable<VotesDto>
    {
        public List<string> Likes { get; set; }
        public List<string> Dislikes { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as VotesDto);
        }

        public bool Equals(VotesDto other)
        {
            return other is not null &&
                   this.Likes.SequenceEqual(other.Likes) &&
                   this.Dislikes.SequenceEqual(other.Dislikes);
        }
    }
}
