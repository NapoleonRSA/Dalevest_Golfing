using golf.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace golf.Core.Models.Entities
{
    public class Team: BaseEntity
    {

        public ICollection<TeamPlayer> TeamPlayers { get; set; }
        public Game Game { get; set; }

        public Player Captain { get; set; }

        public String TeamName { get; set; }

    }

    public class TeamPlayer
    {
        public int TeamId { get; set; }
        public Team Team { get; set; }

        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
