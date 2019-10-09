using System;
using System.Collections.Generic;
using System.Text;
using golf.Core.SeedWork;

namespace golf.Core.Models.Entities
{
    public class PlayerHoleScore: BaseEntity
    {
        public Player Player { get; set; }
        public Hole Hole { get; set; }
        public int Score { get; set; }
        public int Points { get; set; }
        public Score GameScore { get; set; }
        public DateTime? ScoreUpdated { get; set; }
    }
}
