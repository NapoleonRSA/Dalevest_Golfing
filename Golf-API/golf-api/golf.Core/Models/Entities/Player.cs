using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace golf.Core.Models.Entities
{
    public class Player
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }
        public string PlayerName { get; set; }
        public string LastName { get; set; }
        public string Cellphone { get; set; }
        public int Team_id { get; set; }
        public int TotalScore { get; set; }

    }
}
