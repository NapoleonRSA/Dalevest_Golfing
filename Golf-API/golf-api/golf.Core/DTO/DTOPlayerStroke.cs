using System;
using System.Collections.Generic;
using System.Text;

namespace golf.Core.DTO
{
    public class DTOPlayerStroke
    {
        public int playerId { get; set; }
        public int hole_nr { get; set; }
        public int Strokes { get; set; }
    }
}
