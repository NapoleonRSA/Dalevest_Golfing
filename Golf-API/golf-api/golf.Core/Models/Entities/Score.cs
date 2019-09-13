using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace golf.Core.Models.Entities
{
    public class Score
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public int GameScore { get; set; }
        public int GamePoints { get; set; }


        [ForeignKey("PlayerId")]
        public virtual Player Player { get; set; }

        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }

    }
}
