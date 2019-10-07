using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using golf.Core.SeedWork;
using Microsoft.AspNetCore.Identity;

namespace golf.Core.Models.Entities
{
    public class Player: BaseEntity
    {
        public string Email { get; set; }
        public string PlayerName { get; set; }
        public string LastName { get; set; }
        public string Cellphone { get; set; }
        public int HandiCap { get; set; }
        public ICollection<TeamPlayer> TeamPlayers { get; set; }

    }
}
