using System;
using System.Collections.Generic;
using System.Text;

namespace golf.Core.DTO
{
    public class DTOTeam
    {
        public int GameId { get; set; }
        public List<DTOPlayer> Players { get; set; }
        public string Description { get; set; }
        public DTOPlayer Captain { get; set; }
    }
}
