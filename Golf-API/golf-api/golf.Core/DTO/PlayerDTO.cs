﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace golf.Core.DTO
{
    public class PlayerDTO
    {
       // public int Id { get; set; }
        public string PlayerName { get; set; }
        public int Team_id { get; set; }
        public int TotalScore { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
