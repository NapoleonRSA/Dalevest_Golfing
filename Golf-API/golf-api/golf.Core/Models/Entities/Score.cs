using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using golf.Core.SeedWork;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace golf.Core.Models.Entities
{
    public class Score: BaseEntity
    {
        public Game Game { get; set; }
        public Player Player { get; set; }
        public List<PlayerHoleScore> Holes { get; set; }
        public int GameScore { get; set; }
        public int GamePoints { get; set; }
    }
}
