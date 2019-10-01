using System;
using System.Collections.Generic;
using System.Text;

namespace golf.Core.Models.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public List<Player> Players { get; set; }
        public Game Game { get; set; }

    }
}
