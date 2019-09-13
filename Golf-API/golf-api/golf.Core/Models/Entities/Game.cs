using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace golf.Core.Models.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public string GameName { get; set; }
        public string Password { get; set; }
        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

    }
}
