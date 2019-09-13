using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace golf.Core.Models.Entities
{
    public class Score
    {
        [Key]
        public int Id { get; set; }
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public int HoleId { get; set; }
        public int GameScore { get; set; }
        public int GamePoints { get; set; }


        [ForeignKey("PlayerId")]
        public virtual Player Player { get; set; }

        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }

        [ForeignKey("HoleId")]
        public virtual Hole Hole { get; set; }

    }
}
