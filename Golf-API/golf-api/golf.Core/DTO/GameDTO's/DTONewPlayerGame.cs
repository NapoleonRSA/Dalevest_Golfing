using System;
using System.Collections.Generic;
using System.Text;

namespace golf.Core.DTO.GameDTO_s
{
    public class DTONewPlayerGame
    {
        public int gameId { get; set; }
        public int playerId { get; set; }
        public List<int> holeId { get; set; }

    }
}
