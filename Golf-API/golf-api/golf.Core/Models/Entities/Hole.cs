using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace golf.Core.Models.Entities
{
    public class Hole
    {
        [Key]
        public int Id { get; set; }
        public int hole_nr { get; set; }
        public int Score { get; set; }
        public int Strokes { get; set; }

        [ForeignKey("PlayerId")]
        public virtual Player Player { get; set; }
    }
}
