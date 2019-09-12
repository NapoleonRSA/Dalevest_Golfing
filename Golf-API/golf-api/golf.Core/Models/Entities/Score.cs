using System;
using System.Collections.Generic;
using System.Text;

namespace golf.Core.Models.Entities
{
    public class Score
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public int HoleId { get; set; }
        public int GameScore { get; set; }
        public int Points { get; set; }
    }
}
