using System;
using System.Collections.Generic;
using System.Text;

namespace golf.Core.DTO.GameDTO_s
{
    public class DTOPlayerGameScore
    {
        public string PlayerName { get; set; }
        public int Score { get; set; }
        public int Points { get; set; }
        public int Thru { get; set; }
    }
}
