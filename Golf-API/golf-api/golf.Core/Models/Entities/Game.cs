using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using golf.Core.SeedWork;

namespace golf.Core.Models.Entities
{
    public class Game: BaseEntity
    {
        public string GameName { get; set; }
        public string Password { get; set; }
        public Course Course { get; set; }

        public GameType GameType { get; set; }
    }
}
